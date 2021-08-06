.PHONY: all clean cleanall
.DEFAULT: all

all: Dockerfile
	sed -e "s/^ENV UID=/&$$(id -u)/g;s/^ENV GID=/&$$(id -g)/g" $< | docker build -t python_geospatial -f - .

clean:
	@echo "Do cleaning here"

cleanall: clean
	docker rmi python_geospatial rocker/r-base



