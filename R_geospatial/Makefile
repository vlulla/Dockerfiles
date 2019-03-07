.PHONY: all clean
.DEFAULT: all

all: Dockerfile
	docker build -t r_geospatial -f $< .


clean:
	@echo "Do cleaning here"
	docker image rm r_geospatial


