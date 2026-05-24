using Application;
using Application.DTOs;
using Application.Interfaces.Handler;
using Application.Interfaces.Handlers;
using Application.UseCases;
using Microsoft.AspNetCore.Mvc;

namespace TPAPIdeProyectoSoftware.Controllers
{
    [ApiController]
    [Route("api/v1/sectors")]
    public class SectorController : ControllerBase
    {
        private readonly ICreateSectorHandler _createSectorHandler;
        private readonly IDeleteSectorHandler _deleteSectorHandler;
        private readonly IUpdateSectorHandler _updateSectorHandler;
        private readonly IGetAllSectorHandler _getAllSectorHandler;
        private readonly IGetByIdSectorHandler _getByIdSectorHandler;

        public SectorController(
            ICreateSectorHandler createSectorHandler,
            IDeleteSectorHandler deleteSectorHandler,
            IUpdateSectorHandler updateSectorHandler,
            IGetAllSectorHandler getAllSectorHandler,
            IGetByIdSectorHandler getByIdSectorHandler)
        {
            _createSectorHandler = createSectorHandler;
            _deleteSectorHandler = deleteSectorHandler;
            _updateSectorHandler = updateSectorHandler;
            _getAllSectorHandler = getAllSectorHandler;
            _getByIdSectorHandler = getByIdSectorHandler;
        }

        [HttpPost]
        public async Task<IActionResult> CreateSector([FromBody] SectorRequestDto request)
        {
            var command = new CreateSectorCommand(
                request.EventId,
                request.Name,
                request.Price,
                request.Capacity
            );

            var message = await _createSectorHandler.Handle(command);

            if (message != "OK")
                return BadRequest(new { message });

            return StatusCode(201, new
            {
                message = "Sector creado correctamente"
            });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetSectorById(int id)
        {
            var query = new GetByIdSectorQuery(id);

            var (sector, message) = await _getByIdSectorHandler.Handle(query);

            if (message != "OK")
                return NotFound(new { message });

            return Ok(sector);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllSectors()
        {
            var query = new GetAllSectorQuery();

            var (sectors, message) = await _getAllSectorHandler.Handle(query);

            if (message != "OK")
                return NotFound(new { message });

            return Ok(sectors);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSector(int id)
        {
            var command = new DeleteSectorCommand(id);

            var message = await _deleteSectorHandler.Handle(command);

            if (message == "Sector no encontrado")
                return NotFound(new { message });

            return StatusCode(204, new { message });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSector(int id, [FromBody] SectorRequestDto request)
        {
            var command = new UpdateSectorCommand(
                id,
                request.EventId,
                request.Name,
                request.Price,
                request.Capacity  
            );

            var message = await _updateSectorHandler.Handle(command);

            if (message == "Sector no encontrado")
                return NotFound(new { message });

            if (message != "Sector actualizado correctamente")
                return BadRequest(new { message });

            return StatusCode(204, new { message });
        }
    }
}