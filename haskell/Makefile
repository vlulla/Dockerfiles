## The next few lines from https://tech.davis-hansson.com/p/make/  "Your Makefiles are wrong"
##
## So that we can use brace expansion which is unavailable in sh! Sheesh!
.SHELLFLAGS := -eu -o pipefail -c
.DELETE_ON_ERROR:
.ONESHELL:
SHELL := bash

MAKEFLAGS += --warn-undefined-variables
MAKEFLAGS += --no-builtin-rules


.PHONY: all clean cleanall
.DEFAULT_GOAL: all

all: Dockerfile
	docker build --build-arg USER_ID=$$(id -u) --build-arg GROUP_ID=$$(id -g) -f $< -t haskell_with_user .

clean:
	docker image prune -f

cleanall: clean
	docker rmi haskell_with_user
