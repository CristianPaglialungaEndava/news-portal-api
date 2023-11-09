using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using Microsoft.Net.Http.Headers;
using newsPortal.Infrastructure;
using newsPortal.Repositories;
using newsPortal.Repositories.Interfaces;
using newsPortal.Services;
using newsPortal.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Inject Dependencies
builder.Services.AddMemoryCache();
builder.Services.AddScoped<IStoryService, StoryService>();
builder.Services.AddScoped<HackNewService>();
builder.Services.AddScoped<INewsService, CachingHackNewService>();
// Configure HttpClient 
builder.Services.AddHttpClient("HackNews", (httpClient) =>
{
    var baseUrl = builder.Configuration["BaseUrls:HackNews"];
    httpClient.BaseAddress = new Uri(baseUrl);
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
