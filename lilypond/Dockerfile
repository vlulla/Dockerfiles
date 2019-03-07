FROM ubuntu
LABEL maintainer "Vijay Lulla <vijaylulla@gmail.com>"

## docker build -t lilypond .

## ENV DISPLAY :0
RUN apt-get update -qq -y \
   && apt-get install -y --no-install-recommends \
   vim-tiny \
   texlive-base \
   lilypond

CMD ["bash"]
