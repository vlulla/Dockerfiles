.PHONY: all clean cleanall
.DEFAULT: all

all: Dockerfile
	sed -e "s/^ARG UID=/&$$(id -u)/g;s/^ARG GID=/&$$(id -g)/g;s/^ARG PYUSR=/&$$(id -un)/g;s/^ARG PYGRP=/&$$(id -gn)/g" $< | docker build --tag python_geospatial -f - .

clean:
	@echo "Do cleaning here"

cleanall: clean
	docker rmi python_geospatial rocker/r-base



