# syntax=docker/dockerfile:1
## FROM ubuntu:jammy
FROM debian:testing

ENV DEBIAN_FRONTEND noninteractive

ARG UNAME=vlulla
ARG UID=1000
ARG GID=1000

RUN <<EOT
set -ex
apt-get update --yes
apt-get install --yes --no-install-recommends apt-utils
apt-get install --yes texlive-latex-base texlive-fonts-recommended texlive-fonts-extra texlive-bibtex-extra texlive-xetex texlive-latex-extra texlive-latex-recommended texlive-science latexmk biber pandoc make
EOT

RUN <<EOT
set -ex
groupadd --gid ${GID} ${UNAME}
useradd -m --gid ${GID} --uid ${UID} -d /home/${UNAME} ${UNAME}
EOT

USER ${UNAME}
WORKDIR /home/${UNAME}

CMD [ "bash" ]
