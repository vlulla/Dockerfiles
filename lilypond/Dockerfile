FROM ubuntu
LABEL maintainer "Vijay Lulla <vijaylulla@gmail.com>"

## Build: docker build -t lilypond .
## Run  : docker run --rm -it --name lilypond lilypond

## ENV DISPLAY :0
RUN apt-get update -qq -y \
   && apt-get install -y --no-install-recommends \
   vim-tiny \
   texlive-base \
   lilypond

CMD ["bash"]
