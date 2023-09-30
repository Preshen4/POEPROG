using Microsoft.AspNetCore.Builder;
using NovelNestLibraryAPI.Models;
using NovelNestLibraryAPI.Services;
using NovelNestLibraryAPI.Controllers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.Configure<NovelNestLibraryDatabaseSettings>(
    builder.Configuration.GetSection("NovelNestLibraryDatabase"));

builder.Services.AddSingleton<UserService>();
builder.Services.AddSingleton<ReplacingBookQuizService>();
builder.Services.AddSingleton<LeaderboardService>();
builder.Services.AddControllers()
    .AddJsonOptions(
        options => options.JsonSerializerOptions.PropertyNamingPolicy = null);
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Change to match the URL of your react app 
app.UseCors(options => options.WithOrigins("http://localhost:3000").AllowAnyMethod().AllowAnyHeader());

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();


app.Run();
