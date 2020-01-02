## The next few lines from https://tech.davis-hansson.com/p/make/  "Your Makefiles are wrong"
##
## So that we can use brace expansion which is unavailable in sh! Sheesh!
.SHELLFLAGS := -eu -o pipefail -c
.DELETE_ON_ERROR:
.ONESHELL:
SHELL := bash


.PHONY: all clean message
.DEFAULT_GOAL: all

all: Dockerfile
	sed -e "s/{{USERNAME}}/$$(id -un)/g" -e "s/{{GROUPNAME}}/$$(id -gn)/g" -e "s/{{GID}}/$$(id -g)/g" -e "s/{{UID}}/$$(id -u)/g" $< | sudo docker build -t dockerize_latex -f - .
clean:
	@echo "Do cleaning here"

