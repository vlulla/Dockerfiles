# syntax=docker/dockerfile:1
ARG VERSION=latest
FROM debian:${VERSION}
ARG VERSION

RUN <<EOT
apt-get update -qq --yes 

apt-get install --yes --auto-remove --no-install-recommends build-essential
apt-get install --yes --auto-remove zsh 
apt-get install --yes --auto-remove jq git # install packages you need here...

apt-get autoclean && rm -rf /var/lib/apt/lists/*
EOT


ARG USR=usr
ARG GRP=grp

RUN <<EOT
adduser --group ${GRP}
adduser --shell /bin/zsh --ingroup ${GRP} ${USR}
EOT

WORKDIR /home/${USR}
USER ${USR}

CMD ["/bin/bash"]

