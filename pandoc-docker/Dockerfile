# syntax=docker/dockerfile:1
ARG VERSION=latest
FROM pandoc/latex:${VERSION}
ARG VERSION
LABEL maintainer="Vijay Lulla <vijaylulla@gmail.com>"
RUN tlmgr install koma-script
