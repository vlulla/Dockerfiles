# syntax=docker/dockerfile:1
ARG VERSION=latest
FROM debian:${VERSION}
ARG VERSION
LABEL maintainer "Vijay Lulla <vijaylulla@gmail.com>"

SHELL ["/bin/bash", "-eux", "-o", "pipefail", "-c"]

## Useful tools for all of my exploratory work.
RUN <<EOT
set -ex
export DEBIAN_FRONTEND=noninteractive
apt-get update -qq --yes

apt-get install --yes --auto-remove --no-install-recommends build-essential ca-certificates tini coreutils findutils git tree erlang wget

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
set -ex
adduser --group ${GRP}
adduser --shell /bin/bash --ingroup ${GRP} ${USR}
EOT

WORKDIR /home/${USR}
USER ${USR}

RUN <<EOT
set -ex
cd /home/${USR}
git clone https://github.com/erlang/rebar3.git rebar3.git
cd rebar3.git && ./bootstrap
./rebar3 local install
printf 'export PATH=%s:/home/%s/.cache/rebar3/bin\n' "${PATH}" "${USR}" >> /home/${USR}/.bashrc
cd /home/${USR}
git clone https://github.com/proper-testing/proper proper.git
cd proper.git && PATH=${PATH}:/home/${USR}/.cache/rebar3/bin make

printf 'export ERL_LIBS=/home/%s/proper.git\n' "${USR}" >> /home/${USR}/.bashrc
printf 'code:load_abs("/home/%s/proper.git").\n' "${USR}" >> /home/${USR}/.erlang
EOT

## ## Understanding interaction between ENTRYPOINT and CMD is helpful. https://docs.docker.com/engine/reference/builder/#understand-how-cmd-and-entrypoint-interact
## Might resolve timedatectl/systemd issues that I saw in R console on Vertex AI??
ENTRYPOINT ["tini", "--"]

CMD ["/bin/bash"]

