using Application.DTOs.Reservation;
using Application.Interfaces.Handlers.Reservation;
using Application.Interfaces.Handlers.User;
using Application.UseCases.RESERVATION.Commands;
using Application.UseCases.USER.Commands;
using Application.UseCases.USER.Queries;
using Microsoft.AspNetCore.Mvc;

namespace TPAPIdeProyectoSoftware.Controllers
{
    [ApiController]
    [Route("api/reservations")]
    public class ReservationController : ControllerBase
    {
        private readonly ICreateReservationHandler _createHandler;
        private readonly IDeleteReservationHandler _deleteHandler;
        private readonly IGetAllReservationHandler _getAllHandler;
        private readonly IGetByIdReservationHandler _getByIdHandler;
        private IConfirmPaymentHandler _confirmPaymentHandler;


        public ReservationController(
            ICreateReservationHandler createHandler,
            IDeleteReservationHandler deleteHandler,
            IGetAllReservationHandler getAllHandler,
            IGetByIdReservationHandler getByIdHandler,
            IConfirmPaymentHandler confirmPaymentHandler)
        {
            _createHandler = createHandler;
            _deleteHandler = deleteHandler;
            _getAllHandler = getAllHandler;
            _getByIdHandler = getByIdHandler;
            _confirmPaymentHandler = confirmPaymentHandler;

        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateReservationRequestDto request)
        {
            if (request == null)
                return BadRequest("Datos inválidos");

            var command = new CreateReservationCommand(
                request.UserId,
                request.SeatIds
            );

            var reservationIds = await _createHandler.Handle(command);

            return CreatedAtAction(
                nameof(Create),
                new { id = reservationIds },
                new
                {
                    message = "Reservation creada correctamente",
                    reservationId = reservationIds
                });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var query = new GetByIdReservationQuery(id);

            var (reservation, message) = await _getByIdHandler.Handle(query);

            if (message != "OK")
                return NotFound(new { message });

            return Ok(reservation);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var query = new GetAllReservationQuery();

            var (reservations, message) = await _getAllHandler.Handle(query);

            if (message != "OK")
                return NotFound(new { message });

            return Ok(reservations);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var command = new DeleteReservationCommand(id);

            var message = await _deleteHandler.Handle(command);

            if (message == "Reservation no encontrado")
                return NotFound(new { message });

            return Ok(new { message });
        }


        [HttpPost("confirm-payment")]
        public async Task<IActionResult> ConfirmPayment([FromBody] ConfirmPaymentDto dto)
        {
            if (dto == null || dto.ReservationIds == null || !dto.ReservationIds.Any())
            {
                return BadRequest(new
                {
                    message = "No se enviaron reservas"
                });
            }

            var result = await _confirmPaymentHandler.Handle(dto.ReservationIds);

            return Ok(new
            {
                message = result
            });
        }
    }
}