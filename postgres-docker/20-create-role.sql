-- This script is automatically run by docker-compose but if you want
-- to create a specific role then use it like this:
--
--   bash $ psql -U postgres -h localhost -f create-role.sql postgres

begin;
revoke create on schema public from public;
create role v with login password 'v';
commit;

create database v with owner=v;
begin;
\c v
create schema v authorization v;
create extension postgis;
commit;

