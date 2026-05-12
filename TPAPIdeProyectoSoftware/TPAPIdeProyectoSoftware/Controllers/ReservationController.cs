using Application.DTOs.Reservation;
using Application.DTOs.User;
using Application.Interfaces.Handlers.Reservation;
using Application.Interfaces.Handlers.User;
using Application.UseCases.USER.Handlers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;

namespace TPAPIdeProyectoSoftware.Controllers
{
    [ApiController]
    [Route("api/Reservation")]
    public class ReservationController : ControllerBase
    {
        private readonly ICreateMultipleReservationHandler _createlishandler;

        private readonly ICreateReservationHandler _createHandler;
        private readonly IDeleteReservationHandler _deleteHandler;
        private readonly IUpdateReservationHandler _updateHandler;
        private readonly IGetAllReservationHandler _getAllReservationHandler;
        private readonly IGetByIdReservationHandler _getByIdReservationHandler;
        private readonly IConfirmPaymentHandler _confirmPaymentHandler;

        public ReservationController(
        IDeleteReservationHandler deleteHandler,
        ICreateReservationHandler createHandler,
        IUpdateReservationHandler updateHandler,
        IGetByIdReservationHandler getByIdReservationHandler,
        IGetAllReservationHandler getAllReservationHandler,
        ICreateMultipleReservationHandler createlishandler,
        IConfirmPaymentHandler confirmPaymentHandler)
        {
            _createHandler = createHandler;
            _deleteHandler = deleteHandler;
            _updateHandler = updateHandler;
            _getAllReservationHandler = getAllReservationHandler;
            _getByIdReservationHandler = getByIdReservationHandler;
            _createlishandler = createlishandler;
            _confirmPaymentHandler = confirmPaymentHandler;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ReservationRequestDto dto)
        {
            var message = await _createHandler.Handle(dto);

            if (message != "OK")
                return StatusCode(409, new { message });

            return StatusCode(201, new
            {
                message = "Reservation creado correctamente"
            });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var (users, message) = await _getByIdReservationHandler.Handle(id);

            if (message != "OK")
                return Ok(new { message });

            return Ok(users);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var (users, message) = await _getAllReservationHandler.Handle();

            if (message != "OK")
                return Ok(new { message });

            return Ok(users);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var message = await _deleteHandler.Handle(id);

            if (message == "Reservation no encontrado")
                return NotFound(new { message });

            return Ok(new { message });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateReservationRequestDto dto)
        {
            var message = await _updateHandler.Handle(id, dto);

            if (message == "Reservation no encontrado")
                return NotFound(new { message });

            return Ok(new { message });
        }

        [HttpPost("multiple")]
        public async Task<IActionResult> CreateMultiple([FromBody] CreateMultipleReservationDto dto)
        {
            try
            {
                var reservationIds = await _createlishandler.Handle(dto);

                return StatusCode(201, reservationIds);
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return Conflict(new
                {
                    error = "La butaca ya fue reservada por otro usuario.",
                    detail = ex.Message
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    error = ex.Message
                });
            }
        }

        [HttpPost("confirm-payment")]
        public async Task<IActionResult> ConfirmPayment([FromBody] ConfirmPaymentDto dto)
        {
            if (dto == null || dto.ReservationIds == null || !dto.ReservationIds.Any())
                return BadRequest(new { message = "No se enviaron reservas" });

            var result = await _confirmPaymentHandler.Handle(dto.ReservationIds);

            return StatusCode(204, new { message = result });
        }
    }
}
