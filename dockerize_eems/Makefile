.PHONY: all clean alpine
.DEFAULT: all eems alpine

all: eems alpine

eems: Dockerfile
	docker build --build-arg USER_ID=$$(id -u) --build-arg GROUP_ID=$$(id -g) -t eems -f $< .

ubuntu: Dockerfile.ubuntu
	docker build --build-arg USER_ID=$$(id -u) --build-arg GROUP_ID=$$(id -g) -t eems_ubuntu -f $< .

clean:
	@echo "Do cleaning here"
	docker rmi eems eems_alpine
