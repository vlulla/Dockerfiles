.PHONY: all clean alpine
.DEFAULT: all eems alpine

all: eems

eems: Dockerfile
	docker buildx build --build-arg USER_ID=$$(id -u) --build-arg GROUP_ID=$$(id -g) -t eems -f $< .

clean:
	@echo "Do cleaning here"
	docker rmi eems
