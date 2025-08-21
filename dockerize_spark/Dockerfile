# syntax=docker/dockerfile:1
ARG VERSION=trixie
FROM debian:${VERSION}
LABEL maintainer "Vijay Lulla <vijaylulla@gmail.com>"

SHELL ["/bin/bash", "-o", "pipefail", "-c"]

ARG DEBIAN_FRONTEND=noninteractive

ARG GID=${GID:-1010}
ARG UID=${UID:-1010}

RUN apt-get update -qq -y && apt-get install -y --no-install-recommends default-jdk curl procps iputils-ping
## RUN apt-get install -y curl iproute2 net-tools iputils-ping
## echo $(dirname $(update-alternatives --list java))

ARG USER=${USER:-usr}
ARG GROUP=${GROUP:-grp}
ARG HOME=/home/${USER}

RUN <<EOT
set -ex
groupadd -g ${GID} ${GROUP} && useradd -m -u ${UID} -g ${GROUP} -G sudo ${USER}
chown -R ${USER}:${GROUP} ${HOME}
EOT

USER ${USER}
RUN mkdir -p ${HOME}/code/spark 
WORKDIR ${HOME}/code/spark
RUN <<EOT
set -ex
curl -SL -O https://dlcdn.apache.org/spark/spark-4.0.0/spark-4.0.0-bin-hadoop3.tgz && tar xf spark-4.0.0-bin-hadoop3.tgz && rm -rf spark-4.0.0-bin-hadoop3.tgz
curl -SL -O https://dlcdn.apache.org/hadoop/common/hadoop-3.4.1/hadoop-3.4.1.tar.gz && tar xf hadoop-3.4.1.tar.gz && rm -rf hadoop-3.4.1.tar.gz
EOT

## bash $ java -XshowSettings:properties --version 2>&1 | awk '/java.home/' | awk '{print $3}' # for JAVA_HOME
ENV JAVA_HOME="/usr/lib/jvm/java-21-openjdk-amd64"
ENV HADOOP_HOME="${HOME}/code/spark/hadoop-3.4.1"
ENV SPARK_HOME="${HOME}/code/spark/spark-4.0.0-bin-hadoop3"
ENV PATH="${PATH:+${PATH}:}${HADOOP_HOME}/bin:${SPARK_HOME}/bin:${SPARK_HOME}/sbin"
ENV LD_LIBRARY_PATH="${LD_LIBRARY_PATH:+${LD_LIBRARY_PATH}:}${HADOOP_HOME}/lib/native"

WORKDIR ${HOME}

CMD ["bash"]
