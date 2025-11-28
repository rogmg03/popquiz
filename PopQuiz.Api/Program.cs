using Microsoft.EntityFrameworkCore;
using PopQuiz.Core.BusinessLogic;
using PopQuiz.Data.Models;
using PopQuiz.Data.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configure DbContext with connection string from appsettings
builder.Services.AddDbContext<HallOfFameContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("HallOfFame")));

//Business Logic

builder.Services.AddScoped<IMovieBusiness, MovieBusiness>();
builder.Services.AddScoped<IGenreBusiness, GenreBusiness>();
builder.Services.AddScoped<IDirectorBusiness, DirectorBusiness>();
builder.Services.AddScoped<IUserBusiness, UserBusiness>();


//Repositories

builder.Services.AddScoped<IRepositoryMovie, RepositoryMovie>();
builder.Services.AddScoped<IRepositoryGenre, RepositoryGenre>();
builder.Services.AddScoped<IRepositoryDirector, RepositoryDirector>();
builder.Services.AddScoped<IRepositoryUser, RepositoryUser>();


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
