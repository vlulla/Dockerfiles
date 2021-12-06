.PHONY: all clean cleanall
.DEFAULT: all

all: Dockerfile
	sed -e "s/^ARG UID=/&$$(id -u)/g;s/^ARG GID=/&$$(id -g)/g" $< | docker build -t dockerized_spark -f - .

clean:
	@echo "Do cleaning here"
	rm -rf Dockerfile.singularity

cleanall: clean
	docker rmi r_geospatial r_geospatial_for_singularity rocker/r-base



