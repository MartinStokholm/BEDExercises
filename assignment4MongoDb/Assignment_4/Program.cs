using Assignment_4.Models;
using Assignment_4.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.Configure<MongoDbSettings>(
    builder.Configuration.GetSection("MongoDbSettings"));

builder.Services.AddSingleton<TypesService>();
builder.Services.AddSingleton<RaritiesService>();
builder.Services.AddSingleton<SetsService>();
builder.Services.AddSingleton<ClassesService>();
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
    
    var seedTypeData = services.GetRequiredService<TypesService>();
    seedTypeData.CreateTypes();

    var seedRarityData = services.GetRequiredService<RaritiesService>();
    seedRarityData.CreateRarities();

    var seedSetData = services.GetRequiredService<SetsService>();
    seedSetData.CreateSets();

    var seedClassData = services.GetRequiredService<ClassesService>();
    seedClassData.CreateClasses();

    var seedCardData = services.GetRequiredService<CardsService>();
    seedCardData.CreateCards();
    
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllers();

app.Run();

