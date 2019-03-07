.PHONY: all clean
.DEFAULT: all

all: Dockerfile
	docker build -t lilypond -f $< .


clean:
	@echo "Do cleaning here"
	docker image rm lilypond
	docker image rm ubuntu


