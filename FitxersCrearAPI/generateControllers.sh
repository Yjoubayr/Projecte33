#!/bin/bash
if [ -z "$1" ]
  then
    echo "No argument supplied"
    echo "Usage: ./generateControllers.sh <ModelName>"
    exit 1
fi
dotnet aspnet-codegenerator controller -name $1Controller -async -api -m $1 -dc DataContext -outDir Controllers