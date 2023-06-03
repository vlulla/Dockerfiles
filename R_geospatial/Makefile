SHELL := bash
.ONESHELL:
.SHELLFLAGS := -eu -o pipefail -c
.DELETE_ON_ERROR:
.RECIPEPREFIX = >

MAKEFLAGS += --warn-undefined-variables
MAKEFLAGS += --no-builtin-rules

IMGTAG=r_geospatial

.PHONY: all clean cleanall
.DEFAULT: all

## See https://stackoverflow.com/a/65854404 to understand the new builder context we have to create for larger log recording!
## bash $ docker buildx ls # to see the various builder contexts...

all: Dockerfile
# > sed -e "s/^ARG UID=$$/&$$(id -u)/g;s/^ARG GID=$$/&$$(id -g)/g" $< | docker buildx build -t r_geospatial -f - .
## > docker buildx create --use --name larger-log --driver-opt env.BUILDKIT_STEP_LOG_MAX_SIZE=$$((15*1024*1024))
> docker buildx build  --progress=plain $(if $(NC),--no-cache,) --build-arg UID=$$(id -u) --build-arg GID=$$(id -g) --build-arg RUSER=$$(id -un) --build-arg RGROUP=$$(id -gn) --tag $(IMGTAG) -f $< .

clean:
> docker rmi $(IMGTAG)

cleanall: clean
> @echo "Do more thorough cleaning here"
