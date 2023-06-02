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
   git  jq git htop vim ripgrep curl gnupg strace psmisc iputils-ping tcpdump traceroute procps zsh parallel lua5.4 graphviz

pushd /tmp && git clone https://gitlab.com/OldManProgrammer/unix-tree tree && pushd tree && make && make install && popd && popd && rm -rf tree

apt-get autoclean && rm -rf /var/lib/apt/lists/*
unset DEBIAN_FRONTEND
EOT

ARG USR=${USR:-usr}
ARG GRP=${GRP:-grp}

## ## See the rules that ADD obeys at https://docs.docker.com/engine/reference/builder/#add
## ## <src> path must be **inside of the context** of the build! And, that is why I cannot do ADD --chown=${USR}:${GRP} /home/vijay /home/usr !
##
## ADD --chown=${USR}:${GRP} . /home/usr

RUN <<EOT
adduser --group ${GRP}
adduser --shell /usr/bin/zsh --ingroup ${GRP} ${USR}
EOT

WORKDIR /home/${USR}
USER ${USR}

RUN <<EOT
mkdir -p code 
cd code && git clone https://github.com/vlulla/config.git

printf '[[ -f "/home/%s/code/config/zshrc" ]] && source "/home/%s/code/config/zshrc"\n' ${USR:-usr} ${USR:-usr} > /home/${USR:-usr}/.zshrc
printf 'source /home/%s/code/config/vimrc\n' "${USR:-usr}" > /home/${USR:-usr}/.vimrc
EOT

## ## Understanding interaction between ENTRYPOINT and CMD is helpful. https://docs.docker.com/engine/reference/builder/#understand-how-cmd-and-entrypoint-interact
## Might resolve timedatectl/systemd issues that I saw in R console on Vertex AI??
ENTRYPOINT ["tini", "--"]

CMD ["/usr/bin/zsh"]

