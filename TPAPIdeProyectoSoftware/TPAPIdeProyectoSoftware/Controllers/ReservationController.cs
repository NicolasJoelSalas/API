using Application.DTOs.Reservation;
using Application.DTOs.User;
using Application.Interfaces.Handlers.Reservation;
using Application.Interfaces.Handlers.User;
using Application.UseCases;
using Application.UseCases.RESERVATION.Commands;
using Application.UseCases.RESERVATION.Queries;
using Application.UseCases.USER.Commands;
using Application.UseCases.USER.Queries;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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



        public ReservationController(
            ICreateReservationHandler createHandler,
            IDeleteReservationHandler deleteHandler,
            IGetAllReservationHandler getAllHandler,
            IGetByIdReservationHandler getByIdHandler)
        {
            _createHandler = createHandler;
            _deleteHandler = deleteHandler;
            _getAllHandler = getAllHandler;
            _getByIdHandler = getByIdHandler;

        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateReservationRequestDto request)
        {
            if (request == null)
                return BadRequest("Datos inválidos");

            var command = new CreateReservationCommand(
                request.UserId,
                request.SeatId
            );

            Guid reservationId = await _createHandler.Handle(command);

            return CreatedAtAction(
                nameof(Create),
                new { id = reservationId },
                new
                {
                    message = "Reservation creada correctamente",
                    reservationId = reservationId
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

        //[HttpPost("multiple")]
        //public async Task<IActionResult> CreateMultiple([FromBody] CreateMultipleReservationDto dto)
        //{
        //    try
        //    {
        //        var command = new CreateMultipleReservationCommand(dto);

        //        var result = await _createMultipleHandler.Handle(command);

        //        return StatusCode(201, result);
        //    }
        //    catch (DbUpdateConcurrencyException ex)
        //    {
        //        return Conflict(new
        //        {
        //            error = "La butaca ya fue reservada por otro usuario.",
        //            detail = ex.Message
        //        });
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(new
        //        {
        //            error = ex.Message
        //        });
        //    }
        //}

        //[HttpPost("confirm-payment")]
        //public async Task<IActionResult> ConfirmPayment([FromBody] ConfirmPaymentDto dto)
        //{
        //    if (dto == null || dto.ReservationIds == null || !dto.ReservationIds.Any())
        //        return BadRequest(new { message = "No se enviaron reservas" });

        //    var command = new ConfirmPaymentCommand(dto.ReservationIds);

        //    var result = await _confirmPaymentHandler.Handle(command);

        //    return NoContent();
        //}
    }
}