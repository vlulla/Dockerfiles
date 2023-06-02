SHELL := bash
.ONESHELL:
.SHELLFLAGS := -eu -o pipefail -c
.DELETE_ON_ERROR:
.RECIPEPREFIX = >

MAKEFLAGS += --warn-undefined-variables
MAKEFLAGS += --no-builtin-rules

.PHONY: all clean cleanall
.DEFAULT: all

IMGTAG=vl-ds

all: Dockerfile
# > sed -E -e "s/^ARG USR=usr$$/&$$(id -u)/g;s/^ARG GRP=grp$$/&$$(id -g)/g" $< | docker buildx build -t vl-ds -f - .
> cat $< | docker buildx build $(if $(I),--no-cache,) --build-arg USR=$$(id -un) --build-arg GRP=$$(id -gn) --build-arg VERSION=sid --tag ${IMGTAG} -

run: Dockerfile
> docker run -ti --rm --name tst --entrypoint zsh ${IMGTAG}

clean:
> docker rmi ${IMGTAG}

cleanall: clean
> @echo "Do more thorough cleaning here"
