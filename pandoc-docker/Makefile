## The next few lines from https://tech.davis-hansson.com/p/make/  "Your Makefiles are wrong"
##
## So that we can use brace expansion which is unavailable in sh! Sheesh!
.SHELLFLAGS := -eu -o pipefail -c
.DELETE_ON_ERROR:
.ONESHELL:
SHELL := bash

.PHONY: all clean message
.DEFAULT_GOAL: all

ifeq ($(shell uname -s), Linux)
	BUILD_ARGS=--build-arg UID=$$(id -u) --build-arg USR=$$(id -un) --build-arg GID=$$(id -g) --build-arg GRP=$$(id -gn)
else
	BUILD_ARGS=
endif

all: Dockerfile
	docker buildx build --progress=plain $(if $(NC),--no-cache,) --network=host $(BUILD_ARGS) -t pandoc-komascript -f $< .
clean:
	docker rmi pandoc-komascript

