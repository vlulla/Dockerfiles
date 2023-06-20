# syntax=docker/dockerfile:1
FROM ubuntu:22.04

SHELL ["/bin/bash", "-o", "pipefail", "-c"]

## see https://github.com/conda-forge/miniforge-images/blob/master/ubuntu/Dockerfile
ARG VERSION=latest
ARG MINIFORGE_NAME=Miniforge3
ARG MINIFORGE_VERSION=22.11.1-4
LABEL maintainer "Vijay Lulla <vijaylulla@gmail.com>"

ENV CONDA_DIR=/opt/conda DEBIAN_FRONTEND=noninteractive LANG=C.UTF-8 LC_ALL=C.UTF-8
ENV PATH=${CONDA_DIR}/bin:${PATH}

RUN <<EOT
apt-get update -qq --yes --fix-missing && \
  apt-get install --yes --auto-remove --no-install-recommends build-essential wget bzip2 ca-certificates git tini r-base r-base-dev cmake libssl-dev libopenblas-base libatlas3-base libcurl4-openssl-dev libgit2-dev zlib1g-dev libharfbuzz-dev libfontconfig1-dev libfribidi-dev libxml2-dev libfreetype6-dev libpng-dev libtiff5-dev libjpeg-dev libudunits2-dev libmariadb-dev libpq-dev && \
  apt-get autoclean && rm -rf /var/lib/apt/lists/* && \
  wget --no-hsts --quiet https://github.com/conda-forge/miniforge/releases/download/${MINIFORGE_VERSION}/${MINIFORGE_NAME}-${MINIFORGE_VERSION}-Linux-$(uname -m).sh -O /tmp/miniforge.sh && \
  /bin/bash /tmp/miniforge.sh -b -p ${CONDA_DIR} && \
  rm /tmp/miniforge.sh  && \
  conda clean --tarballs --index-cache --packages --yes && \
  find ${CONDA_DIR} -follow -type f -name '*.a' -delete && \
  find ${CONDA_DIR} -follow -type f -name '*.pyc' -delete && \
  echo ". ${CONDA_DIR}/etc/profile.d/conda.sh && conda activate base" >> /etc/skel.bashrc && \
  echo ". ${CONDA_DIR}/etc/profile.d/conda.sh && conda activate base" >> ~/.bashrc && \
  echo "options(Ncpus = $(nproc)-2,mc.cores=$(nproc)-2)" >> ~/.Rprofile && \
  Rscript -e "update.packages(checkBuilt=TRUE,dependencies=TRUE,ask=FALSE)"
EOT

RUN <<EOT
pip install nevergrad && \
  Rscript -e "install.packages(c('remotes','reticulate','ggplot2','dplyr','purrr'),dependencies=TRUE,ask=FALSE)" && \
  Rscript -e "remotes::install_github('facebookexperimental/Robyn/R')"
EOT


ENTRYPOINT ["tini","--"]
CMD ["/bin/bash"]

