## FROM continuumio/anaconda3
FROM condaforge/mambaforge

LABEL MAINTAINER="Vijay Lulla <vijaylulla@gmail.com>"
ENV DEBIAN_FRONTEND=noninteractive

RUN apt-get update -qq -y \
   && apt-get --assume-yes install spatialite-bin

## RUN conda update --all -y
RUN pip install hypothesis geopandas

CMD [ "/bin/bash" ]

