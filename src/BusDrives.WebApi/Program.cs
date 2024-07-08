using BusDrives.DataService.Data;
using BusDrives.DataService.Repositories;
using BusDrives.DataService.Repositories.Interfaces;
using BusDrives.WebApi.EndpointExtension;
using BusDrives.WebApi.Extensions;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<ICityRepository, CityRepository>();
builder.Services.AddScoped<IBusDriveRepository, BusDriveRepository>();

builder.Services.AddDbContext<AppDbContext>(opt => 
    opt.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.AddCityEndpoints();
app.AddBusDriveEndpoints();

app.Run();
