## The next few lines from https://tech.davis-hansson.com/p/make/  "Your Makefiles are wrong"
##
## So that we can use brace expansion which is unavailable in sh! Sheesh!
.SHELLFLAGS := -eu -o pipefail -c
.DELETE_ON_ERROR:
.ONESHELL:
SHELL := bash


.PHONY: all clean message
.DEFAULT_GOAL: all

all: example.pdf

%.pdf: %.tex
	latexmk -silent -rules -xelatex $<
	latexmk -silent -pdf -c $<

clean:
	rm -rf *.{aux,bbl,bcf,blg,fdb_latexmk,fls,log,pdf,run.xml,bbl,run.xml}
