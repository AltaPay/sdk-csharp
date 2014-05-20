#!/usr/bin/env bash

wget "http://gateway.dev.earth.pensio.com/APIResponse.xsd" -OAPIResponse.xsd
xsd APIResponse.xsd /c /o:AltaPayMoto/AltaPayMoto/Generated /namespace:AltaPay.Service.Dto
rm APIResponse.xsd