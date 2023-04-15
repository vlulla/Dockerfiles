FROM debian:testing
LABEL maintainer "Vijay Lulla <vijaylulla@gmail.com>"

ARG GID=
ARG UID=

RUN apt-get update -qq -y && apt-get install -y --no-install-recommends default-jdk curl procps iputils-ping
## RUN apt-get install -y curl iproute2 net-tools iputils-ping
## echo $(dirname $(update-alternatives --list java))

ARG USER=
ARG GROUP=
ARG HOME=/home/${USER}
RUN groupadd -g ${GID} ${GROUP} && useradd -m -u ${UID} -g ${GROUP} -G sudo ${USER}
RUN chown -R ${USER}:${GROUP} ${HOME}

USER ${USER}
RUN mkdir -p ${HOME}/code/spark 
WORKDIR ${HOME}/code/spark
RUN curl -SL -O https://dlcdn.apache.org/spark/spark-3.4.0/spark-3.4.0-bin-hadoop3-scala2.13.tgz && tar xf spark-3.4.0-bin-hadoop3-scala2.13.tgz && rm -rf spark-3.4.0-bin-hadoop3-scala2.13.tgz
RUN curl -SL -O https://dlcdn.apache.org/hadoop/common/hadoop-3.3.5/hadoop-3.3.5.tar.gz && tar xf hadoop-3.3.5.tar.gz && rm -rf hadoop-3.3.5.tar.gz

ENV JAVA_HOME="/usr/lib/jvm/java-17-openjdk-amd64"
ENV HADOOP_HOME="${HOME}/code/spark/hadoop-3.3.5"
ENV SPARK_HOME="${HOME}/code/spark/spark-3.4.0-bin-hadoop3-scala2.13"
ENV PATH="${PATH:+${PATH}:}${HADOOP_HOME}/bin:${SPARK_HOME}/bin:${SPARK_HOME}/sbin"
ENV LD_LIBRARY_PATH="${LD_LIBRARY_PATH:+${LD_LIBRARY_PATH}:}${HADOOP_HOME}/lib/native"

WORKDIR ${HOME}

CMD ["bash"]
