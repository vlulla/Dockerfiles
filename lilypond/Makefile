.PHONY: all clean
.DEFAULT: all

BUILD_ARGS=--build-arg USER=$$(id -un) --build-arg GROUP=$$(id -gn)

all: Dockerfile
	docker buildx build --progress=plain $(if $(I),--no-cache,) --network=host --platform=linux/amd64 -t lilypond ${BUILD_ARGS} -f $< .
clean:
	@echo "Do cleaning here"
	docker rmi lilypond ubuntu


