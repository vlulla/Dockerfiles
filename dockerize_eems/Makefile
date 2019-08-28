.PHONY: all clean alpine
.DEFAULT: all eems alpine

all: eems alpine

eems: Dockerfile
	docker build -t eems -f $< .

ubuntu: Dockerfile.ubuntu
	docker build -t eems_ubuntu -f $< .

clean:
	@echo "Do cleaning here"
	docker rmi eems eems_alpine
