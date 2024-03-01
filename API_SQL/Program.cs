using Newtonsoft.Json.Serialization;
using dymj.ReproductorMusica.API_SQL.Data;
using Microsoft.EntityFrameworkCore;
using dymj.ReproductorMusica.API_SQL.Controller;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//builder.Services.AddScoped<dymj.ReproductorMusica.API_SQL.Services.CancoService>();
//builder.Services.AddScoped<dymj.ReproductorMusica.API_SQL.Services.GrupService>();
//builder.Services.AddScoped<dymj.ReproductorMusica.API_SQL.Services.AlbumService>();
//builder.Services.AddScoped<dymj.ReproductorMusica.API_SQL.Services.MusicService>(); 
//builder.Services.AddScoped<dymj.ReproductorMusica.API_SQL.Services.LlistaService>(); 
//builder.Services.AddScoped<dymj.ReproductorMusica.API_SQL.Services.InstrumentService>(); 
//builder.Services.AddScoped<dymj.ReproductorMusica.API_SQL.Services.TocarService>(); 


builder.Services.AddControllers();
builder.Services.AddControllers().AddNewtonsoftJson(options =>
{
    options.SerializerSettings.ContractResolver = new DefaultContractResolver();
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

ConfigDocker configDocker = new ConfigDocker();
// var connectionString = "Server=localhost;Port=3308;Database=mySQLDb;Uid=sa;Pwd=Passw0rd!;";
    var connectionString = "Server=localhost;Port=3308;Database=mySQLDb;Uid=sa;Pwd=Passw0rd!;";

builder.Services.AddDbContext<DataContext>(options => 

    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")), ServiceLifetime.Singleton
    //options.UseSqlServer(builder.Configuration.GetConnectionString("ConnectionProvaMSSQL")), ServiceLifetime.Singleton
    //options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString))

);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
