using Application.DAOs;
using Application.Logic;
using Contracts;
using EfcData;
using FileData.DataAccess;

using (PostContext ctx = new())
{
    ctx.Seed();
}

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IUserService, UserSqliteDAO>();
builder.Services.AddScoped<IPostService, PostSqliteDAO>();
builder.Services.AddScoped<IPostDAO, PostFileDAO>();
builder.Services.AddScoped<IUserDAO, UserFileDAO>();
builder.Services.AddScoped<PostContext>();
builder.Services.AddScoped<UserContext>();


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