SHELL := bash
.ONESHELL:
.SHELLFLAGS := -eu -o pipefail -c
.DELETE_ON_ERROR:
.RECIPEPREFIX = >

MAKEFLAGS += --warn-undefined-variables
MAKEFLAGS += --no-builtin-rules

.PHONY: all clean cleanall
.DEFAULT: all

IMGTAG = dockerized_spark

all: Dockerfile
## > sed -e "s/^ARG UID=/&$$(id -u)/g;s/^ARG GID=/&$$(id -g)/g;s/^ARG USER=/&$$(id -un)/g;s/^ARG GROUP=/&$$(id -gn)/g" $< | docker buildx build -t dockerized_spark -f - .
> docker buildx build -f $< --progress=plain $(if $(NC),--no-cache,) --build-arg UID=$$(id -u) --build-arg GID=$$(id -g) --build-arg USER=$$(id -un) --build-arg GROUP=$$(id -gn) --network=host --tag ${IMGTAG} .

clean:
> docker rmi ${IMGTAG}

cleanall: clean
> @echo "Do more thorough cleaning here"
