# Gebäudeautomation-Backend

This project is a web-app, created for the module Gebäudeautomation.

## Overview

The project consists of a server-side application.
First off (`API`), that uses a db to store user and sensor data. The data can also be manipulated via the api.

## Software Setup

In order to develop for API-backend, the following components are required:

* Git client
* Code editor, e.g. Visual Studio Code (https://code.visualstudio.com)
* dotnet SDK 7 (https://dotnet.microsoft.com/en-us/download/dotnet/7.0)
* NodeJS (v18.12.0) and NPM (v8.19.2) (https://nodejs.org/en/). It is recommended to use Node-Version-Manager (nvm https://github.com/nvm-sh/nvm)
* Docker (https://docs.docker.com/get-docker/). It is recommended to use Docker Desktop.

## Project Setup

Use `git-clone` to clone the repository.

### Docker

Docker is used to provide an easy to use database with a setting preset corresponding with the api.
Docker compose is used to manage multiple containers (services) at once in a single docker network.
Currently there is a postgres database container and a pgadmin container.
The Docker container for postgres must always be started before the api.

If you only need the database:

```bash
# Navigate to the docker project
cd docker

# Build only the db service without pgadmin
docker-compose up -d geau-db
```

If you want pg-admin interface aswell.

```bash
# Build the two service (evire-pgadmin depends on evire-db)
docker-compose up -d geau-pgadmin

```

### Server-Side Project

If the command above does not work, add the api certificate to the "Trusted Root Certificate Authorities" certificate store.

```bash
# Navigate to the project
cd API

# Downloads all NuGet packages required for the project.
dotnet restore

# Restore dotnet tools
dotnet tool restore

# Builds the whole project
dotnet build

# Starts the API
dotnet run

```

The database is automatically migrated when the server with the API is started.

### Raspberry PI
Nutzer: t20
Passwort: geau2023
Hostname: geau.local