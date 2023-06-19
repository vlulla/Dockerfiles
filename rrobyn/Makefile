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


all: Dockerfile
> docker buildx build $(if $(I),--no-cache,) --platform=linux/amd64 --network=host --progress=plain --tag rrobyn:$$(command date +"%Y%m%d") -f $< .

run:
> docker run --tty --interactive --name tst --platform=linux/amd64 --volume="$$(pwd):/app" --workdir=/app --rm --entrypoint bash rrobyn:$$(command date +"%Y%m%d")

clean:
> @echo "Removing the docker image"
> docker rmi "$$(docker images --format='{{.|json}}' | jq -r -c 'select(.Repository=="rrobyn")|.Repository+":"+.Tag')"
