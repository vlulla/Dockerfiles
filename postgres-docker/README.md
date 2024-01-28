# Example of using PostgreSQL in docker for quick experimentation

Open two terminal windows!

```bash
## in the first terminal window
$ make up ## calls docker compose -f compose.yaml up
```

Now in the second terminal:

```bash
$ docker ps -a
$ docker container inspect db_container ## the name is the service name in compose.yaml!
$ docker container inspect db_container | jq '.[0].NetworkSettings.Ports'
## Now we can use the HostPort with the host psql command!
$ psql -h localhost -p 25432 -U postgres postgres 
## $ docker exec -it postgis psql -h localhost -U postgres postgres ## If you don't have psql locally installed!
## Details for these can be found in the compose.yaml file
## added advantage that the settings from ${HOME}/.psqlrc will be applied to this exploratory db!!
psql > \d
psql > \q
$ make down ## calls docker compose -f compose.yaml down
```

If you'd still like to use a separate user in this throwaway container here are the steps to follow:

```bash
$ psql -h localhost -p 25432 -U postgres postgres
# use the POSTGRES_PASSWORD set in the compose.yaml
psql #> CREATE ROLE tst WITH LOGIN CREATEDB PASSWORD 'tstpasswd';
psql #> CREATE DATABASE tst WITH owner = tst;
psql #> \c tst
psql #> CREATE EXTENSION postgis; -- now tst database will have postgis enabled database too!
psql #> \q
$ psql -h localhost -p 25432 -U tst tst
# type in the password 'tstpasswd' to login
```

This alternative user is now created as a part of `docker-compose up` process. Modify the role creation sql file to your liking. Or, add more sql files as volumes into the yml compose definition file.

Using `docker compose` is handy because it will automatically create/destroy the containers, networks, services that are needed to run the application together.

**NOTE:** The steps to create `30-pgstac.sql` are:

```bash
$ cd /tmp && git clone https://github.com/stac-utils/pgstac.git pgstac.git 
$ cat /tmp/pgstac.git/src/pgstac/pgstac.sql > 30-pgstac.sql
```
---

To use pgadmin run the `make up-with-pgadmin` command. Access the pgadmin interface on `localhost:8080` (modify to your liking in the yaml) and use the username password to connect to the pgadmin interface. In the interface add the postgres server. The connection details for the postgres database can be found in the yaml file. The only thing to note is that for the `Hostname/address` you'll have to use one of the aliases of the docker `db` container. The container name (`db`) will work too!! I prefer assigning, and using a, hostname to the `db` container. **NOTE**: Any of the aliases listed in `docker container inspect db | jq '.[0].NetworkSettings.Networks["postgis-net"].Aliases'` will do for server hostname/address field.
