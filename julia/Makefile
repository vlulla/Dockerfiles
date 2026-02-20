SHELL := bash
.ONESHELL:
.SHELLFLAGS := -eu -o pipefail -c
.DELETE_ON_ERROR:
.RECIPEPREFIX = >

MAKEFLAGS += --warn-undefined-variables
MAKEFLAGS += --no-builtin-rules

.PHONY: all clean cleanall
.DEFAULT: all

IMGTAG = julia
## NC <--> NoCache! Run below to see the difference
## bash $ diff <(NC=0 make -n) <(make -n)

all: Dockerfile
> docker buildx build $(if $(NC),--no-cache,) --platform=linux/amd64 --progress=plain -f $< --tag ${IMGTAG} .

clean:
> docker rmi ${IMGTAG}

cleanall: clean
> @echo "Do cleaning here"



