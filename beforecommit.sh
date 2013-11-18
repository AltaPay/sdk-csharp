#!/usr/bin/env bash

# For this script to work you must have:
# 1) nunit-console
# 2) monodevelop

CONFIGURATION='Debug'

echo "Updating repository"
svn up

echo ""
echo "Compiling in $CONFIGURATION mode"
mdtool build --configuration:$CONFIGURATION PensioMoto/PensioMoto.sln

echo ""
echo "Running unit tests"
nunit-console PensioMoto/PensioMoto.Tests/bin/$CONFIGURATION/PensioMoto.Tests.dll -run=PensioMoto.Tests.Unit

# we do not care about the nunit report at this stage
# might as well remove it
rm TestResult.xml
