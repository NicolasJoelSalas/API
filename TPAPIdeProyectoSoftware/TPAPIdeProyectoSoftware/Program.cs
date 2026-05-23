
using Application.Interfaces.Handler;
using Application.Interfaces.Handlers;
using Application.Interfaces.Handlers.Event;
using Application.Interfaces.Handlers.Reservation;
using Application.Interfaces.Handlers.Sector;
using Application.Interfaces.Handlers.User;
using Application.Interfaces.Repositories;
using Application.UseCases;
using Application.UseCases.Audit_Log.Handlers;
using Application.UseCases.Event.Handlers;
using Application.UseCases.EVENT.Handlers;
using Application.UseCases.EVENT.Queries;
using Application.UseCases.RESERVATION.Handlers;
using Application.UseCases.SECTOR.Handlers;
using Application.UseCases.USER.Handlers;
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
builder.Services.AddScoped<IAudit_LogRepository, Audit_LogRepository>();
builder.Services.AddScoped<IReservationRepository, ReservationRepository>();
builder.Services.AddScoped<ISeatRepository, SeatRepository>();
builder.Services.AddScoped<ISectorRepository, SectorRepository>();
builder.Services.AddScoped<IEventRepository, EventRepository>();


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
builder.Services.AddScoped<IConfirmPaymentHandler, ConfirmPaymentHandler>();

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

builder.Services.AddHostedService<WorkerReservationExpired>();

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
