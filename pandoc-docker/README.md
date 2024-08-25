# Pandoc with latex with koma-script

I use `scrartcl` documentclass in my [md template](https://github.com/vlulla/vim_templates/blob/main/mdtemplate.md). The `pandoc/latex` image does not have this document class but pandoc's excellent documentation explains how to [build custom images](https://github.com/pandoc/dockerfiles#building-custom-images).

```bash
~ $ docker build --tag pandoc-komascript .
~ $ # assuming you have t.md file...
~ $ docker run -it --rm --mount type=bind,src="$(pwd)",dst=/data pandoc-komascript -o t.pdf --citeproc t.md
~ $ docker run -it --rm --mount type=bind,src="$(pwd)",dst=/data pandoc-komascript -o t.html --standalone --embed-resources --mathml --citeproc t.md
```
