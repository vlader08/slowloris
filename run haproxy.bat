docker network create -d bridge hanet

docker build -f restapi/Dockerfile -t restapi restapi/.

docker build -f haproxy/Dockerfile -t my-haproxy haproxy/.

docker run -d --name web --net hanet restapi

docker run -d --name my-running-haproxy -p 80:80 -p 8404:8404 --net hanet my-haproxy