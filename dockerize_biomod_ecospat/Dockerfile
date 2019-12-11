FROM rocker/geospatial:3.6.1
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

ENV USER ${USER:-vlulla}

RUN useradd -ms /bin/bash ${USER}

USER ${USER}
WORKDIR /home/${USER}

CMD ["bash"]
