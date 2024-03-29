# syntax=docker/dockerfile:1
FROM --platform=linux/amd64 ubuntu:jammy

SHELL ["/bin/bash", "-o", "pipefail", "-c"]

## see https://github.com/conda-forge/miniforge-images/blob/master/ubuntu/Dockerfile
ARG VERSION=latest
ARG MINIFORGE_NAME=Miniforge3
## ARG MINIFORGE_VERSION=23.1.0-4
ARG MINIFORGE_VERSION=23.3.1-1
LABEL maintainer "Vijay Lulla <vijaylulla@gmail.com>"

ENV CONDA_DIR=/opt/conda DEBIAN_FRONTEND=noninteractive LANG=C.UTF-8 LC_ALL=C.UTF-8
ENV PATH=${CONDA_DIR}/bin:${PATH}

RUN <<EOT
set -ex
apt-get update -qq --yes --fix-missing
apt-get install --yes --auto-remove --no-install-recommends build-essential wget bzip2 ca-certificates git tini
apt-get autoclean && rm -rf /var/lib/apt/lists/*
wget --no-hsts --quiet https://github.com/conda-forge/miniforge/releases/download/${MINIFORGE_VERSION}/${MINIFORGE_NAME}-${MINIFORGE_VERSION}-Linux-$(uname -m).sh -O /tmp/miniforge.sh
/bin/bash /tmp/miniforge.sh -b -p ${CONDA_DIR}
rm /tmp/miniforge.sh
conda clean --tarballs --index-cache --packages --yes
find ${CONDA_DIR} -follow -type f -name '*.a' -delete
find ${CONDA_DIR} -follow -type f -name '*.pyc' -delete
echo ". ${CONDA_DIR}/etc/profile.d/conda.sh && conda activate base" >> /etc/skel.bashrc
echo ". ${CONDA_DIR}/etc/profile.d/conda.sh && conda activate base" >> ~/.bashrc
EOT

RUN <<EOT
set -ex
apt-get update -qq --yes
apt-get install --yes --no-install-recommends software-properties-common dirmngr
wget -qO- https://cloud.r-project.org/bin/linux/ubuntu/marutter_pubkey.asc | tee -a /etc/apt/trusted.gpg.d/cran_ubuntu_key.asc
printf "deb https://cloud.r-project.org/bin/linux/ubuntu $(lsb_release -cs)-cran40/" > /etc/apt/sources.list.d/R.list
apt-get update -qq --yes
apt-get install --yes r-base r-base-dev cmake libssl-dev libopenblas-base libatlas3-base libcurl4-openssl-dev libgit2-dev zlib1g-dev libharfbuzz-dev libfontconfig1-dev libfribidi-dev libxml2-dev libfreetype6-dev libpng-dev libtiff5-dev libjpeg-dev libudunits2-dev libpq-dev libgeos-dev libgdal-dev
## echo "options(Ncpus = parallel::detectCores()-2,mc.cores=parallel::detectCores()-2)" >> ~/.Rprofile
sed -i.orig -E -e 's/options\(repos = r\)/options(Ncpus = parallel::detectCores()-2, mc.cores = parallel::detectCores()-2, repos = r)/g' /etc/R/Rprofile.site
Rscript -e "update.packages(checkBuilt=TRUE,dependencies=TRUE,ask=FALSE)"
Rscript -e "install.packages(c('remotes','reticulate','ggplot2','dplyr','purrr','tidyverse'),dependencies=TRUE,ask=FALSE)"
EOT

RUN <<EOT
set -ex
pip install --no-cache-dir nevergrad rpy2
Rscript -e "remotes::install_github('facebookexperimental/Robyn/R')"
EOT

ENV R_LIBS_USER="${R_LIBS_USER:+${R_LIBS_USER}:}/usr/local/lib/R/site-library:/usr/local/library/R/"
ENV RETICULATE_PYTHON="${CONDA_PYTHON_EXE:-/opt/conda/bin/python}"

ARG UID=${UID:-1000}
ARG USR=${USR:-usr}
ARG GID=${GID:-1000}
ARG GRP=${GRP:-grp}

RUN <<EOT
set -ex
addgroup --gid ${GID} ${GRP}
adduser --uid ${UID} --ingroup ${GRP} ${USR}
EOT

WORKDIR /home/${USR}
USER ${USR}
RUN echo ". ${CONDA_DIR}/etc/profile.d/conda.sh && conda activate base" >> ~/.bashrc

ENTRYPOINT ["tini","--"]
CMD ["/bin/bash"]

