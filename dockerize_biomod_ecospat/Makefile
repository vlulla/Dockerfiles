.PHONY: all build clean cleanall

all: build
	@echo "call with a target"

build: Dockerfile
	docker build -t ecospat_tuning -f $< .

clean:
	@echo "Do cleaning here"
cleanall:
	@echo "Do *more extensive* cleaning here"

