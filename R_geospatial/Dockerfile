FROM r-base
MAINTAINER "Vijay Lulla" vijaylulla@gmail.com

RUN apt-get update -qq -y \
   && apt-get install -t unstable -y --no-install-recommends \
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
     libssl-dev \
     libudunits2-dev \
     libv8-dev \
     libxt-dev \
     netcdf-bin \
     protobuf-compiler \
     tk-dev \
     unixodbc-dev \
   && ln -s /usr/lib/x86_64-linux-gnu/libgeos-3.7.1.so /usr/lib/x86_64-linux-gnu/libgeos.so

RUN install2.r --error --deps TRUE \
     RColorBrewer \
     RandomFields \
     RNetCDF \
     caret \
     censusapi \
     classInt \
     e1071 \
     data.table \
     deldir \
     gbm \
     glmnet \
     geonames \
     gstat \
     hdf5r \
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
     sf \
     sp \
     raster \
     rasterVis \
     rgbif \
     rgdal \
     rgeos \
     rlas \
     rnaturalearth \
     RSQLite \
     simstudy \
     spacetime \
     spatstat \
     spdep \
     sqldf \
     stars \
     stringi \
     stringr \
     tau \
     tm \
     tmap \
     geoR \
     geosphere \
     xgboost \
     ## from biconductor
   && R -e "BiocManager::install('rhdf5')"

CMD ["bash"]
