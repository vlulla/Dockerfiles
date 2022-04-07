FROM rocker/geospatial AS builder
LABEL maintainer="Vijay Lulla vijaylulla@gmail.com"

RUN install2.r --error devtools \
      CircStats ENMeval Formula Hmisc MatrixModels ModelMetrics PresenceAbsence SQUAREM \
      SparseM TH.data TeachingDemos acepack ade4 adehabitatHR adehabitatLT adehabitatMA \
      ape biomod2 caret checkmate dismo doParallel earth ecodist filehash gam gbm glmnet \
      gower hexbin htmlTable ipred latticeExtra lava maxnet mda multcomp mvtnorm numDeriv \
      pROC permute plotmo plotrix poibin polspline prodlim quantreg randomForest rasterVis \
      recipes reshape rms sandwich shape snow snowfall survival timeDate tm vegan patchwork ecospat

## RUN install2.r --error MigClim
RUN git clone https://github.com/cran/MigClim && cd MigClim && R CMD INSTALL .
RUN install2.r --error R.utils
## RUN install2.r --error SDMTools
RUN git clone https://github.com/cran/SDMTools && cd SDMTools && R CMD INSTALL .

RUN install2.r --error BiocManager && R --no-save --no-restore -e "BiocManager::install(c('graph','Rgraphviz','gRbase'))"
RUN install2.r --error bnspatial

## RUN wget https://cran.r-project.org/src/contrib/Archive/maxent/maxent_1.3.3.1.tar.gz \
##   && tar xf maxent_1.3.3.1.tar.gz \
##   && cd maxent \
##   && R CMD INSTALL .

RUN git clone https://github.com/biomodhub/biomod2 \
  && cd biomod2 \
  && R CMD INSTALL .

COPY ./maxent.jar /usr/local/lib/R/site-library/dismo/java/maxent.jar

FROM rocker/r-ver AS runner
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
    ## liblwgeom-dev \
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
