using Application.DAOs;
using Application.Logic;
using Contracts;
using FileData.DataAccess;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IUserService, UserServiceImpl>();
builder.Services.AddScoped<IPostService, PostServiceImpl>();
builder.Services.AddScoped<IPostDAO, PostFileDAO>();
builder.Services.AddScoped<IUserDAO, UserFileDAO>();
builder.Services.AddScoped<JsonContext>();

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