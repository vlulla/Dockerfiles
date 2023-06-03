# syntax=docker/dockerfile:1
ARG VERSION=${VERSION:-1.4.3}
FROM mambaorg/micromamba:${VERSION}

SHELL [ "/bin/bash", "-eo","pipefail", "-c"]

LABEL MAINTAINER="Vijay Lulla <vijaylulla@gmail.com>"
ENV DEBIAN_FRONTEND=noninteractive

RUN <<EOT
micromamba install --yes --name base --channel conda-forge hypothesis geopandas
micromamba clean --all --yes
EOT

CMD [ "/bin/bash" ]

