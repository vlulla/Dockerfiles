# syntax=docker/dockerfile:1
ARG VERSION=jammy
FROM ubuntu:${VERSION}
ARG VERSION
LABEL maintainer "Vijay Lulla <vijaylulla@gmail.com>"

SHELL ["/bin/bash", "-eux", "-o", "pipefail", "-c"]

## Useful tools for all of my exploratory work.
RUN <<EOT
set -ex
export DEBIAN_FRONTEND=noninteractive
apt-get update -qq --yes

apt-get install --yes -qq --auto-remove --no-install-recommends build-essential ca-certificates tini coreutils findutils \
   git jq git htop vim ripgrep curl gnupg strace psmisc iputils-ping tcpdump traceroute procps zsh parallel lua5.4 graphviz \
   sed gawk sqlite3 unzip zstd apt-utils less libcurl4-openssl-dev libxml2-dev libnode-dev libuv1-dev default-jdk rlwrap xserver-xorg-core aria2 wget

curl -sL https://cloud.r-project.org/bin/linux/ubuntu/marutter_pubkey.asc | tee -a /etc/apt/trusted.gpg.d/cran_ubuntu_key.asc
printf "deb https://cloud.r-project.org/bin/linux/ubuntu jammy-cran40/" > /etc/apt/sources.list.d/R.list
apt-get update -qq --yes
apt-get install --yes -qq --auto-remove r-base r-base-dev gfortran libatlas3-base libssl-dev libudunits2-dev r-cran-ggplot2 r-cran-data.table r-cran-rsqlite r-cran-matrix r-cran-tidyverse r-cran-rcpp r-cran-sf \
   libgdal-dev libfftw3-dev libgit2-dev r-cran-caret r-cran-docopt r-cran-brms r-cran-glmnet littler libfribidi-dev uuid-dev

ln -s /usr/lib/R/site-library/littler/examples/install.r /usr/local/bin/install.r
ln -s /usr/lib/R/site-library/littler/examples/install2.r /usr/local/bin/install2.r
ln -s /usr/lib/R/site-library/littler/examples/installGithub.r /usr/local/bin/installGithub.r
ln -s /usr/lib/R/site-library/littler/examples/installBioc.r /usr/local/bin/installBioc.r

MAKEFLAGS="${MAKEFLAGS:- }-j$(( $(nproc) - 2))" install2.r --error --skipinstalled xgboost arrow e1071 simstudy collapse rbenchmark
MAKEFLAGS="${MAKEFLAGS:- }-j$(( $(nproc) - 2))" install2.r --error BH

apt-get autoclean && rm -rf /var/lib/apt/lists/*
unset DEBIAN_FRONTEND
EOT

RUN <<EOT
set -ex
pushd /tmp && git clone https://gitlab.com/OldManProgrammer/unix-tree tree && pushd tree && make && make install && popd && rm -rf tree && popd
pushd /tmp && aria2c https://github.com/duckdb/duckdb/releases/download/v0.10.2/duckdb_cli-linux-amd64.zip && unzip duckdb_cli-linux-amd64.zip -d /usr/local/bin && rm -rf duckdb_cli-linux-amd64.zip
EOT

RUN <<EOT
mkdir -p /etc/apt/keyrings
curl -L https://pkg.osquery.io/deb/pubkey.gpg | tee /etc/apt/keyrings/osquery.asc
printf "deb [arch=amd64 signed-by=/etc/apt/keyrings/osquery.asc] https://pkg.osquery.io/deb deb main" > /etc/apt/sources.list.d/osquery.list
apt-get update -qq --yes
apt-get install --yes osquery
EOT

ARG UID=${UID:-1000}
ARG USR=${USR:-usr}
ARG GID=${GID:-1000}
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
ln -s /home/${USR}/code/config/Rprofile /home/${USR}/.Rprofile
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
(
extensions=( arrow autocomplete aws azure excel fts httpfs iceberg icu inet json parquet postgres_scanner spatial sqlite_scanner substrait tpcds tpch )
for ext in ${extensions[@]}; do
  printf "INSTALL '%s';\n" "${ext}"
done
) | duckdb
EOT

RUN <<EOT
set -ex
pushd /home/${USR}/.local
curl -Ls https://micro.mamba.pm/api/micromamba/linux-64/latest | tar -xvj bin/micromamba
mkdir -p /home/${USR}/micromamba && ./bin/micromamba shell init -s zsh -p /home/${USR}/micromamba
MAMBA_ROOT_PREFIX=/home/${USR}/micromamba /home/${USR}/.local/bin/micromamba install --channel conda-forge --name base --yes ipython numpy pandas pyarrow scikit-learn polars hypothesis pytest python-duckdb
MAMBA_ROOT_PREFIX=/home/${USR}/micromamba /home/${USR}/.local/bin/micromamba clean --all --yes
printf 'micromamba activate\n' | tee -a /home/${USR}/.zshrc >> /home/${USR}/.bashrc
EOT

## RUN <<EOT
## set -ex
## curl -fsSL https://install.julialang.org | sh -s -- -y
## EOT

## ## Understanding interaction between ENTRYPOINT and CMD is helpful. https://docs.docker.com/engine/reference/builder/#understand-how-cmd-and-entrypoint-interact
## Might resolve timedatectl/systemd issues that I saw in R console on Vertex AI??
ENTRYPOINT ["tini", "--"]

CMD ["/usr/bin/zsh"]

