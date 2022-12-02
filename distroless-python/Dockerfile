## FROM gcr.io/distroless/python3-debian11
FROM gcr.io/distroless/python3-debian11:debug
LABEL maintainer "Vijay Lulla <vijaylulla@gmail.com>"

COPY . /app

WORKDIR /app

ENTRYPOINT ["python","hello.py"]

