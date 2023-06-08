# syntax=docker/dockerfile:1
ARG VERSION=latest
FROM debian:${VERSION}
ARG VERSION
LABEL maintainer "Vijay Lulla <vijaylulla@gmail.com>"

SHELL ["/bin/bash", "-c"]

## Useful tools for all of my exploratory work.
RUN <<EOT
export DEBIAN_FRONTEND=noninteractive
apt-get update -qq --yes

apt-get install --yes -qq --auto-remove --no-install-recommends build-essential ca-certificates tini coreutils findutils \
   git  jq git htop vim ripgrep curl gnupg strace psmisc iputils-ping tcpdump traceroute procps zsh parallel lua5.4 graphviz \
   sed gawk sqlite3 unzip zstd apt-utils less libcurl4-openssl-dev libxml2-dev libnode-dev libuv1-dev

gpg --keyserver keyserver.ubuntu.com --recv-key '95C0FAF38DB3CCAD0C080A7BDC78B2DDEABC47B7'
gpg --armor --export '95C0FAF38DB3CCAD0C080A7BDC78B2DDEABC47B7' | tee /etc/apt/trusted.gpg.d/cran_debian_key.asc
printf 'deb http://cloud.r-project.org/bin/linux/debian bookworm-cran40/' > /etc/apt/sources.list.d/R.list
apt-get update -qq --yes
apt-get install --yes -qq --auto-remove r-base r-base-dev gfortran libatlas3-base libssl-dev libudunits2-dev r-cran-ggplot2 r-cran-data.table r-cran-rsqlite r-cran-matrix r-cran-tidyverse r-cran-rcpp r-cran-sf

apt-get autoclean && rm -rf /var/lib/apt/lists/*
unset DEBIAN_FRONTEND
EOT

RUN <<EOT
pushd /tmp && git clone https://gitlab.com/OldManProgrammer/unix-tree tree && pushd tree && make && make install && popd && popd && rm -rf tree
pushd /tmp && curl -sSLO https://github.com/duckdb/duckdb/releases/download/v0.8.0/duckdb_cli-linux-amd64.zip && unzip duckdb_cli-linux-amd64.zip -d /usr/local/bin && rm -rf duckdb_cli-linux-amd64.zip
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
addgroup --gid ${GID} ${GRP}
adduser --shell /usr/bin/zsh --uid ${UID} --ingroup ${GRP} ${USR}
EOT

WORKDIR /home/${USR}
USER ${USR}

RUN <<EOT
mkdir -p code 
cd code && git clone https://github.com/vlulla/config.git

printf '[[ -f "/home/%s/code/config/zshrc" ]] && source "/home/%s/code/config/zshrc"\n' ${USR:-usr} ${USR:-usr} > /home/${USR:-usr}/.zshrc
printf 'source /home/%s/code/config/vimrc\n' "${USR:-usr}" > /home/${USR:-usr}/.vimrc
ln -s /home/${USR:-usr}/code/config/Rprofile /home/${USR:-usr}/.Rprofile
ln -s /home/${USR:-usr}/code/config/sqliterc /home/${USR:-usr}/.sqliterc
ln -s /home/${USR:-usr}/code/config/duckdbrc /home/${USR:-usr}/.duckdbrc
ln -s /home/${USR:-usr}/code/config/psqlrc /home/${USR:-usr}/.psqlrc
Rscript -e "dir.create(Sys.getenv('R_LIBS_USER'),recursive=TRUE)"

## ## The below is needed because fs package in R will not compile otherwise
## printf 'unset QUOTING_STYLE' >> /home/${USR:-usr}/.zshrc
( cat <<EOF
echo "#######################################################################################"
echo "# If installing packages in R raises lots of errors with ls -t then run it like this: #"
echo "#                                                                                     #"
echo "# zsh $ ( unset QUOTING_STYLE && R )                                                  #"
echo "#######################################################################################"
EOF
) >> /home/${USR:-usr}/.zshrc
EOT

## ## Understanding interaction between ENTRYPOINT and CMD is helpful. https://docs.docker.com/engine/reference/builder/#understand-how-cmd-and-entrypoint-interact
## Might resolve timedatectl/systemd issues that I saw in R console on Vertex AI??
ENTRYPOINT ["tini", "--"]

CMD ["/usr/bin/zsh"]

