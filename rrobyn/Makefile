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

.PHONY: all clean run
.DEFAULT_GOAL: all

## macOS has low uid/gid values!!!
ifeq ($(shell uname), Linux)
BUILD_ARGS=--build-arg USR=$$(id -un) --build-arg UID=$$(id -u) --build-arg GRP=$$(id -gn) --build-arg GID=$$(id -g)
else
BUILD_ARGS=
endif

all: Dockerfile
> docker buildx build $(if $(I),--no-cache,) --platform=linux/amd64 --network=host                  --tag rrobyn:$$(command date +"%Y%m%d") -f $< ${BUILD_ARGS} . > docker-build-$$(command date +"%Y%m%d").log 2>&1

build-nobuildx: Dockerfile
> docker build $(if $(I),--no-cache,) --platform=linux/amd64 --network=host --tag rrobyn:$$(command date +"%Y%m%d") -f $< ${BUILD_ARGS} . > docker-build-$$(command date +"%Y%m%d").log 2>&1

run:
> docker run --tty --interactive --name tst --platform=linux/amd64 --volume="$$(pwd):/app" --workdir=/app --rm --entrypoint bash rrobyn:$$(command date +"%Y%m%d")

clean:
> @echo "Removing the docker image"
> docker rmi "$$(docker images --format='{{.|json}}' | jq -r -c 'select(.Repository=="rrobyn")|.Repository+":"+.Tag')"
