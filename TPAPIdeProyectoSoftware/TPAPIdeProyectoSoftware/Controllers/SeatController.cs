using Application;
using Application.DTOs.User;
using Application.Interfaces.Handler;
using Application.Interfaces.Handlers;
using Application.Interfaces.Handlers.User;
using Application.UseCases;
using Application.UseCases.USER.Commands;
using Application.UseCases.USER.Queries;
using Azure.Core;
using Microsoft.AspNetCore.Mvc;

namespace TPAPIdeProyectoSoftware.Controllers
{
    [ApiController]
    [Route("api/v1/seats")]
    public class SeatController : ControllerBase
    {
        private readonly ICreateSeatHandler _createHandler;
        private readonly IDeleteSeatHandler _deleteHandler;
        private readonly IUpdateSeatHandler _updateHandler;
        private readonly IGetAllSeatHandler _getAllSeatHandler;
        private readonly IGetByIdSeatHandler _getByIdSeatHandler;

        public SeatController(
            ICreateSeatHandler createHandler,
            IDeleteSeatHandler deleteHandler,
            IUpdateSeatHandler updateHandler,
            IGetAllSeatHandler getAllSeatHandler,
            IGetByIdSeatHandler getByIdSeatHandler)
        {
            _createHandler = createHandler;
            _deleteHandler = deleteHandler;
            _updateHandler = updateHandler;
            _getAllSeatHandler = getAllSeatHandler;
            _getByIdSeatHandler = getByIdSeatHandler;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] SeatCreateRequestDto request)
        {
            var command = new CreateSeatCommand(
               request.SectorId,
               request.RowIdentifier,
               request.SeatNumber,
               request.Version

           );

            var message = await _createHandler.Handle(command);

            if (message != "OK")
                return BadRequest(new { message });

            return StatusCode(201, new
            {
                message = "Seat creado correctamente"
            });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {

            var query = new GetByIdSeatQuery(id);

            var (seat, message) = await _getByIdSeatHandler.Handle(query);

            if (message != "OK")
                return NotFound(new { message });

            return Ok(seat);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var query = new GetAllSeatQuery();

            var (seats, message) = await _getAllSeatHandler.Handle(query);

            if (message != "OK")
                return NotFound(new { message });

            return Ok(seats);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var command = new DeleteSeatCommand(id);

            var message = await _deleteHandler.Handle(command);

            if (message == "Seat no encontrado")
                return NotFound(new { message });

            return Ok(new { message });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] SeatRequestDto request)
        {
            var command = new UpdateSeatCommand(
                id,
                request.SectorId,
                request.RowIdentifier,
                request.SeatNumber,
                request.Status,
                request.Version
            );

            var message = await _updateHandler.Handle(command);

            if (message == "Seat no encontrado")
                return NotFound(new { message });

            if (message != "Seat actualizado correctamente")
                return BadRequest(new { message });

            return Ok(new { message });
        }
    }
}