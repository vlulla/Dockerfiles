.PHONY: all clean docker2singularity cleanall
.DEFAULT: all

all: Dockerfile
	sed -e "s/^ARG UID=$$/&$$(id -u)/g;s/^ARG GID=$$/&$$(id -g)/g" $< | docker build -t r_geospatial -f - .

Dockerfile.singularity: Dockerfile
	sed -e '/RUN groupadd/d' -e '/USER r/d' $< > $@

docker2singularity: Dockerfile.singularity
	docker build -t r_geospatial_for_singularity -f $< .

singularityimg: docker2singularity
	docker run -v /var/run/docker.sock:/var/run/docker.sock -v $(PWD):/output --privileged -t --rm singularityware/docker2singularity r_geospatial_for_singularity


clean:
	@echo "Do cleaning here"
	rm -rf Dockerfile.singularity

cleanall: clean
	docker rmi r_geospatial r_geospatial_for_singularity rocker/r-base



