# To build a sample web api dotnet project
> dotnet new webapi

# Run a dotnet project from the terminal
# need to be ran from the project's folder
> dotnet run

# To build an image of our rest api for docker
# need to be ran from the project's folder
> docker build -t restapi .

# To build an image of HAProxy with our configs
# need to be ran from the project's folder
> docker build -t my-haproxy .

# To create a bridge network in docker
> docker network create -d bridge hanet

# To run the web api as a container in docker and connect it to our network
> docker run -d --name web --net hanet restapi

# To run the haproxy in docker and connect it to our network
> docker run -d --name my-running-haproxy -p 80:80 -p 8404:8404 --net hanet my-haproxy