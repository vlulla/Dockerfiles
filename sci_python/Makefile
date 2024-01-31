SHELL := bash
.ONESHELL:
.SHELLFLAGS := -eu -o pipefail -c
.DELETE_ON_ERROR:
.RECIPEPREFIX = >

MAKEFLAGS += --warn-undefined-variables
MAKEFLAGS += --no-builtin-rules

IMGTAG = sci_python

.PHONY: all clean cleanall
.DEFAULT: all

DOCKER_OPTS = --platform linux/amd64 --network=host $(if $(NC),--no-cache,) --progress=plain

all: Dockerfile
> docker buildx build $(DOCKER_OPTS) --tag $(IMGTAG) -f $< .


clean:
> docker rmi ${IMGTAG}

cleanall: clean
> @echo "Do cleaning here"


