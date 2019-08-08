.PHONY: all clean alpine
.DEFAULT: all eems alpine

all: eems alpine

eems: Dockerfile
	docker build -t eems -f $< .

alpine: Dockerfile.alpine
	docker build -t eems_alpine -f $< .

clean:
	@echo "Do cleaning here"
	docker rmi eems eems_alpine
