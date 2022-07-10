FROM ubuntu:jammy

LABEL maintainer "Vijay Lulla <vijaylulla@gmail.com>"

## Buid with command: docker build -t r_geospatial .
##
## Run with command: docker run --rm -it --hostname R-geospatial --name geospatialR --net mynet r_geospatial

## ENV DISPLAY :0
ENV DEBIAN_FRONTEND=noninteractive
ENV TZ UTC

RUN apt-get update -qq --yes \
   && apt-get install --yes --no-install-recommends software-properties-common dirmngr wget \
   && wget -qO- https://cloud.r-project.org/bin/linux/ubuntu/marutter_pubkey.asc | tee -a /etc/apt/trusted.gpg.d/cran_ubuntu_key.asc \
   && echo "deb https://cloud.r-project.org/bin/linux/ubuntu $(lsb_release -cs)-cran40/" | tee -a /etc/apt/sources.list.d/R.list \
   && apt-get update \
   && apt-get install -y --no-install-recommends libcurl4-openssl-dev libssl-dev libxml2-dev libudunits2-dev libopenblas-base libatlas3-base libgdal-dev libfftw3-dev gdal-bin libgit2-dev \
     r-base r-base-dev r-recommended r-cran-docopt littler \
  && ln -s /usr/lib/R/site-library/littler/examples/install.r /usr/local/bin/install.r \
  && ln -s /usr/lib/R/site-library/littler/examples/install2.r /usr/local/bin/install2.r \
  && ln -s /usr/lib/R/site-library/littler/examples/installGithub.r /usr/local/bin/installGithub.r \
  && ln -s /usr/lib/R/site-library/littler/examples/installBioc.r /usr/local/bin/installBioc.r \
  && rm -rf /tmp/downloaded_packages/ /tmp/*.rds \
  && rm -rf /var/lib/apt/lists/*

RUN sed -i -e '/options(repos = r)/ s_(\([^)]\+\))_(\1, Ncpus = parallel::detectCores() - 2)_g' /usr/lib/R/etc/Rprofile.site

RUN install2.r --error --skipinstalled BiocManager && installBioc.r BiocVersion \
 && install2.r --error --skipinstalled devtools Rcpp RcppArmadillo ggplot2 rbenchmark \
 && installBioc.r graph EBImage \
 && install2.r --error --skipinstalled jsonlite stringi stringr data.table sqldf igraph simstudy ggrepel pbapply \
          caret e1071 gbm glmnet nnet xgboost randomForest arrow classInt fst RSQLite rgdal sf sp raster terra lidR

RUN install2.r --error --skipinstalled rgeos \
 && install2.r --error --deps TRUE --skipinstalled --repos "http://packages.ropensci.org" rnaturalearth rnaturalearthdata rnaturalearthhires


## Add a regular user
ARG UID=1001
ARG GID=1001
ARG RUSER=r
ARG RGROUP=r
ARG HOME=/home/${RUSER}
RUN groupadd -g ${GID} ${RGROUP} && useradd -m -u ${UID} -g ${RGROUP} -G sudo,${RGROUP} ${RUSER}
RUN chown -R ${RUSER}:${RGROUP} ${HOME}

WORKDIR ${HOME}
USER ${RUSER}

CMD ["bash"]
