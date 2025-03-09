# syntax=docker/dockerfile:1
FROM ubuntu:jammy

LABEL maintainer "Vijay Lulla <vijaylulla@gmail.com>"

SHELL ["/bin/bash", "-c"]

## Buid with command: docker build -t spatialitetools .
##
## Run with command: docker run --rm -ti --hostname spatialitetools --name spatialitetools --net mynet spatialitetools

## ENV DISPLAY :0
ARG DEBIAN_FRONTEND=noninteractive
ENV TZ=US/Indianapolis
RUN <<EOT
set -ex
apt-get update -qq -y
apt-get install -y --no-install-recommends build-essential apt-utils tzdata
apt-get install -y libgeos-dev libxml2 libxml2-dev pkg-config curl wget jq gawk sed git gcc gdal-bin libgdal-dev postgis
EOT

RUN apt-get install -y iproute2 net-tools iputils-ping

RUN <<EOT
set -ex
pushd /tmp
git clone https://gitlab.com/rttopo/rttopo.git
cd rttopo
./autogen.sh
./configure
make -j $(( $(nproc) - 2))
make check
make install
popd
EOT

RUN <<EOT
set -ex
pushd /tmp
curl -SL -O http://www.gaia-gis.it/gaia-sins/readosm-1.1.0a.tar.gz
tar xf readosm-1.1.0a.tar.gz
cd readosm-1.1.0a
./configure
make -j $(( $(nproc) - 2))
make install
popd
EOT

RUN <<EOT
set -ex
pushd /tmp
curl -SL -O https://www.gaia-gis.it/gaia-sins/freexl-2.0.0.tar.gz
tar xf freexl-2.0.0.tar.gz
cd freexl-2.0.0
./configure
make -j $(( $(nproc) - 2))
make install
popd
EOT

RUN <<EOT
set -ex
pushd /tmp
curl -SL -O https://www.gaia-gis.it/gaia-sins/libspatialite-sources/libspatialite-5.1.0.tar.gz
tar xf libspatialite-5.1.0.tar.gz
cd libspatialite-5.1.0
./configure --disable-rttopo --enable-freexl=no --disable-geos3110
make -j $(( $(nproc) - 2))
make install
popd
EOT

RUN <<EOT
set -ex
pushd /tmp
curl -SL -O https://www.gaia-gis.it/gaia-sins/spatialite-tools-sources/spatialite-tools-5.1.0a.tar.gz
tar xf spatialite-tools-5.1.0a.tar.gz
cd spatialite-tools-5.1.0a
./configure --disable-rttopo
make -j $(( $(nproc) - 2))
make install
popd
EOT

ENV LD_LIBRARY_PATH=/usr/local/lib

ARG UID=${UID:-1000}
ARG GID=${GID:-1000}

## Add a user
ARG USR=${USR:-usr}
ARG GRP=${GRP:-grp}
ARG HOME=/home/${USR}

RUN groupadd -g ${GID} ${GRP} && useradd -m -u ${UID} -g ${GRP} -G ${GRP} ${USR}
RUN chown -R ${USR}:${GRP} ${HOME}

WORKDIR ${HOME}
USER ${USR}

CMD ["bash"]
