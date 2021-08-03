## FROM rocker/r-base
FROM r-base

LABEL maintainer "Vijay Lulla <vijaylulla@gmail.com>"

## Buid with command: docker build -t r_geospatial .
##
## Run with command: docker run --rm -it --name geospatialR r_geospatial

## ENV DISPLAY :0
RUN apt-get update -qq -y \
   && apt-get install -t unstable -y --no-install-recommends \
     bwidget \
     default-jdk \
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

RUN  install2.r --error --deps TRUE --skipinstalled sf sp RSQLite \
  && install2.r --error --deps TRUE --skipinstalled jsonlite sqldf \
  && install.r  --error --deps TRUE --skipinstalled BiocManager \
  && installBioc.r graph EBImage \
  && install2.r --error --deps TRUE --skipinstalled igraph rbenchmark stringi stringr \
  && install2.r --error --deps TRUE --skipinstalled rgdal rgeos spdep \
  && install2.r --error --deps TRUE --skipinstalled ncdf4 proj4 raster rasterVis \
  && install2.r --error --deps TRUE --skipinstalled ggrepel devtools pbapply \
  && install2.r --error --deps TRUE --skipinstalled --repos "http://packages.ropensci.org" rnaturalearth rnaturalearthdata rnaturalearthhires \
  && install2.r --error --deps TRUE --skipinstalled leaflet lidR mapdata maptools mapview RNetCDF geonames tmap gstat \
  && install2.r --error --deps TRUE --skipinstalled data.table caret classInt e1071 gbm glmnet nnet xgboost deldir simstudy

## Add r user
ENV RUSER=r
ENV RGROUP=r
ENV HOME=/home/${RUSER}
RUN groupadd -r ${RGROUP} && useradd -r -m -g ${RGROUP} -G sudo,${RGROUP} ${RUSER}
RUN chown -R ${RUSER}:${RGROUP} ${HOME}

WORKDIR ${HOME}
USER ${RUSER}

CMD ["bash"]
