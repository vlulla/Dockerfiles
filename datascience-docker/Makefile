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
## > cat $< | docker buildx build $(if $(I),--no-cache,) --platform linux/amd64 --progress=plain --build-arg USR=$$(id -un) --build-arg GRP=$$(id -gn) --build-arg VERSION=bookworm --tag ${IMGTAG}:$$(git rev-parse --short=12 HEAD) -
> cat $< | docker buildx build $(if $(I),--no-cache,) --platform linux/amd64 --progress=plain --build-arg UID=$$(id -u) --build-arg USR=$$(id -un) --build-arg GID=$$(id -g) --build-arg GRP=$$(id -gn) --build-arg VERSION=bookworm --tag ${IMGTAG} -

run: Dockerfile
> @docker run --tty --interactive --rm --hostname=ds-container --platform linux/amd64 --name tst --volume="$$(pwd):/app:ro" --workdir /app ${IMGTAG}

clean:
> @docker rmi $$(docker images --filter=reference='${IMGTAG}:*' --format='{{.Repository}}:{{.Tag}}') 2>/dev/null || echo "docker images not found"

cleanall: clean
> @echo "Do more thorough cleaning here"
