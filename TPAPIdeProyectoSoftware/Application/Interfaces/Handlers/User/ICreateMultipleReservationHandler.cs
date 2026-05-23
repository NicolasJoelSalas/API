using Application.DTOs.User;
using Application.UseCases.RESERVATION.Commands;
using System;

namespace Application.Interfaces.Handlers.User
{
    public interface ICreateMultipleReservationHandler
    {
       Task<List<Guid>> Handle(CreateReservationCommand dto);
    }
}
