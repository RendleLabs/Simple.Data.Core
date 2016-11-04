#!/bin/bash
docker-compose up -d
sleep 5
dotnet test
docker-compose down
