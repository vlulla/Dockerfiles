# syntax=docker/dockerfile:1
FROM julia:latest

## Buid with command: docker build -t julia .
##
## Run with command: docker run --rm -it --init --hostname=julia --name=julia --net=mynet julia

SHELL ["/bin/bash", "-o", "pipefail", "-c"]

ARG DEBIAN_FRONTEND=noninteractive
RUN julia -e 'using Pkg;map(Pkg.add,["CSV","DataFrames","Distributions","DuckDB","GLM","GLMNet","HiGHS","JuMP","KernelDensity","Loess","MixedModels","MultivariateStats","NLopt","Plots","RDatasets","SCIP","StatsBase","StatsModels","StatsPlots","Survival","TimeSeries",])'

CMD [ "julia" ]
