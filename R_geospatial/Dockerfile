# syntax=docker/dockerfile:1
ARG VERSION=noble
FROM ubuntu:${VERSION}

## Buid with command: docker build -t r_geospatial .
##
## Run with command: docker run --rm -it --init --hostname=R-geospatial --name=geospatialR --net=mynet --read-only r_geospatial

## ENV DISPLAY :0
ARG DEBIAN_FRONTEND=noninteractive
ENV TZ UTC

SHELL ["/bin/bash", "-eo", "pipefail", "-c"]

RUN <<EOT
   set -ex
   apt-get update -qq --yes
   apt-get install -qq --yes --no-install-recommends software-properties-common dirmngr wget  parallel
   wget -qO- https://cloud.r-project.org/bin/linux/ubuntu/marutter_pubkey.asc | tee -a /etc/apt/trusted.gpg.d/cran_ubuntu_key.asc
   echo "deb https://cloud.r-project.org/bin/linux/ubuntu $(lsb_release -cs)-cran40/" | tee -a /etc/apt/sources.list.d/R.list
   apt-get -qq update
   apt-get install -qq --yes --no-install-recommends cmake libcurl4-openssl-dev libssl-dev libxml2-dev libudunits2-dev libatlas3-base libgdal-dev libfftw3-dev gdal-bin libgit2-dev \
     r-base r-base-dev r-recommended r-cran-docopt littler libfontconfig1-dev libharfbuzz-dev libfribidi-dev
  ln -s /usr/lib/R/site-library/littler/examples/install.r /usr/local/bin/install.r
  ln -s /usr/lib/R/site-library/littler/examples/install2.r /usr/local/bin/install2.r
  ln -s /usr/lib/R/site-library/littler/examples/installGithub.r /usr/local/bin/installGithub.r
  ln -s /usr/lib/R/site-library/littler/examples/installBioc.r /usr/local/bin/installBioc.r
  rm -rf /tmp/downloaded_packages/ /tmp/*.rds
  rm -rf /var/lib/apt/lists/*

  sed -i -e '/options(repos = r)/ s_(\([^)]\+\))_(\1, Ncpus = parallel::detectCores() - 2)_g' /usr/lib/R/etc/Rprofile.site

  install2.r --error --skipinstalled BiocManager && installBioc.r BiocVersion
  install2.r --error --repos "https://packagemanager.posit.co/cran/__linux__/noble/latest" --skipinstalled devtools Rcpp RcppArmadillo ggplot2 rbenchmark httr
  installBioc.r graph EBImage
  install2.r --error --repos "https://packagemanager.posit.co/cran/__linux__/noble/latest" --skipinstalled jsonlite stringi stringr data.table sqldf igraph simstudy ggrepel pbapply \
          caret e1071 gbm glmnet nnet xgboost randomForest arrow classInt fst RSQLite sf sp raster terra lidR
  install2.r --error --repos "https://packagemanager.posit.co/cran/__linux__/noble/latest" --skipinstalled svglite tmap
  install2.r --error --deps TRUE --skipinstalled --repos "http://packages.ropensci.org" rnaturalearth rnaturalearthdata rnaturalearthhires
EOT


## Add a regular user
ARG UID=${UID:-1010}
ARG GID=${GID:-1010}
ARG RUSER=${RUSER:-r}
ARG RGROUP=${RGROUP:-r}
ARG HOME=/home/${RUSER}

RUN <<EOT
set -ex
groupadd -g ${GID} ${RGROUP} && useradd -l -m -u ${UID} -g ${RGROUP} -G sudo,${RGROUP} ${RUSER}
chown -R ${RUSER}:${RGROUP} ${HOME}
EOT

WORKDIR ${HOME}
USER ${RUSER}:${RGROUP}

CMD ["/usr/bin/bash"]
