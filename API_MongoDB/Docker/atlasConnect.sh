#!/bin/bash
# This script is used to connect to the server
# mongosh "mongodb://localhost:27017/?readPreference=primary&appname=MongoDB%20Compass&ssl=false"
mongosh "mongodb+srv://myatlasclusteredu.qqya62v.mongodb.net/" --apiVersion 1 \
        --username myAtlasDBUser  --password myatlas-001