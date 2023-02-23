#!/bin/bash
<<-EOSQL
  CREATE DATABASE Copastur;
  GRANT ALL PRIVILEGES ON DATABASE Copastur TO admin;
  \connect Copastur admin
  BEGIN;
    CREATE TABLE IF NOT EXISTS usuarios (
	  id varchar(250) NOT NULL,
    name varchar(250) NOT NULL,
    email varchar(250) NOT NULL,
    PRIMARY KEY (id)
	);
  COMMIT;
EOSQL