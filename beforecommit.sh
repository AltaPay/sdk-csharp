#!/usr/bin/env bash

# For this script to work you must have:
# 1) nunit-console
# 2) monodevelop

CONFIGURATION='Debug'

echo "Updating repository"
svn up

echo ""
echo "Compiling in $CONFIGURATION mode"
mdtool build --configuration:$CONFIGURATION AltaPayMoto/AltaPayMoto.sln

echo ""
echo "Running unit tests"
nunit-console AltaPayMoto/AltaPayMoto.Tests/bin/$CONFIGURATION/AltaPayMoto.Tests.dll -run=AltaPay.Moto.Tests.Unit

# we do not care about the nunit report at this stage
# might as well remove it
rm TestResult.xml
