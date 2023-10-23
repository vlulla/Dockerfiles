## FROM ubuntu:jammy
FROM debian:testing

ENV DEBIAN_FRONTEND noninteractive

ARG UNAME=vlulla
ARG UID=1000
ARG GID=1000

RUN apt-get update -y && apt-get install -y --no-install-recommends apt-utils
RUN apt-get install -y texlive-bibtex-extra texlive-xetex \
      texlive-latex-extra texlive-latex-recommended \
      latexmk biber pandoc make

RUN groupadd --gid $GID $UNAME
RUN useradd -m --gid $GID --uid $UID -d /home/$UNAME $UNAME

USER $UNAME
WORKDIR /home/$UNAME

CMD [ "bash" ]
