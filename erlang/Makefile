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

.PHONY: all clean message cleanall
.DEFAULT_GOAL: all

BUILD_ARGS = --build-arg USR=$$(id -un) --build-arg GRP=$$(id -gn)
all: Dockerfile
> cat $< | docker buildx build --tag erlang --network host --progress plain ${BUILD_ARGS} -


clean:
> @echo "Do cleaning here"
> rm -rf $(patsubst %.md,%.pdf,$(wildcard *.md))

cleanall: clean
> @echo "Do some specialized cleaning here..."
> rm -rf *.Rout .RData

