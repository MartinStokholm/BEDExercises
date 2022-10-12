using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.SignalR;
using ModelManagementAPI.Entities;
using System.Text.Json.Serialization;
using ModelManagementAPI.Hubs;

var builder = WebApplication.CreateBuilder(args);
//builder.Services.AddDbContext<DataContext>(options =>
//    options.UseSqlServer(builder.Configuration.GetConnectionString("RestAPIContext") ?? throw new InvalidOperationException("Connection string 'RestAPIContext' not found.")));
// Use in-memory database for quick dev and testing
builder.Services.AddDbContext<DataContext>(options =>
    options.UseInMemoryDatabase("InMemoryDb"));

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddSignalR();
builder.Services.AddControllers();
// Add to fix circular reference problem with JSON serialization
builder.Services.AddControllers().AddJsonOptions(x =>
    x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapControllers();

app.UseAuthorization();

app.MapRazorPages();

app.MapHub<MessageHub>("/messageHub");

app.Run();
