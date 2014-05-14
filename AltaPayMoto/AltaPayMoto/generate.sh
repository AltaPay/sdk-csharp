#!/usr/bin/env bash

wget "http://gateway.dev.earth.pensio.com/APIResponse.xsd" -O Generated/APIResponse.xsd
xsd Generated/APIResponse.xsd /c /o:Generated /namespace:AltaPay.Service.Dto