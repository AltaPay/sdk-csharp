#!/bin/bash

set -e

SOURCEPATH="$( cd -- "$(dirname "$0")" >/dev/null 2>&1 ; pwd -P )"

echo ${SOURCEPATH}/..

cd ${SOURCEPATH}/..

docker build . --file docker/Dockerfile-build-image -t csharp-build
docker run --rm --mount type=bind,source="$(pwd)",target=/app csharp-build ../bin/sh -c 'cd /app && ant'
