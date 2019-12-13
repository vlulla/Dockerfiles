FROM rocker/geospatial:3.6.1 AS builder
LABEL maintainer="Vijay Lulla vijaylulla@gmail.com"

RUN install2.r --error devtools \
  reshape rasterVis pROC gbm mda survival biomod2 \
  randomForest PresenceAbsence dismo caret ENMeval \
  doParallel earth checkmate maxnet ade4 \
  Hmisc gam ecospat latticeExtra hexbin \
  ModelMetrics recipes Formula plotmo TeachingDemos \
  glmnet acepack htmlTable ape adehabitatHR \
  adehabitatMA ecodist rms MigClim vegan \
  poibin snowfall gower ipred timeDate \
  prodlim lava numDeriv SQUAREM shape \
  plotrix adehabitatLT CircStats filehash SparseM \
  quantreg polspline multcomp mvtnorm TH.data \
  sandwich MatrixModels SDMTools permute snow tm

RUN wget https://cran.r-project.org/src/contrib/Archive/maxent/maxent_1.3.3.1.tar.gz \
  && tar xf maxent_1.3.3.1.tar.gz \
  && cd maxent \
  && R CMD INSTALL .

RUN git clone https://github.com/biomodhub/biomod2 \
  && cd biomod2 \
  && R CMD INSTALL .

COPY ./maxent.jar /usr/local/lib/R/site-library/dismo/java/maxent.jar

FROM rocker/r-ver:3.6.1 AS runner
RUN apt-get update \
  && apt-get install -y --no-install-recommends \
    default-jdk \
    lbzip2 \
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
    libpq-dev \
    libproj-dev \
    libprotobuf-dev \
    libnetcdf-dev \
    libsqlite3-dev \
    libssl-dev \
    libudunits2-dev \
    netcdf-bin \
    postgis \
    protobuf-compiler \
    sqlite3 \
    tk-dev \
    unixodbc-dev

COPY --from=builder /usr/local/bin/ /usr/local/bin/
COPY --from=builder /usr/local/lib/ /usr/local/lib/
COPY --from=builder /usr/local/include/ /usr/local/include/

ENV USER ${USER:-vlulla}

RUN useradd -ms /bin/bash ${USER}

USER ${USER}
WORKDIR /home/${USER}

CMD ["bash"]
