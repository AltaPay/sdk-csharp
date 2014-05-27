#!/usr/bin/env bash

# For this script to work you must have:
# 1) nunit-console
# 2) monodevelop

CONFIGURATION='Debug'

echo "Updating repository"
svn up

echo ""
echo "Compiling in $CONFIGURATION mode"
mdtool build --configuration:$CONFIGURATION AltaPayApi/AltaPayApi.sln

echo ""
echo "Running unit tests"
nunit-console AltaPayApi/AltaPayApi.Tests/bin/$CONFIGURATION/AltaPayApi.Tests.dll -run=AltaPay.Service.Tests.Unit

# we do not care about the nunit report at this stage
# might as well remove it
rm TestResult.xml
