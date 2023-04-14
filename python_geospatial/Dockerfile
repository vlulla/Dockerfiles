# syntax=docker/dockerfile:1
FROM debian:testing

## Buid with command: docker build -t python_geospatial .
##
## Run with command: docker run --rm -it --init --hostname=geospatialPython --name=geospatialPython --net=mynet --read-only python_geospatial

SHELL ["/bin/bash", "-o", "pipefail", "-c"]

ARG DEBIAN_FRONTEND=noninteractive

## ## ENV DISPLAY :0
RUN <<EOT bash
  apt-get update -qq --yes
  apt-get install --yes --no-install-recommends build-essential apt-utils tini libgeos-dev libxml2 libxml2-dev pkg-config \
            curl wget jq parallel python3 ca-certificates
  rm -rf /var/lib/apt/lists/*
EOT

ARG UID=
ARG GID=

## Add a user
ARG PYUSR=
ARG PYGRP=
ARG HOME=/home/${PYUSR}
RUN groupadd -g ${GID} ${PYGRP} && useradd -l -m -u ${UID} -g ${PYGRP} -G sudo,${PYGRP} ${PYUSR}
RUN chown -R ${PYUSR}:${PYGRP} ${HOME}

WORKDIR ${HOME}
USER ${PYUSR}

RUN <<EOT
    wget --quiet "https://github.com/conda-forge/miniforge/releases/latest/download/Mambaforge-$(uname)-$(uname -m).sh"
    bash "./Mambaforge-$(uname)-$(uname -m).sh" -b -p ~/mambaforge && rm -rf "~/Mambaforge-$(uname)-$(uname -m).sh"
    ~/mambaforge/bin/mamba init bash
    ~/mambaforge/bin/mamba install --name base --yes geopandas xarray rasterio dask distributed pyarrow pygeos
EOT

ENTRYPOINT ["tini","--"]
CMD ["bash"]
