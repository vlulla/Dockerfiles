## The next few lines from https://tech.davis-hansson.com/p/make/  "Your Makefiles are wrong"
##
## So that we can use brace expansion which is unavailable in sh! Sheesh!
SHELL := bash
.ONESHELL:
.SHELLFLAGS := -eu -o pipefail -c
.DELETE_ON_ERROR:
.RECIPEPREFIX = >

MAKEFLAGS += --warn-undefined-variables
MAKEFLAGS += --no-builtin-rules

TAG := experiments_env
USR := $${USER:-usr}
GRP := grp

.PHONY: all clean message cleanall build-image
.DEFAULT_GOAL: all

all: build-image Makefile

build-image: Dockerfile
> docker buildx build -t ${TAG} --build-arg="USR=${USR}" --build-arg="GRP=${GRP}" -f $< .

clean:
> @echo "Do cleaning here"
> docker container prune

cleanall: clean
> @echo "Do some specialized cleaning here..."
> docker rmi --filter="dangling=true"

