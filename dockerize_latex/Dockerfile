FROM ubuntu:18.04

ENV DEBIAN_FRONTEND noninteractive

RUN apt-get update -y && apt-get install -y --no-install-recommends apt-utils
RUN apt-get install -y texlive-bibtex-extra texlive-xetex \
      texlive-latex-extra texlive-latex-recommended \
      latexmk biber make

RUN groupadd --gid {{GID}} {{GROUPNAME}} 
RUN useradd -m --gid {{GID}} --uid {{UID}} -d /home/{{USERNAME}} {{USERNAME}}

USER {{USERNAME}}:{{GROUPNAME}}
WORKDIR /home/{{USERNAME}}

CMD [ "bash" ]
