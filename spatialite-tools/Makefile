SHELL := bash
.ONESHELL:
.SHELLFLAGS := -eu -o pipefail -c
.DELETE_ON_ERROR:
.RECIPEPREFIX = >

.PHONY: all clean cleanall
.DEFAULT: all

ifeq ($(shell uname -s), Linux)
	BUILD_ARGS=--build-arg UID=$$(id -u) --build-arg USR=$$(id -un) --build-arg GID=$$(id -g) --build-arg GRP=$$(id -gn)
else
	BUILD_ARGS=
endif

all: Dockerfile
> cat $< | docker buildx build  $(if $(I),--no-cache,) --network=host --platform=linux/amd64 --progress=plain $(BUILD_ARGS) --tag spatialite -

clean:
> @echo "Do cleaning here"

cleanall: clean
> docker rmi spatialite



