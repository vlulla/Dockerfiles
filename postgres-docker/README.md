# Example of using PostgreSQL in docker for quick experimentation

Open two terminal windows!

```bash
## in the first terminal window
$ make up ## calls docker compose -f compose.yaml up
$ docker ps -a
$ docker container inspect db_container ## the name is the service name in compose.yaml!
$ docker container inspect db_container | jq '.[0].NetworkSettings.Ports'
```

Now in the second terminal:

```bash
$ psql -h localhost -p 25432 -U postgres postgres 
## Details for these can be found in the compose.yaml file
## added advantage that the settings from ${HOME}/.psqlrc will be applied to this exploratory db!!
psql > \d
psql > \q
$ make down ## calls docker compose -f compose.yaml down
```
