.PHONY: all clean cleanall
.DEFAULT: all

all: Dockerfile
	sed -e "s/^ARG UID=/&$$(id -u)/g;s/^ARG GID=/&$$(id -g)/g;s/^ARG SWUSR=/&$$(id -un)/g;s/^ARG SWGRP=/&$$(id -gn)/g" $< | docker buildx build --tag spatialite -f - .

clean:
	@echo "Do cleaning here"

cleanall: clean
	docker rmi spatialite



