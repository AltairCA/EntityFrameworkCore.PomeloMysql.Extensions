#!/bin/bash
NEXT_VERSION=$1
cd "${PROJECT_FOLDER}"
dotnet build && dotnet pack -p:PackageVersion=${NEXT_VERSION}
cd bin/Debug && zip -r release.zip .