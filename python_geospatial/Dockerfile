# syntax=docker/dockerfile:1
FROM debian:testing

## Buid with command: docker build -t python_geospatial .
##
## Run with command: docker run --rm -it --init --hostname=geospatialPython --name=geospatialPython --net=mynet --read-only python_geospatial

SHELL ["/bin/bash", "-o", "pipefail", "-c"]

ARG DEBIAN_FRONTEND=noninteractive

## ## ENV DISPLAY :0
RUN <<EOT
  set -ex
  apt-get update -qq --yes
  apt-get install --yes --no-install-recommends build-essential apt-utils tini libgeos-dev libxml2 libxml2-dev pkg-config curl wget jq parallel python3 ca-certificates
  rm -rf /var/lib/apt/lists/*
EOT

ARG UID=${UID:-1010}
ARG GID=${GID:-1010}

## Add a user
ARG PYUSR=${PYUSR:-usr}
ARG PYGRP=${PYGRP:-grp}
ARG HOME=/home/${PYUSR}

RUN <<EOT
set -ex
groupadd -g ${GID} ${PYGRP} && useradd -l -m -u ${UID} -g ${PYGRP} -G sudo,${PYGRP} ${PYUSR}
chown -R ${PYUSR}:${PYGRP} ${HOME}
EOT

WORKDIR ${HOME}
USER ${PYUSR}

RUN <<EOT
    set -ex
    mkdir -p ${HOME}/{.local,micromamba} && pushd ${HOME}/.local
    curl -Ls https://micro.mamba.pm/api/micromamba/linux-64/latest | tar -xvj bin/micromamba
    ${HOME}/.local/bin/micromamba shell init -s bash -p ${HOME}/micromamba
    MAMBA_ROOT_PREFIX=${HOME}/micromamba ${HOME}/.local/bin/micromamba create --channel conda-forge --name geo --yes geopandas xarray rasterio dask distributed pyarrow pygeos polars
EOT

ENTRYPOINT ["tini","--"]
CMD ["bash"]
