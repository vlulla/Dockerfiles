
# LaTeX (with GNU Make) in Docker

A docker image to allow using latex (with xelatex) to build pdfs from LaTeX source files.  Since this container will need to write files to the directory you'll need to make sure that uid/gid are correct. All of this is accomplished by using `sed` and `make`.  NOTE: since `make(1)` expands `$` during its passes we have to use two dollar signs (`$$`) to be able to send correct commands to the shell.  Please consult the [Makefile](Makefile) and [Using Variables in Recipes](https://www.gnu.org/software/make/manual/make.html#Variables-in-Recipes) to see how this is organized.

To run this on a linux system with appropriapte permissions run the following:

```
$ docker run -ti --rm -v $(pwd)/example:/home/$(id -un)/example -u $(id -un):$(id -gn) dockerize_latex
vlulla@indocker:~$ ls
vlulla@indocker:~$ cd example
vlulla@indocker:~$ make  ## or latexmk -xelatex -c example.tex
vlulla@indocker:~$ exit
$ ## Verify that the example file is present.
$ ls -alh example/example.pdf
```
