mkdir Migrations
docker run -e "ACCEPT_EULA=Y" -e "SA_PASSWORD=Passw0rd!" -p 1433:1433 --name scrabble -h scrabble -d projecteinf/mssql:latest
dotnet ef migrations add "init"
dotnet tool install --global dotnet-ef
dotnet-ef database update
