.PHONY: all clean cleanall
.DEFAULT: all

all: Dockerfile
	docker buildx build -t sci_python -f $< .


clean:
	@echo "Do cleaning here"
	docker rmi sci_python
cleanall: clean
	docker rmi continuumio/anaconda3


