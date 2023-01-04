﻿using Assignment_4.Models;
using Assignment_4.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.Configure<MongoDbSettings>(
    builder.Configuration.GetSection("MongoDbSettings"));

builder.Services.AddSingleton<TypesService>();
builder.Services.AddSingleton<ClassesService>();
builder.Services.AddSingleton<RaritiesService>();
builder.Services.AddSingleton<SetsService>();
builder.Services.AddSingleton<CardsService>();



builder.Services.AddControllers();

builder.Host.ConfigureLogging(logging =>
{
    logging.ClearProviders();
    logging.AddConsole();
});

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var seedCardData = services.GetRequiredService<CardsService>();
    var seedTypeData = services.GetRequiredService<TypesService>();
    var seedRarityData = services.GetRequiredService<RaritiesService>();
    var seedSetData = services.GetRequiredService<SetsService>();
    var seedClassData = services.GetRequiredService<ClassesService>();

    seedTypeData.CreateTypes();
    seedRarityData.CreateRarities();
    seedSetData.CreateSets();
    seedClassData.CreateClasses();
    seedCardData.CreateCards();
    
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

//app.UseHttpLogging();

app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllers();

app.Run();
