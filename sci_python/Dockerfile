# syntax=docker/dockerfile:1
ARG VERSION="${VERSION:-noble}"
FROM ubuntu:${VERSION}

SHELL [ "/bin/bash", "-eo","pipefail", "-c"]

LABEL MAINTAINER="Vijay Lulla <vijaylulla@gmail.com>"
ARG DEBIAN_FRONTEND=noninteractive

RUN <<EOT
apt-get update --yes
apt-get upgrade --yes
apt-get install --yes --no-install-recommends curl ca-certificates tar bzip2
EOT

ENV MAMBA_ROOT_PREFIX=${MAMBA_ROOT_PREFIX:-/opt/micromamba}

RUN <<EOT
set -ex
mkdir -p /tmp/mm && cd /tmp/mm
curl -Ls https://micro.mamba.pm/api/micromamba/linux-64/latest | tar -xvj bin/micromamba
mv bin/micromamba /usr/bin/
micromamba create --name scipy --yes --channel conda-forge hypothesis numpy pandas matplotlib scipy sympy ipython numba bottleneck seaborn
micromamba clean --all --yes
rm -rf /tmp/mm
EOT

CMD [ "/bin/bash" ]

