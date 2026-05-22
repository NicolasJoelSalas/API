using Application.DTOs.User;
using System;

namespace Application.Interfaces.Handlers.User
{
    public interface ICreateMultipleReservationHandler
    {
       Task<List<Guid>> Handle(CreateMultipleReservationDto dto);
    }
}
