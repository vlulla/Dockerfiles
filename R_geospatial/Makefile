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
> docker buildx build  --progress=plain $(if $(NC),--no-cache,) --platform=linux/amd64 --build-arg RUSER=$$(id -un) --build-arg RGROUP=$$(id -gn) --tag $(IMGTAG) -f $< .

clean:
> docker rmi $(IMGTAG)

cleanall: clean
> @echo "Do more thorough cleaning here"
