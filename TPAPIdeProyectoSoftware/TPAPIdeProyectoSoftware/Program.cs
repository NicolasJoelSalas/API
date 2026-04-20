using Application;
using Application.Interfaces.Command.User;
using Application.Interfaces.Handlers.User;
using Application.Interfaces.Queries.User;
using Application.Interfaces.Repositories;
using Application.UseCases.USER.Commands;
using Application.UseCases.USER.Handlers;
using Application.UseCases.USER.Queries;
using Infrastructure.Persistence;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>
    (options => options.UseSqlServer
    (builder.Configuration.GetConnectionString("DefaultConnection")));

// Repositories
builder.Services.AddScoped<IUserRepository, UserRepository>();

// Commands
builder.Services.AddScoped<ICreateUserCommand, CreateUserCommand>();
builder.Services.AddScoped<IUpdateUserCommand, UpdateUserCommand>();
builder.Services.AddScoped<IDeleteUserCommand, DeleteUserCommand>();

// Querys

builder.Services.AddScoped<IGetAllUserQuery, GetAllUserQuery>();
builder.Services.AddScoped<IGetByIdUserQuery, GetByIdUserQuery>();
builder.Services.AddScoped<IEmailExistsUserquery, EmailExistsUserQuery>();

// Handlers
builder.Services.AddScoped<ICreateUserHandler, CreateUserHandler>();
builder.Services.AddScoped<IUpdateUserHandler, UpdateUserHandler>();
builder.Services.AddScoped<IDeleteUserHandler, DeleteUserHandler>();
builder.Services.AddScoped<IGetByIdUserHandler, GetByIdUserHandler>();
builder.Services.AddScoped<IGetAllUserHandler, GetAllUserHandler>();



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
