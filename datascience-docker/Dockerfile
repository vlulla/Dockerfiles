# syntax=docker/dockerfile:1
ARG VERSION=latest
FROM rocker/r-ver:${VERSION}
ARG VERSION
LABEL maintainer="Vijay Lulla <vijaylulla@gmail.com>"

SHELL ["/bin/bash", "-eux", "-o", "pipefail", "-c"]

## Useful tools for all of my exploratory work.
RUN <<EOT
set -ex
export DEBIAN_FRONTEND=noninteractive
apt-get update -qq --yes
apt-get upgrade --yes

apt-get install --yes -qq --auto-remove --no-install-recommends build-essential ca-certificates tini coreutils findutils \
   git jq git htop vim ripgrep curl gnupg strace psmisc iputils-ping tcpdump traceroute procps zsh parallel lua5.4 graphviz \
   sed gawk sqlite3 unzip zstd apt-utils less libcurl4-openssl-dev libxml2-dev libnode-dev libuv1-dev default-jdk rlwrap xserver-xorg-core aria2 wget \
   libudunits2-dev libgdal-dev libgeos-dev libhdf5-dev libproj-dev libnetcdf-dev libpq-dev cmake libglpk-dev shellcheck tk libgl1-mesa-dev libglu1-mesa-dev

apt-get autoclean && rm -rf /var/lib/apt/lists/*
unset DEBIAN_FRONTEND
EOT

RUN install2.r --error --skipinstalled --skipmissing -n "$(( $(nproc) - 2))" xgboost arrow e1071 simstudy fastverse rbenchmark fastmatch BH ggplot2 RSQLite sf rstan rstanarm duckdb tidyverse rgl caret
RUN R --no-restore -e "install.packages(c('remotes'),dependencies=T)"
RUN R --no-restore -e "remotes::install_github('stan-dev/posteriordb-r')"
RUN R --no-restore -e "install.packages(c('devtools','mvtnorm','brms','dagitty','fpp3','bsts'),dependencies=T)"
RUN R --no-restore -e "remotes::install_github('facebook/prophet@*release',subdir='R')"
RUN R --no-restore -e "remotes::install_github(c('stan-dev/cmdstanr','rmcelreath/rethinking'),dependencies=T)"
## RUN <<EOT
## mkdir -p /opt/cmdstan
## R --no-restore -e "cmdstanr::install_cmdstan(dir='/opt/cmdstan')"
## EOT
## ENV CMDSTAN="/opt/cmdstan"
## RUN R --no-save --no-restore -e "cmdstanr::rebuild_cmdstan()"
## RUN cd /opt/cmdstan/cmdstan-2.35.0 && make examples/bernoulli/bernoulli
RUN install2.r --error --skipinstalled --skipmissing -n "$(( $(nproc) - 2))" TTR scoringRules lightgbm

RUN <<EOT
set -ex
pushd /tmp && git clone https://gitlab.com/OldManProgrammer/unix-tree.git tree && pushd tree && make && make install && popd && rm -rf tree && popd
pushd /tmp && aria2c https://github.com/duckdb/duckdb/releases/download/v1.2.1/duckdb_cli-linux-amd64.zip && unzip duckdb_cli-linux-amd64.zip -d /usr/local/bin && rm -rf duckdb_cli-linux-amd64.zip
EOT

RUN <<EOT
mkdir -p /etc/apt/keyrings
curl -L https://pkg.osquery.io/deb/pubkey.gpg | tee /etc/apt/keyrings/osquery.asc
printf "deb [arch=amd64 signed-by=/etc/apt/keyrings/osquery.asc] https://pkg.osquery.io/deb deb main" > /etc/apt/sources.list.d/osquery.list
apt-get update -qq --yes
apt-get install --yes osquery
EOT

ARG UID=${UID:-1010}
ARG USR=${USR:-usr}
ARG GID=${GID:-1010}
ARG GRP=${GRP:-grp}

## ## See the rules that ADD obeys at https://docs.docker.com/engine/reference/builder/#add
## ## <src> path must be **inside of the context** of the build! And, that is why I cannot do ADD --chown=${USR}:${GRP} /home/vijay /home/usr !
##
## ADD --chown=${USR}:${GRP} . /home/usr

RUN <<EOT
set -ex
addgroup --gid ${GID} ${GRP}
adduser --shell /usr/bin/zsh --uid ${UID} --ingroup ${GRP} ${USR}
EOT

WORKDIR /home/${USR}
USER ${USR}

RUN <<EOT
set -ex
mkdir -p code
cd code && git clone https://github.com/vlulla/config.git

printf '[[ -f "/home/%s/code/config/zshrc" ]] && source "/home/%s/code/config/zshrc"\n' ${USR} ${USR} > /home/${USR}/.zshrc
printf 'source /home/%s/code/config/vimrc\n' "${USR}" > /home/${USR}/.vimrc
ln -s /home/${USR}/code/config/sqliterc /home/${USR}/.sqliterc
ln -s /home/${USR}/code/config/duckdbrc /home/${USR}/.duckdbrc
ln -s /home/${USR}/code/config/psqlrc /home/${USR}/.psqlrc
ln -s /home/${USR}/code/config/ripgreprc /home/${USR}/.ripgreprc
Rscript -e "dir.create(Sys.getenv('R_LIBS_USER'),recursive=TRUE)"

## ## The below is needed because fs package in R will not compile otherwise
## printf 'unset QUOTING_STYLE' >> /home/${USR}/.zshrc
( cat <<EOF
msg() {
echo '
#######################################################################################
# If installing packages in R raises lots of errors with ls -t then run R like this:  #
#                                                                                     #
# zsh $ ( unset QUOTING_STYLE && R )                                                  #
#######################################################################################
'
}
msg
EOF
) >> /home/${USR}/.zshrc
mkdir -p /home/${USR}/.local
aria2c https://github.com/clojure/brew-install/releases/latest/download/linux-install.sh && bash ./linux-install.sh -p /home/${USR}/.local && rm -rf linux-install.sh
duckdb -list -noheader -init /dev/null :memory: "select 'INSTALL '''||extension_name||''';' as stmt from duckdb_extensions() where extension_name not in ('shell','jemalloc','delta','arrow','vss') order by extension_name;" 2>/dev/null | duckdb -init /dev/null :memory:
EOT

RUN <<EOT
set -ex
pushd /home/${USR}/.local
curl -Ls https://micro.mamba.pm/api/micromamba/linux-64/latest | tar -xvj bin/micromamba
mkdir -p /home/${USR}/micromamba && ./bin/micromamba shell init --shell zsh --root-prefix=/home/${USR}/micromamba && ./bin/micromamba shell init --shell bash --root-prefix=/home/${USR}/micromamba
MAMBA_ROOT_PREFIX=/home/${USR}/micromamba /home/${USR}/.local/bin/micromamba config set extract_threads 1 ## needed for macos with ARM processor ## see https://github.com/mamba-org/micromamba-docker/issues/349
MAMBA_ROOT_PREFIX=/home/${USR}/micromamba /home/${USR}/.local/bin/micromamba create --channel conda-forge --name ds --yes ipython numpy pandas pyarrow scikit-learn polars hypothesis pytest python-duckdb einops seaborn black cmdstan sqlglot
MAMBA_ROOT_PREFIX=/home/${USR}/micromamba /home/${USR}/.local/bin/micromamba run --name ds pip install kuzu
MAMBA_ROOT_PREFIX=/home/${USR}/micromamba /home/${USR}/.local/bin/micromamba clean --all --yes
printf 'micromamba activate ds\n' | tee -a /home/${USR}/.zshrc >> /home/${USR}/.bashrc
EOT
ENV CMDSTAN=/home/${USR}/micromamba/envs/ds/bin/cmdstan

## RUN <<EOT
## set -ex
## curl -fsSL https://install.julialang.org | sh -s -- -y
## sed -i -E -e '/^path.+juliaup/s-^-postpath "${HOME}/.juliaup/bin" ## -g' /home/${USR}/.zshrc
## /home/${USR}/.juliaup/bin/julia -e 'using Pkg;[Pkg.add(p) for p in ["DataFrames","Distances","Distributions","DuckDB","GLM","GLMNet","JuMP","KernelDensity","Loess","MixedModels","MultivariateStats","NLopt","RDatasets","Rmath","StatsBase","StatsModels","Survival","TimeSeries"]]'
## EOT
##
## RUN <<EOT
## mkdir -p /home/${USR}/.julia/config && ln -s /home/${USR}/code/config/startup.jl /home/${USR}/.julia/config/startup.jl
## EOT

RUN <<EOT
## R from container does not have browser/viewer for R's `?` or `help` command requires setting help_type option to "text"
( cat <<EOF
with(list(profile_fname="~/code/config/Rprofile"),if(file.exists(profile_fname)) source(profile_fname))
options(help_type="text")
EOF
) >> /home/${USR}/.Rprofile

cd /home/${USR}/code && git clone https://github.com/vlulla/vl-utils.git
mkdir -p /home/${USR}/code/R/ && ln -s /home/${USR}/code/vl-utils/r-utils.R /home/${USR}/code/R/utils.R
mkdir -p /home/${USR}/.ipython/profile_default/startup && ln -s /home/${USR}/code/vl-utils/python-utils.py /home/${USR}/.ipython/profile_default/startup/00-python-utils.py
EOT

RUN <<EOT
cd /home/${USR}/.local/bin
curl -sSLO https://github.com/kuzudb/kuzu/releases/download/v0.6.1/kuzu_cli-linux-x86_64.tar.gz
tar xf kuzu_cli-linux-x86_64.tar.gz && rm -rf kuzu_cli-linux-x86_64.tar.gz
EOT

RUN <<EOT
mkdir -p /home/${USR}/code/k && cd /home/${USR}/code/k && git clone https://codeberg.org/ngn/k ngn-k.git && cd ngn-k.git && make && cp ./k /home/${USR}/.local/bin/
EOT
## ## Understanding interaction between ENTRYPOINT and CMD is helpful. https://docs.docker.com/engine/reference/builder/#understand-how-cmd-and-entrypoint-interact
## Might resolve timedatectl/systemd issues that I saw in R console on Vertex AI??
ENTRYPOINT ["tini", "--"]

CMD ["/usr/bin/zsh"]

