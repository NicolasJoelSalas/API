using Application.DTOs.User;
using Application.Interfaces.Command;
using Application.Interfaces.Command.User;
using Application.Interfaces.Handlers.User;
using Application.Interfaces.Queries.User;
using Application.UseCases.USER.Queries;
using Domain.Entities;
using Domain.Enums;

namespace Application.UseCases
{
    public class UpdateReservationHandler : IUpdateReservationHandler
    {
        private readonly IUpdateReservationCommand _command;
        private readonly IGetByIdReservationQuery _query;
        private readonly IGetByIdUserQuery _queryUser;
        private readonly IGetByIdSeatQuery _querySeat;
        private readonly IGetAllReservationQuery _getAllReservationQuery;

        public UpdateReservationHandler(
            IUpdateReservationCommand command,
            IGetByIdReservationQuery query,
            IGetByIdUserQuery queryUser,
            IGetByIdSeatQuery querySeat,
            IGetAllReservationQuery getAllReservationQuery)
        {
            _command = command;
            _query = query;
            _queryUser = queryUser;
            _querySeat = querySeat;
            _getAllReservationQuery = getAllReservationQuery;
        }

        public async Task<string> Handle(Guid id, UpdateReservationRequestDto dto)
        {
            if (dto == null)
                return "Datos inválidos";

            if (dto.UserId <= 0)
                return "El Id del usuario es obligatorio";

            var user = await _queryUser.GetById(dto.UserId);

            if (user == null)
                return "El Usuario no existe";

            var seat = await _querySeat.GetById(dto.SeatId);

            if (seat == null)
                return "El asiento no existe";

            var listaDeReservaciones = await _getAllReservationQuery.GetAll();
            foreach (var elemento in listaDeReservaciones)
            {
                if (elemento.SeatId == dto.SeatId)
                    return "El asiento ya se encuentra reservado";
            }

            if (dto.Status != ReservationStatus.Pending.ToString() &&
                dto.Status != ReservationStatus.Paid.ToString() &&
                dto.Status != ReservationStatus.Expired.ToString())
            {
                return "Estado de reserva inválido";
            }

            var Reservation = new RESERVATION
            {
                Id = id,
                UserId = dto.UserId,
                SeatId = dto.SeatId,
                Status = dto.Status,
                ReservedAt = DateTime.UtcNow,
                ExpiresAt = DateTime.UtcNow.AddMinutes(5),
            };

            await _command.ExecuteUpdateReservation(Reservation);

            return "Update actualizado correctamente";
        }
    }
}