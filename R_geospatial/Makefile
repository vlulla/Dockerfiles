.PHONY: all clean
.DEFAULT: all

all: Dockerfile
	docker build -t r_geospatial -f $< .


clean:
	@echo "Do cleaning here"
	docker rmi r_geospatial rocker/r-base


