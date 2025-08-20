ARG VERSION=noble
FROM ubuntu:${VERSION}
LABEL maintainer "Vijay Lulla <vijaylulla@gmail.com>"

## Build: docker build -t lilypond .
## Run  : docker run --rm -it --name lilypond lilypond
ARG DEBIAN_FRONTEND=noninteractive

SHELL ["bash", "-c"]

## ENV DISPLAY :0
RUN <<EOT
set -ex
apt-get update -qq -y
apt-get install -y --no-install-recommends build-essential vim-tiny texlive-base flex ca-certificates
apt-get install -y --no-install-recommends perl guile-3.0 guile-3.0-dev guile-2.2 guile-2.2-dev python3 flex bison ghostscript imagemagick zip rsync dblatex libpango1.0-dev fonts-texgyre tidy extractpdfmark texlive-metapost curl fontforge texlive-lang-cyrillic gettext texinfo texi2html install-info texlive-xetex tini libfl-dev texlive-font-utils
rm -rf /var/lib/apt/lists/*
EOT

WORKDIR /tmp
RUN <<EOT
set -ex
curl -SL -O https://lilypond.org/download/source/v2.24/lilypond-2.24.4.tar.gz
tar xf lilypond-2.24.4.tar.gz
cd lilypond-2.24.4
mkdir -p build
cd build
../autogen.sh --noconfigure
../configure
make -j $(( $(nproc) - 2)) all
make install
cd /tmp
rm -rf lilypond-2.24.4 lilypond-2.24.4.tar.gz
EOT

## RUN apt-get install -y iproute2 net-tools iputils-ping

ARG GID=1010
ARG UID=1010
ARG USER=
ARG GROUP=
ARG HOME=/home/${USER}
RUN groupadd -g ${GID} ${GROUP} && useradd -l -m -u ${UID} -g ${GROUP} -G sudo ${USER}
RUN chown -R ${USER}:${GROUP} ${HOME}

USER ${USER}
WORKDIR ${HOME}

ENTRYPOINT ["tini","--"]
CMD ["bash"]
