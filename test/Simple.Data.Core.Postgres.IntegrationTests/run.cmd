docker-compose up -d
timeout 6 > NUL
dotnet test
docker-compose down
