.PHONY: all clean
.DEFAULT: all

all: Dockerfile
	docker build -t eems -f $< .

clean:
	@echo "Do cleaning here"
	docker rmi eems
