using Application.Interfaces.Handlers.Reservation;
using Domain.Entities;
using Domain.Enums;

namespace Application.UseCases.User.Handlers
{
    public class ConfirmPaymentHandler /*: IConfirmPaymentHandler*/
    {
        //private readonly IUpdateReservationsStatusCommand _reservationCommand;
        //private readonly IMarkSeatsAsSoldCommand _seatCommand;
        //private readonly ICreateAudit_LogCommand _auditLogCommand;
        //private readonly IGetByIdReservationQuery _query;

        //public ConfirmPaymentHandler(
        //    IUpdateReservationsStatusCommand reservationCommand,
        //    IMarkSeatsAsSoldCommand seatCommand,
        //    ICreateAudit_LogCommand auditLogCommand,
        //    IGetByIdReservationQuery query)
        //{
        //    _reservationCommand = reservationCommand;
        //    _seatCommand = seatCommand;
        //    _auditLogCommand = auditLogCommand;
        //    _query = query;
        //}

        //public async Task<string> Handle(List<Guid> reservationIds)
        //{
        //    if (reservationIds == null || !reservationIds.Any())
        //        return "No hay reservas para procesar";

        //    // 🔹 Obtener una reserva para UserId
        //    var reservation = await _query.GetById(reservationIds.First());

        //    if (reservation == null)
        //        return "Reserva no encontrada";

        //    var userId = reservation.UserId;

        //    // 🔥 1. Reservas → Paid
        //    await _reservationCommand.Execute(
        //        reservationIds,
        //        ReservationStatus.Paid.ToString()
        //    );

        //    // 🔥 2. Seats → Sold
        //    await _seatCommand.Execute(reservationIds);

        //    // 🔥 3. Audit log
        //    var auditLog = new Domain.Entities.AUDIT_LOG
        //    {
        //        Action = "Pago Confirmado",
        //        UserId = userId,
        //        EntityType = "Reservation",
        //        EntityId = string.Join(",", reservationIds),
        //        Details = $"Pago confirmado para {reservationIds.Count} reservas",
        //        CreatedAt = DateTime.UtcNow,
        //    };

        //    await _auditLogCommand.ExecuteCreateAudit_Log(auditLog);

        //    return "Pago confirmado y reservas actualizadas";
        //}
    }
}