## FROM debian
FROM ubuntu

LABEL maintainer "Vijay Lulla <vijaylulla@gmail.com>"

## Buid with command: docker build -t spatialitetools .
##
## Run with command: docker run --rm -ti --hostname spatialitetools --name spatialitetools --net mynet spatialitetools

## ENV DISPLAY :0
ARG DEBIAN_FRONTEND=noninteractive
ENV TZ=US/Indianapolis
RUN apt-get update -qq -y \
   && apt-get install -y --no-install-recommends build-essential apt-utils tzdata
RUN apt-get install -y libgeos-dev libxml2 libxml2-dev pkg-config curl wget jq gawk sed git gcc gdal-bin libgdal-dev postgis 



RUN cd && git clone https://git.osgeo.org/gitea/rttopo/librttopo.git && cd librttopo && ./autogen.sh && ./configure \
  && make -j 4 && make check && make install
RUN cd && curl -SL -O http://www.gaia-gis.it/gaia-sins/readosm-1.1.0a.tar.gz && tar xf readosm-1.1.0a.tar.gz \
  && cd readosm-1.1.0a && ./configure && make -j 4 && make install
RUN cd && curl -SL -O http://www.gaia-gis.it/gaia-sins/libspatialite-5.0.1.tar.gz && tar xf libspatialite-5.0.1.tar.gz \
  && cd libspatialite-5.0.1 && ./configure --disable-rttopo && make -j 4 && make install
RUN cd && curl -SL -O http://www.gaia-gis.it/gaia-sins/spatialite-tools-5.0.0.tar.gz && tar xf spatialite-tools-5.0.0.tar.gz \
  && cd spatialite-tools-5.0.0 && ./configure --disable-rttopo && make -j 4 && make install

ENV LD_LIBRARY_PATH=/usr/local/lib

ARG UID=
ARG GID=

## Add a user
ARG SWUSR=
ARG SWGRP=
ARG HOME=/home/${SWUSR}

RUN groupadd -g ${GID} ${SWGRP} && useradd -m -u ${UID} -g ${SWGRP} -G sudo,${SWGRP} ${SWUSR}
RUN chown -R ${SWUSR}:${SWGRP} ${HOME}

WORKDIR ${HOME}
USER ${SWUSR}

CMD ["bash"]
