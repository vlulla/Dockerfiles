SHELL := bash
.ONESHELL:
.SHELLFLAGS := -eu -o pipefail -c
.DELETE_ON_ERROR:
.RECIPEPREFIX = >

.PHONY: all clean cleanall
.DEFAULT: all

TAG := $$(git rev-parse --short=12 HEAD)

all: Dockerfile
> docker buildx build --rm --tag distroless_python:${TAG} -f $< .

run:
> docker run -ti --rm --name tst distroless_python:${TAG}

clean:
> @echo "Do cleaning here"
> docker rmi $$(docker images --filter dangling=true -q)

cleanall:
> docker rmi distroless_python:${TAG}

