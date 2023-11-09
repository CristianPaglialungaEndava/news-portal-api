using Microsoft.Extensions.Options;
using Microsoft.Net.Http.Headers;
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

builder.Services.AddScoped<IStoryService, StoryService>();
builder.Services.AddScoped<INewsService, HackNewService>();
// Configure HttpClient 
builder.Services.AddHttpClient("HackNews", (httpClient) =>
{
    var baseUrl = "https://hacker-news.firebaseio.com/v0/";//builder.Configuration.GetValue<string>("BaseUrls:HackNews") ?? string.Empty;

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
