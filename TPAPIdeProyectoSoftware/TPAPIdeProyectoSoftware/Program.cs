using Application;
using Application.Interfaces;
using Application.Interfaces.Command;
using Application.Interfaces.Command.Event;
using Application.Interfaces.Command.User;
using Application.Interfaces.Commands.Event;
using Application.Interfaces.Handler;
using Application.Interfaces.Handlers;
using Application.Interfaces.Handlers.Event;
using Application.Interfaces.Handlers.Sector;
using Application.Interfaces.Handlers.User;
using Application.Interfaces.Queries;
using Application.Interfaces.Queries.Audit_Log;
using Application.Interfaces.Queries.Event;
using Application.Interfaces.Queries.Sector;
using Application.Interfaces.Queries.User;
using Application.Interfaces.Repositories;
using Application.UseCases;
using Application.UseCases.AUDIT_LOG.Queries;
using Application.UseCases.EVENT.Commands;
using Application.UseCases.EVENT.Handlers;
using Application.UseCases.EVENT.Queries;
using Application.UseCases.SECTOR.Handlers;
using Application.UseCases.SECTOR.Queries;
using Application.UseCases.USER.Commands;
using Application.UseCases.USER.Handlers;
using Application.UseCases.USER.Queries;
using Domain.Entities;
using Infrastructure.Persistence;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

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
builder.Services.AddScoped<IAudit_LogRepository, Audit_LogRepository>();
builder.Services.AddScoped<IReservationRepository, ReservationRepository>();
builder.Services.AddScoped<ISeatRepository, SeatRepository>();
builder.Services.AddScoped<ISectorRepository, SectorRepository>();
builder.Services.AddScoped<IEventRepository, EventRepository>();



// Commands

//Event

builder.Services.AddScoped<ICreateEventCommand, CreateEventCommand>();
builder.Services.AddScoped<IUpdateEventCommand, UpdateEventCommand>();
builder.Services.AddScoped<IDeleteEventCommand, DeleteEventCommand>();


//User
builder.Services.AddScoped<ICreateUserCommand, CreateUserCommand>();
builder.Services.AddScoped<IUpdateUserCommand, UpdateUserCommand>();
builder.Services.AddScoped<IDeleteUserCommand, DeleteUserCommand>();

    //Audit_Log
builder.Services.AddScoped<ICreateAudit_LogCommand, CreateAudit_LogCommand>();
builder.Services.AddScoped<IUpdateAudit_LogCommand, UpdateAudit_LogCommand>();
builder.Services.AddScoped<IDeleteAudit_LogCommand, DeleteAudit_LogCommand>();

    //Reservation
builder.Services.AddScoped<ICreateReservationCommand, CreateReservationCommand>();
builder.Services.AddScoped<IUpdateReservationCommand, UpdateReservationCommand>();
builder.Services.AddScoped<IDeleteReservationCommand, DeleteReservationCommand>();

    //Seat
builder.Services.AddScoped<ICreateSeatCommand, CreateSeatCommand>();
builder.Services.AddScoped<IUpdateSeatCommand, UpdateSeatCommand>();
builder.Services.AddScoped<IDeleteSeatCommand, DeleteSeatCommand>();

    //Sector
builder.Services.AddScoped<ICreateSectorCommand, CreateSectorCommand>();
builder.Services.AddScoped<IUpdateSectorCommand, UpdateSectorCommand>();
builder.Services.AddScoped<IDeleteSectorCommand, DeleteSectorCommand>();


// Querys

// Event 
builder.Services.AddScoped<IGetAllEventQuery, GetAllEventQuery>();
builder.Services.AddScoped<IGetByIdEventQuery, GetByIdEventQuery>();
builder.Services.AddScoped<INameExistsEventQuery, NameExistsEventQuery>();
builder.Services.AddScoped<IGetSectorsByEventQuery, GetSectorsByEventQuery>();




//User
builder.Services.AddScoped<IGetAllUserQuery, GetAllUserQuery>();
builder.Services.AddScoped<IGetByIdUserQuery, GetByIdUserQuery>();
builder.Services.AddScoped<IEmailExistsUserquery, EmailExistsUserQuery>();

    //Audit_Log

builder.Services.AddScoped<IGetAllAudit_LogQuery, GetAllAudit_LogQuery>();
builder.Services.AddScoped<IGetByIdAudit_LogQuery, GetByIdAudit_LogQuery>();
builder.Services.AddScoped<IGetIdUserQueryValidation, GetIdUserQueryValidation>();

    //Reservation

builder.Services.AddScoped<IGetAllReservationQuery, GetAllReservationQuery>();
builder.Services.AddScoped<IGetByIdReservationQuery, GetByIdReservationQuery>();

    // Seat
builder.Services.AddScoped<IGetAllSeatQuery, GetAllSeatQuery>();
builder.Services.AddScoped<IGetByIdSeatQuery, GetByIdSeatQuery>();
builder.Services.AddScoped<IGetEntitySeatQuery, GetEntitySeatQuery>();

// Sector
builder.Services.AddScoped<IGetAllSectorQuery, GetAllSectorQuery>();
builder.Services.AddScoped<IGetByIdSectorQuery, GetByIdSectorQuery>();
builder.Services.AddScoped<IGetSeatsBySectorQuery, GetSeatsBySectorQuery>();

// Handlers

// Event 
builder.Services.AddScoped<ICreateEventHandler, CreateEventHandler>();
builder.Services.AddScoped<IUpdateEventHandler, UpdateEventHandler>();
builder.Services.AddScoped<IDeleteEventHandler, DeleteEventHandler>();
builder.Services.AddScoped<IGetByIdEventHandler, GetEventByIdHandler>();
builder.Services.AddScoped<IGetAllEventHandler, GetAllEventHandler>();
builder.Services.AddScoped<IGetSectorsByEventHandler, GetSectorsByEventHandler>();

//User
builder.Services.AddScoped<ICreateUserHandler, CreateUserHandler>();
builder.Services.AddScoped<IUpdateUserHandler, UpdateUserHandler>();
builder.Services.AddScoped<IDeleteUserHandler, DeleteUserHandler>();
builder.Services.AddScoped<IGetByIdUserHandler, GetByIdUserHandler>();
builder.Services.AddScoped<IGetAllUserHandler, GetAllUserHandler>();
builder.Services.AddScoped<ILoginUserHandler, LoginUserHandler>();
builder.Services.AddScoped<IGetAllUserLoginQuery, GetAllUserLoginQuery>();

    //Audit_Log
builder.Services.AddScoped<ICreateAudit_LogHandler, CreateAudit_LogHandler>();
builder.Services.AddScoped<IUpdateAudit_LogHandler, UpdateAudit_LogHandler>();
builder.Services.AddScoped<IDeleteAudit_LogHandler, DeleteAudit_LogHandler>();
builder.Services.AddScoped<IGetByIdAudit_LogHandler, GetByIdAudit_LogHandler>();
builder.Services.AddScoped<IGetAllAudit_LogHandler, GetAllAudit_LogHandler>();

    //Reservation
builder.Services.AddScoped<ICreateReservationHandler, CreateReservationHandler>();
builder.Services.AddScoped<IUpdateReservationHandler, UpdateReservationHandler>();
builder.Services.AddScoped<IDeleteReservationHandler, DeleteReservationHandler>();
builder.Services.AddScoped<IGetByIdReservationHandler, GetByIdReservationHandler>();
builder.Services.AddScoped<IGetAllReservationHandler, GetAllReservationHandler>();

    //Seat
builder.Services.AddScoped<ICreateSeatHandler, CreateSeatHandler>();
builder.Services.AddScoped<IUpdateSeatHandler, UpdateSeatHandler>();
builder.Services.AddScoped<IDeleteSeatHandler, DeleteSeatHandler>();
builder.Services.AddScoped<IGetByIdSeatHandler, GetByIdSeatHandler>();
builder.Services.AddScoped<IGetAllSeatHandler, GetAllSeatHandler>();

    //Sector
builder.Services.AddScoped<ICreateSectorHandler, CreateSectorHandler>();
builder.Services.AddScoped<IUpdateSectorHandler, UpdateSectorHandler>();
builder.Services.AddScoped<IDeleteSectorHandler, DeleteSectorHandler>();
builder.Services.AddScoped<IGetByIdSectorHandler, GetByIdSectorHandler>();
builder.Services.AddScoped<IGetAllSectorHandler, GetAllSectorHandler>();
builder.Services.AddScoped<IGetSeatsBySectorHandler, GetSeatsBySectorHandler>();


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
