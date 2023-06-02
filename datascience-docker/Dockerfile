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

apt-get install --yes -qq --auto-remove --no-install-recommends build-essential ca-certificates tini coreutils findutils git tree jq git htop vim ripgrep curl gnupg strace psmisc iputils-ping tcpdump traceroute procps
apt-get install --yes -qq zsh 

apt-get autoclean && rm -rf /var/lib/apt/lists/*
unset DEBIAN_FRONTEND
EOT

ARG USR=usr
ARG GRP=grp

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

> /home/${USR}/.zshrc cat <<EOF
[[ -f "/home/${USR}/code/config/zshrc" ]] && source "/home/${USR}/code/config/zshrc"
EOF

> /home/${USR}/.vimrc cat <<EOF
source /home/${USR}/code/config/vimrc
EOF

EOT

## ## Understanding interaction between ENTRYPOINT and CMD is helpful. https://docs.docker.com/engine/reference/builder/#understand-how-cmd-and-entrypoint-interact
## Might resolve timedatectl/systemd issues that I saw in R console on Vertex AI??
ENTRYPOINT ["tini", "--"]

CMD ["/usr/bin/zsh"]

