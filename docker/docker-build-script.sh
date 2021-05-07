#!/bin/bash

docker build . --file Dockerfile-build-image -t csharp-build
docker run --rm --mount type=bind,source="$(pwd)/..",target=/app csharp-build ../bin/sh -c 'cd /app && ant'
