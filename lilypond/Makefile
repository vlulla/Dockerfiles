.PHONY: all clean
.DEFAULT: all

all: Dockerfile
	sed -e "s/^ARG UID=/&$$(id -u)/g;s/^ARG GID=/&$$(id -g)/g;s/^ARG USER=/&$$(id -un)/g;s/^ARG GROUP=/&$$(id -gn)/g" $< | docker build -t lilypond -f - .
clean:
	@echo "Do cleaning here"
	docker rmi lilypond ubuntu


