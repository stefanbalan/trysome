set VERSION=1.0.2
docker build --build-arg VERSION=%VERSION% -t colservice:%VERSION% -f CoL.Service\Dockerfile .
