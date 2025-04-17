# syntax=docker/dockerfile:1
ARG VERSION=latest
FROM rocker/r-base:${VERSION}
ARG VERSION
LABEL maintainer="Vijay Lulla <vijaylulla@gmail.com>"

SHELL ["/bin/bash", "-eux", "-o", "pipefail", "-c"]

## Useful tools for all of my exploratory work.
RUN <<EOT
set -ex
export DEBIAN_FRONTEND=noninteractive
apt-get update -qq --yes

apt-get install --yes --auto-remove --no-install-recommends build-essential ca-certificates tini coreutils findutils git tree jq htop vim ripgrep curl gnupg strace psmisc iputils-ping tcpdump traceroute procps

apt-get autoclean && rm -rf /var/lib/apt/lists/*
unset DEBIAN_FRONTEND
EOT

RUN <<EOT
wget https://packages.microsoft.com/config/debian/12/packages-microsoft-prod.deb -O packages-microsoft-prod.deb
dpkg -i packages-microsoft-prod.deb
rm packages-microsoft-prod.deb
apt-get update -qq --yes
apt-get upgrade --yes
apt-get install --yes dotnet-sdk-9.0
ln -s /usr/lib/x86_64-linux-gnu/libdl.so{.2,}
EOT

ARG USR=${USR:-usr}
ARG GRP=${GRP:-grp}

## ## See the rules that ADD obeys at https://docs.docker.com/engine/reference/builder/#add
## ## <src> path must be **inside of the context** of the build! And, that is why I cannot do ADD --chown=${USR}:${GRP} /home/vijay /home/usr !
##
## ADD --chown=${USR}:${GRP} . /home/usr

RUN <<EOT
set -ex
adduser --group ${GRP}
adduser --shell /bin/bash --ingroup ${GRP} ${USR}
EOT

WORKDIR /home/${USR}
USER ${USR}

## ## Understanding interaction between ENTRYPOINT and CMD is helpful. https://docs.docker.com/engine/reference/builder/#understand-how-cmd-and-entrypoint-interact
## Might resolve timedatectl/systemd issues that I saw in R console on Vertex AI??
ENTRYPOINT ["tini", "--"]

CMD ["/bin/bash"]

