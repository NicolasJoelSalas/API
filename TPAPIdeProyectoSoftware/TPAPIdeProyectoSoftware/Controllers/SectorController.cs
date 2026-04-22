using Application.DTOs;
using Application.DTOs.User;
using Application.Interfaces.Handler;
using Application.Interfaces.Handlers;
using Application.Interfaces.Handlers.User;
using Application.UseCases;
using Microsoft.AspNetCore.Mvc;

namespace TPAPIdeProyectoSoftware.Controllers
{
    [ApiController]
    [Route("api/Sector")]
    public class SectorController : ControllerBase
    {
        private readonly ICreateSectorHandler _createHandler;
        private readonly IDeleteSectorHandler _deleteHandler;
        private readonly IUpdateSectorHandler _updateHandler;
        private readonly IGetAllSectorHandler _getAllSectorHandler;
        private readonly IGetByIdSectorHandler _getByIdSectorHandler;

        public SectorController(
            IDeleteSectorHandler deleteHandler,
            ICreateSectorHandler createHandler,
            IUpdateSectorHandler updateHandler,
            IGetByIdSectorHandler getByIdSectorHandler,
            IGetAllSectorHandler getAllSectorHandler)
        {
            _createHandler = createHandler;
            _deleteHandler = deleteHandler;
            _updateHandler = updateHandler;
            _getAllSectorHandler = getAllSectorHandler;
            _getByIdSectorHandler = getByIdSectorHandler;
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] SectorRequestDto dto)
        {
            var message = await _createHandler.Handle(dto);

            if (message != "OK")
                return BadRequest(new { message });

            return StatusCode(201, new
            {
                message = "Sector creado correctamente"
            });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var (sectors, message) = await _getByIdSectorHandler.Handle(id);

            if (message != "OK")
                return Ok(new { message });

            return Ok(sectors);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var (sectors, message) = await _getAllSectorHandler.Handle();

            if (message != "OK")
                return Ok(new { message });

            return Ok(sectors);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var message = await _deleteHandler.Handle(id);

            if (message == "Sector no encontrado")
                return NotFound(new { message });

            return Ok(new { message });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] SectorRequestDto dto)
        {
            var message = await _updateHandler.Handle(id, dto);

            if (message == "Sector no encontrado")
                return NotFound(new { message });

            return Ok(new { message });
        }

    }
}
