CREATE DATABASE IF NOT EXISTS Copastur;

-- Creation of users table
CREATE TABLE IF NOT EXISTS usuarios (
  id varchar(250) NOT NULL,
  name varchar(250) NOT NULL,
  email varchar(250) NOT NULL,
  PRIMARY KEY (id)
);