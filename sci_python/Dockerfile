FROM continuumio/anaconda3

LABEL MAINTAINER="Vijay Lulla <vijaylulla@gmail.com>"

RUN apt-get update -qq -y \
   && apt-get --assume-yes install spatialite-bin

## RUN conda update --all -y
RUN pip install hypothesis geopandas

CMD [ "/bin/bash" ]

