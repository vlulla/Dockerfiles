FROM continuumio/anaconda3

LABEL MAINTAINER="Vijay Lulla <vijaylulla@gmail.com>"

RUN apt-get update -qq -y \
   && apt-get --assume-yes install vim spatialite-tools

## RUN conda update --all -y
RUN pip install hypothesis geopandas

CMD [ "/bin/bash" ]

