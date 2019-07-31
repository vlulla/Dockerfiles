# Dockerized Estimating Effective Migration Surfaces

A docker image for [EEMS](https://github.com/dipetkov/eems).  This image only creates the program `runeems_sats` but it can be modified to create the other programs too.  To use this just download the `Dockerfile` into an empty directory and build a docker image from it.  The steps to do it are:

```
cd <path/of/directory/containing/Dockerfile>
docker build -t eems .
```

The above will create a lot of text output about what it is doing.  At the end it should say `Successfully built` and `Successfully tagged`.  Now you can run it with the following command:

```
docker run -ti --rm eems
```
