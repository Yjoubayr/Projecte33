using Newtonsoft.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;



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


builder.Services.AddDbContext<DataContext>(options =>
{
    options.UseSqlServer("Server=localhost,1435;Database=master;User Id=sa;Password=Passw0rd!;");
});


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
