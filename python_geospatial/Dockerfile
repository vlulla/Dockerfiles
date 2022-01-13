FROM debian

LABEL maintainer "Vijay Lulla <vijaylulla@gmail.com>"

## Buid with command: docker build -t python_geospatial .
##
## Run with command: docker run --rm -ti --hostname geospatialPython --name geospatialPython --net mynet python_geospatial

## ENV DISPLAY :0
RUN apt-get update -qq -y \
   && apt-get install -y --no-install-recommends build-essential apt-utils
RUN apt-get install -y libgeos-dev libxml2 libxml2-dev pkg-config curl wget jq 
RUN apt-get install -y python3 python3-hypothesis python3-pandas python3-sklearn ipython3 sqlite3 python3-matplotlib \
      python3-rasterio python3-csvkit python3-gdal python3-geopandas python3-tables python3-sqlalchemy  \
      python3-sqlalchemy-utils python3-dateutil python3-geoip2 python3-cartopy python3-fastkml python3-fiona \
      python3-shapely python3-geojson python3-geopy python3-geomet python3-gmplot python3-grib \
      python3-h5py python3-hdf4 python3-imageio python3-jinja2 python3-mapproxy \
      python3-markdown2 python3-opencv python3-pil python3-pip python3-pkgconfig python3-protobuf python3-yaml \
      python3-dask python3-distributed python3-boto3
RUN pip install pyarrow

## RUN apt-get install -y python3-boto3 python3-azure-cli 
RUN apt-get install -y gdal-bin libgdal-dev
RUN pip install GDAL

RUN apt-get install -y spatialite-bin python3-zarr
ARG UID=
ARG GID=

## Add a user
ARG PYTHONUSER=vijay
ARG PYTHONGROUP=vijay
ARG HOME=/home/${PYTHONUSER}
RUN groupadd -g ${GID} ${PYTHONGROUP} && useradd -m -u ${UID} -g ${PYTHONGROUP} -G sudo,${PYTHONGROUP} ${PYTHONUSER}
RUN chown -R ${PYTHONUSER}:${PYTHONGROUP} ${HOME}

WORKDIR ${HOME}
USER ${PYTHONUSER}

CMD ["bash"]
