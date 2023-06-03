SHELL := bash
.ONESHELL:
.SHELLFLAGS := -eu -o pipefail -c
.DELETE_ON_ERROR:
.RECIPEPREFIX = >

MAKEFLAGS += --warn-undefined-variables
MAKEFLAGS += --no-builtin-rules

.PHONY: all clean cleanall
.DEFAULT: all

IMGTAG = python_geospatial
## NC <--> NoCache! Run below to see the difference
## bash $ diff <(NC=0 make -n) <(make -n)

all: Dockerfile
## > sed -e "s/^ARG UID=/&$$(id -u)/g;s/^ARG GID=/&$$(id -g)/g;s/^ARG PYUSR=/&$$(id -un)/g;s/^ARG PYGRP=/&$$(id -gn)/g" $< | docker buildx build --tag python_geospatial -f - .
> docker buildx build $(if $(NC),--no-cache,) --progress=plain -f $< --tag ${IMGTAG} --build-arg=GID=$$(id -g) --build-arg=UID=$$(id -u) --build-arg=PYUSR=$$(id -un) --build-arg=PYGRP=$$(id -gn) .

clean:
> docker rmi ${IMGTAG}

cleanall: clean
> @echo "Do cleaning here"



