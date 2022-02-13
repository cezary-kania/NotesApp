# Notes application
***
## Requirements
* ##### **Docker** 
* ##### **Dotnet SDK (6.0.\*)** *(required only if you want run integration tests)*
***
## Building and running solution

### Description:
Presented solution works on docker containers. After cloning this repository and running commands shown below, there's no need to set up anything. Building takes a while, but please be patient - docker has to download and build all images.

When the solution starting process is completed, in your docker desktop you should see something similar to:
![Docker desktop](https://i.imgur.com/YlOxn13.png)

### Build solution

#### Add .env file with secret password
```
SECRET_PASSWORD=Passw0rd!
```

#### Generate https certificate
```
dotnet dev-certs https --clean
dotnet dev-certs https -ep %USERPROFILE%\.aspnet\https\aspnetapp.pfx -p Pa$$w0rd!
dotnet dev-certs https --trust
```
#### Build docker images
```
$ docker-compose build 
```
### Run solution
```
$ docker-compose up
```
### Run tests
#### Shell (i.e git bash)
``` 
$ chmod +x scripts/test.sh (if it's required)
$ ./scripts/test.sh
```

## Usage

### Description
##### Services are available on addresses:
* ###### **CRUD service** - *https://localhost:4431*
* ###### **Note history service** - *https://localhost:4432*
* ###### **WebUI service** - *http://localhost:8080*
When solution starts, by default there are no notes in database, so list in WebUI service is empty.
Documentation of both CRUD and history service Api, can be found on:
*https://localhost:4431/swagger/index.html* 
and 
*https://localhost:4432/swagger/index.html*
![Api docs](https://imgur.com/wdaqxPp.png)
It's interactive and pretty simple in use, so there's no need to explain details.
After executing any command, you will see CURL examples.

#### WebUI
You can access webui on *http:://localhost:8080*
Example
![webui_1](https://imgur.com/r6PkVlL.png)
After you click on note title, you will see modal with note title and content
![webui_2](https://imgur.com/EOFIjMi.png)
Notes can be sorted by clicking on column headers (click twice to change order). By default, notes are sorted ascending by title.
![webui_2](https://imgur.com/j2isRwL.png)

