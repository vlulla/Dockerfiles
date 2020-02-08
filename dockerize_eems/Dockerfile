FROM alpine
LABEL maintainer "Vijay Lulla <vijaylulla@gmail.com>"

ARG GROUP_ID
ARG USER_ID

## Buid with command: docker build --build-arg USER_ID=$(id -u) --build-arg GROUP_ID=$(id -g) -f Dockerfile -t eems .
##
## Run with command: docker run --rm -it --name eems_container eems

RUN apk update && apk add --no-cache --virtual .build-deps \
     wget \
     git \
     make \
     g++ \
     eigen-dev \
     boost-dev

ENV HOME /home/u
WORKDIR ${HOME}

RUN git clone https://github.com/dipetkov/eems \
  && cd eems/runeems_sats/src \
  && sed -i.orig -e '/^EIGEN_INC/s@=.\+$@= /usr/include/eigen3@' -e '/^BOOST_LIB/s@=.\+$@= /usr/lib@' -e '/^BOOST_INC/s@=.\+$@= /usr/include@' Makefile \
  && make linux

RUN apk del .build-deps

## Add user
## RUN groupadd -r u && useradd -r -m -g u -G sudo,u u && chown -R u:u ${HOME}
RUN addgroup -S -g ${GROUP_ID} u && adduser -D -G u  u && chown -R u:u ${HOME}
USER u

ENV PATH="${PATH}:${HOME}/eems/runeems_sats/src"

CMD ["ash"]
