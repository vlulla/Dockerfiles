FROM ubuntu:22.04
LABEL maintainer "Vijay Lulla <vijaylulla@gmail.com>"

ENV DEBIAN_FRONTEND noninteractive

ARG GROUP_ID=${GROUP_ID:-1001}
ARG USER_ID=${USER_ID:-1001}

## Buid with command: docker build --build-arg USER_ID=$(id -u) --build-arg GROUP_ID=$(id -g) -t eems .
##
## Run with command: docker run --rm -it --name eems_container eems

RUN apt-get update -qq -y \
   && apt-get install -y --no-install-recommends \
     software-properties-common \
     tini \
     wget \
     git \
     make \
     g++ \
     libeigen3-dev \
     libboost-all-dev \
   && rm -rf /var/lib/apt/lists/*

ENV HOME /home/u
WORKDIR ${HOME}

RUN git clone https://github.com/dipetkov/eems \
  && cd eems/runeems_sats/src \
  && sed -i.orig -e '/^EIGEN_INC/s@=.\+$@= /usr/include/eigen3@' -e '/^BOOST_LIB/s@=.\+$@= /usr/lib@' -e '/^BOOST_INC/s@=.\+$@= /usr/include@' Makefile \
  && make linux

## Add user
## RUN groupadd -r u && useradd -r -m -g u -G sudo,u u && chown -R u:u ${HOME}
RUN groupadd -r -g ${GROUP_ID} u && useradd -r -g u --uid ${USER_ID} -G sudo,u u && chown -R u:u ${HOME}
USER u

ENV PATH="${PATH}:${HOME}/eems/runeems_sats/src"

ENTRYPOINT ["tini", "--", "bash"]
