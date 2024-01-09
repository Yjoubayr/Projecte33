dotnet new webapi -o .
rm WeatherForecast.cs
rm Controllers/WeatherForecastController.cs
mkdir Data
mkdir Model
dotnet add package Microsoft.EntityFrameworkCore.InMemory