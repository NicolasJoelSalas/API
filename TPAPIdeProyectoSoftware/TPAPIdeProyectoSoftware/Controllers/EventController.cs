using Application.DTOs.Event;
using Application.DTOs.User;
using Application.Interfaces.Handlers.Event;
using Application.Interfaces.Handlers.Sector;
using Application.UseCases;
using Application.UseCases.SECTOR.Handlers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace TPAPIdeProyectoSoftware.Controllers
{
    [ApiController]
    [Route("api/Event")]
    public class EventController : ControllerBase
    {
        private readonly ICreateEventHandler _createEventHandler;
        private readonly IDeleteEventHandler _deleteEventHandler;
        private readonly IUpdateEventHandler _updateEventHandler;
        private readonly IGetAllEventHandler _getAllEventHandler;
        private readonly IGetByIdEventHandler _getByIdEventHandler;
        private readonly IGetSectorsByEventHandler _getSectorsByEventHandler;
        private readonly IGetSeatsBySectorHandler _getSeatsBySectorHandler;


        public EventController(
        ICreateEventHandler createEventHandler,
        IDeleteEventHandler deleteEventHandler,
        IUpdateEventHandler updateEventHandler,
        IGetAllEventHandler getAllEventHandler,
        IGetByIdEventHandler getByIdEventHandler,
        IGetSectorsByEventHandler getSectorsByEventHandler,
        IGetSeatsBySectorHandler getSeatsBySectorHandler)
        {
            _createEventHandler = createEventHandler;
            _deleteEventHandler = deleteEventHandler;
            _updateEventHandler = updateEventHandler;
            _getAllEventHandler = getAllEventHandler;
            _getByIdEventHandler = getByIdEventHandler;

            _getSectorsByEventHandler = getSectorsByEventHandler;
            _getSeatsBySectorHandler = getSeatsBySectorHandler;
        }

        [HttpPost]
        public async Task<IActionResult> CreateEvent([FromBody] EventResquestDto request)
        {
            var mensaje = await _createEventHandler.CreateEventHandle(request);

            if (mensaje != "OK")
                return BadRequest(new { mensaje });

            return StatusCode(201, new
            {
                mensaje = "Evento creado correctamente"
            });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetEventById(int id)
        {
            var (events, mensaje) = await _getByIdEventHandler.GetByIdEventHandle(id);

            if (mensaje != "OK")
                return Ok(new { mensaje });

            return Ok(events);
        }


        [HttpGet]
        public async Task<IActionResult> GetAllEvents()
        {
            var (events, mensaje) = await _getAllEventHandler.GetAllEventHandle();
            if (mensaje != "OK")
                return Ok(new { mensaje });
            return Ok(events);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var mensaje = await _deleteEventHandler.DeleteEventHandle(id);

            if (mensaje == "El evento no existe")
                return NotFound(new { mensaje });

            return Ok(new { mensaje });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEvent(int id, [FromBody] EventResquestDto request)
        {
            var mensaje = await _updateEventHandler.UpdateEventHandle(id, request);
            if (mensaje == "Evento no encontrado")
                return NotFound(new { mensaje });
            return Ok(new { mensaje });
        }

        [HttpGet("{eventId}/sectors")]
        public async Task<IActionResult> GetSectorsByEvent(int eventId)
        {
            var (sectors, message) =
                await _getSectorsByEventHandler.Handle(eventId);

            if (message != "OK")
                return NotFound(new { message });

            return Ok(sectors);
        }

        [HttpGet("sector/{sectorId}/seats")]
        public async Task<IActionResult> GetSeatsBySector(int sectorId)
        {
            var (seats, message) =
                await _getSeatsBySectorHandler.Handle(sectorId);

            if (message != "OK")
                return NotFound(new { message });

            return Ok(seats);
        }

    }
}