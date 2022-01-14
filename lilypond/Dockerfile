FROM ubuntu
LABEL maintainer "Vijay Lulla <vijaylulla@gmail.com>"

## Build: docker build -t lilypond .
## Run  : docker run --rm -it --name lilypond lilypond
ARG DEBIAN_FRONTEND=noninteractive

## ENV DISPLAY :0
RUN apt-get update -qq -y && apt-get install -y --no-install-recommends build-essential vim-tiny texlive-base
RUN apt-get install -y perl guile-3.0 guile-3.0-dev guile-2.2 guile-2.2-dev python3 flex bison ghostscript imagemagick zip rsync dblatex libpango1.0-dev \
  fonts-texgyre tidy extractpdfmark texlive-metapost curl fontforge texlive-lang-cyrillic gettext texinfo texi2html install-info texlive-xetex

RUN cd && curl -SL -O https://lilypond.org/download/source/v2.23/lilypond-2.23.5.tar.gz && tar xf lilypond-2.23.5.tar.gz 
RUN cd && cd lilypond-2.23.5 && ./autogen.sh --noconfigure && mkdir -p build && cd build && ../configure && make -j 4 all && make test
RUN cd && cd lilypond-2.23.5/build && make install
RUN cd && rm -rf lilypond-2.23.5 lilypond-2.23.5.tar.gz

RUN apt-get install -y iproute2 net-tools iputils-ping

ARG GID=
ARG UID=
ARG USER=
ARG GROUP=
ARG HOME=/home/${USER}
RUN groupadd -g ${GID} ${GROUP} && useradd -m -u ${UID} -g ${GROUP} -G sudo ${USER}
RUN chown -R ${USER}:${GROUP} ${HOME}

USER ${USER}
WORKDIR ${HOME}

CMD ["bash"]
