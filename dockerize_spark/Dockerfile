# syntax=docker/dockerfile:1
FROM debian:testing
LABEL maintainer "Vijay Lulla <vijaylulla@gmail.com>"

SHELL ["/bin/bash", "-o", "pipefail", "-c"]

ARG GID=${GID:-1010}
ARG UID=${UID:-1010}

RUN apt-get update -qq -y && apt-get install -y --no-install-recommends default-jdk curl procps iputils-ping
## RUN apt-get install -y curl iproute2 net-tools iputils-ping
## echo $(dirname $(update-alternatives --list java))

ARG USER=${USER:-usr}
ARG GROUP=${GROUP:-grp}
ARG HOME=/home/${USER}

RUN <<EOT
groupadd -g ${GID} ${GROUP} && useradd -m -u ${UID} -g ${GROUP} -G sudo ${USER}
chown -R ${USER}:${GROUP} ${HOME}
EOT

USER ${USER}
RUN mkdir -p ${HOME}/code/spark 
WORKDIR ${HOME}/code/spark
RUN <<EOT
curl -SL -O https://dlcdn.apache.org/spark/spark-3.5.0/spark-3.5.0-bin-hadoop3-scala2.13.tgz && tar xf spark-3.5.0-bin-hadoop3-scala2.13.tgz && rm -rf spark-3.5.0-bin-hadoop3-scala2.13.tgz
curl -SL -O https://dlcdn.apache.org/hadoop/common/hadoop-3.3.6/hadoop-3.3.6.tar.gz && tar xf hadoop-3.3.6.tar.gz && rm -rf hadoop-3.3.6.tar.gz
EOT

ENV JAVA_HOME="/usr/lib/jvm/java-17-openjdk-amd64"
ENV HADOOP_HOME="${HOME}/code/spark/hadoop-3.3.6"
ENV SPARK_HOME="${HOME}/code/spark/spark-3.5.0-bin-hadoop3-scala2.13"
ENV PATH="${PATH:+${PATH}:}${HADOOP_HOME}/bin:${SPARK_HOME}/bin:${SPARK_HOME}/sbin"
ENV LD_LIBRARY_PATH="${LD_LIBRARY_PATH:+${LD_LIBRARY_PATH}:}${HADOOP_HOME}/lib/native"

WORKDIR ${HOME}

CMD ["bash"]
