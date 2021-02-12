# Notes application
***
## Requirements
* ##### **Docker (Tested on Docker Desktop - versions 20.10.2 and 19.03.13)** 
* ##### **Dotnet SDK (5.0.+)** *(required only if you want run integration tests)*
***
## Building and running solution

### Description:
Presented solution works on docker containers. After cloning this repository and running commands shown below, there's no need to set up anything. Building takes a while, but please be patient - docker has to download and build all images.

When the solution starting process is completed, in your docker desktop you should see something similar to:
![Docker desktop](https://i.imgur.com/YlOxn13.png)

### To build solution (optional):
```
$ docker-compose build 
```
### To run solution:
```
$ docker-compose up
```
### To run tests:
#### Shell (i.e git bash):
``` 
$ chmod +x scripts/test.sh (if it's required)
$ ./scripts/test.sh
```

## Usage

### Description
##### Services are available on addresses:
* ###### **CRUD service** - *http:://localhost:5000*
* ###### **Note history service** - *http:://localhost:5003*
* ###### **WebUI service** - *http:://localhost:8080*
When solution starts, by default there are no notes in database, so list in WebUI service is empty.
Documentation of both CRUD and history service Api, can be found on:
*http:://localhost:5000/swagger/index.html* 
and 
*http:://localhost:5003/swagger/index.html*
![Api docs](https://imgur.com/wdaqxPp.png)
It's interactive and pretty simple in use, so there's no need to explain details.
After executing any command, you will see CURL examples.

#### WebUI
You can access webui on *http:://localhost:8080*
Example:
![webui_1](https://imgur.com/r6PkVlL.png)
After you click on note title, you will see modal with note title and content:
![webui_2](https://imgur.com/EOFIjMi.png)
Notes can be sorted by clicking on column headers (click twice to change order). By default, notes are sorted ascending by title.
![webui_2](https://imgur.com/j2isRwL.png)

