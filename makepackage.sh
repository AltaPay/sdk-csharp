#!/usr/bin/env bash

TMP_DIR="_tmp"
OUT="PensioMoto"
CONFIGURATION="Release"

echo "Clearing builds"
./clearbuilds.sh

echo ""
echo "Compiling in $CONFIGURATION mode"
mdtool build --configuration:$CONFIGURATION PensioMoto/PensioMoto.sln

# remove tmp dir if it exists
if [ -d "$TMP_DIR" ]; then
        rm $TMP_DIR -rf
fi

# create the tmp dir
mkdir $TMP_DIR

#
# the application
#
cp -r PensioMoto/PensioMoto/bin/$CONFIGURATION/* $TMP_DIR
cp PensioMoto/register.cmd $TMP_DIR

#
# source code
#
svn export PensioMoto/ $TMP_DIR/source

#
# remove the output file if it exists
# otherwise we just add to it
#
if [ -f "$OUT.zip" ]; then
        rm "$OUT.zip"
fi


#
# create the archive
#
cd $TMP_DIR
zip -r ../$OUT *
cd ..

#
# clean up
#
rm $TMP_DIR -rf