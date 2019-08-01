FROM ubuntu:18.04
LABEL maintainer "Vijay Lulla <vijaylulla@gmail.com>"

## Buid with command: docker build -t eems .
##
## Run with command: docker run --rm -it --name eems_container eems

RUN apt-get update -qq -y \
   && apt-get install -y --no-install-recommends \
     software-properties-common \
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
RUN groupadd -r u && useradd -r -g u -G sudo,u u && chown -R u:u ${HOME}
USER u

ENV PATH="${PATH}:${HOME}/eems/runeems_sats/src"

CMD ["bash"]
