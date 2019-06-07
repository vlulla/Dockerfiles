FROM rocker/r-base
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
     liblwgeom-dev \
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
   && ln -s /usr/lib/x86_64-linux-gnu/libgeos-3.7.1.so /usr/lib/x86_64-linux-gnu/libgeos.so

## sf, sp, and RSQLite are at the top because they have lots of dependencies!
## simplifies other package installations...especially with --skipinstalled
RUN install2.r --error --deps TRUE --skipinstalled \
     sf \
     sp \
     RSQLite \
     RNetCDF \
     caret \
     classInt \
     e1071 \
     data.table \
     deldir \
     gbm \
     glmnet \
     geonames \
     gstat \
     igraph \
     jsonlite \
     leaflet \
     lidR \
     mapdata \
     maptools \
     mapview \
     nnet \
     ncdf4 \
     proj4 \
     raster \
     rasterVis \
     rbenchmark \
     rgdal \
     rgeos \
     rnaturalearth \
     simstudy \
     ## spatstat \
     spdep \
     sqldf \
     stringi \
     stringr \
     ## tau \
     ## tm \
     tmap \
     xgboost
   ##   xgboost \
   ##   ## from biconductor
   ## && R -e "BiocManager::install('rhdf5')"

ENV HOME /home/r
## Add r user
RUN groupadd -r r && useradd -r -m -g r -G sudo,r r && chown -R r:r ${HOME}

WORKDIR ${HOME}
USER r

CMD ["bash"]
