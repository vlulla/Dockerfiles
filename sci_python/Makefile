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

all: Dockerfile
> docker buildx build --progress=plain $(if $(NC),--no-cache,) --tag ${IMGTAG} -f $< .


clean:
> docker rmi ${IMGTAG}

cleanall: clean
> @echo "Do cleaning here"


