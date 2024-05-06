set VERSION=1.0.36
docker container rm colservice -f
docker build --build-arg VERSION=%VERSION% -t colservice:%VERSION% -t colservice:latest -f CoL.Service\Dockerfile .
docker run -d -e PUID=1005 -e PGID=100 --name colservice --env "TZ=Europe/Bucharest" --restart always --volume /share/docker/col:/data colservice:%VERSION%