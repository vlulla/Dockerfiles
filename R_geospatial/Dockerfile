## FROM rocker/r-base
FROM r-base

LABEL maintainer "Vijay Lulla <vijaylulla@gmail.com>"

## Buid with command: docker build -t r_geospatial .
##
## Run with command: docker run --rm -it --hostname R-geospatial --name geospatialR --net mynet r_geospatial

## SHELL ["/bin/bash", "-o", "pipefail", "-c"]

## ENV DISPLAY :0
RUN apt-get update -qq -y \
   && apt-get install -t unstable -y --no-install-recommends \
     bwidget \
     default-jdk \
     file \
     lbzip2 \
     libcairo2-dev \
     libfftw3-dev \
     libgdal-dev \
     libgeos-dev \
     libgsl0-dev \
     libgl1-mesa-dev \
     libglu1-mesa-dev \
     libhdf4-alt-dev \
     libhdf5-dev \
     libjq-dev \
##      liblwgeom-dev \
     libmagick++-dev \
     libproj-dev \
     libprotobuf-dev \
     libnetcdf-dev \
     libsqlite3-dev \
     libssh2-1-dev \
     libssl-dev \
     libudunits2-dev \
     libv8-dev \
     libxt-dev \
     netcdf-bin \
     protobuf-compiler \
     tk-dev \
     unixodbc-dev \
     zstd libzstd1 libzstd-dev \
     libharfbuzz-dev libfribidi-dev \
   && cd /usr/lib/x86_64-linux-gnu && ln -s libgeos-`pkg-config --modversion geos`.so libgeos.so

## sf, sp, and RSQLite are at the top because they have lots of dependencies!
## simplifies other package installations...especially with --skipinstalled

RUN apt-get install --yes gdal-bin jq iproute2 net-tools iputils-ping cmake cargo

RUN sed -i -e '/options(repos = r)/ s_(\([^)]\+\))_(\1, Ncpus = parallel::detectCores() - 2)_g' /usr/lib/R/etc/Rprofile.site

RUN install2.r --error --skipinstalled BiocManager && installBioc.r BiocVersion
RUN  install2.r --error --deps TRUE --skipinstalled Rcpp devtools RcppArmadillo rbenchmark \
  && installBioc.r graph EBImage \
  && install2.r --error --deps TRUE --skipinstalled jsonlite stringi stringr data.table sqldf igraph simstudy ggrepel pbapply \
            caret e1071 gbm glmnet nnet xgboost randomForest arrow classInt fst RSQLite

RUN install2.r --error --deps TRUE --skipinstalled rgdal rgeos spdep sf sp ncdf4 proj4 raster rasterVis terra gdalcubes lidR
RUN install2.r --error --deps TRUE --skipinstalled --repos "http://packages.ropensci.org" rnaturalearth rnaturalearthdata rnaturalearthhires


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
