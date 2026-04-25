
using Application.DTOs.User;
using Application.Interfaces.Handlers.User;
using Application.UseCases;
using Microsoft.AspNetCore.Mvc;

namespace TPAPIdeProyectoSoftware.Controllers
{
    [ApiController]
    [Route("api/Seat")]
    public class SeatController : ControllerBase
    {
        private readonly ICreateSeatHandler _createHandler;
        private readonly IDeleteSeatHandler _deleteHandler;
        private readonly IUpdateSeatHandler _updateHandler;
        private readonly IGetAllSeatHandler _getAllSeatHandler;
        private readonly IGetByIdSeatHandler _getByIdSeatHandler;

        public SeatController(
            IDeleteSeatHandler deleteHandler,
            ICreateSeatHandler createHandler,
            IUpdateSeatHandler updateHandler,
            IGetByIdSeatHandler getByIdSeatHandler,
            IGetAllSeatHandler getAllSeatHandler)
        {
            _createHandler = createHandler;
            _deleteHandler = deleteHandler;
            _updateHandler = updateHandler;
            _getAllSeatHandler = getAllSeatHandler;
            _getByIdSeatHandler = getByIdSeatHandler;
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] IdSeatRequestDto dto)
        {
            var message = await _createHandler.Handle(dto);

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
            var (users, message) = await _getByIdSeatHandler.Handle(id);

            if (message != "OK")
                return Ok(new { message });

            return Ok(users);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var (users, message) = await _getAllSeatHandler.Handle();

            if (message != "OK")
                return Ok(new { message });

            return Ok(users);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var message = await _deleteHandler.Handle(id);

            if (message == "Seat no encontrado")
                return NotFound(new { message });

            return Ok(new { message });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] SeatRequestDto dto)
        {
            var message = await _updateHandler.Handle(id, dto);

            if (message == "Seat no encontrado")
                return NotFound(new { message });

            return Ok(new { message });
        }

    }
}
