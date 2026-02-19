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

.PHONY: all up down stop start clean
.DEFAULT_GOAL: all

all: start
up: compose.yaml
> docker compose -f $< up

up-with-pgadmin: compose-with-pgadmin.yaml
> docker compose -f $< up

start: compose.yaml | up
> docker compose -f $< start
start-with-pgadmin: compose-with-pgadmin.yaml | up-with-pgadmin
> docker compose -f $< start

stop: compose.yaml
> docker compose -f $< stop
stop-with-pgadmin: compose-with-pgadmin.yaml
> docker compose -f $< stop

down: compose.yaml | stop
> sleep 1 && docker compose -f $< down

down-with-pgadmin: compose-with-pgadmin.yaml | stop-with-pgadmin
> sleep 1 && docker compose -f $< down

## use -B/--always-make option. Unconditionally make all targets!
30-pgstac.sql:
> curl -fsSLO https://raw.githubusercontent.com/stac-utils/pgstac/refs/heads/main/src/pgstac/pgstac.sql && mv pgstac.sql $@

clean:
> docker container prune --force

