using Application.DTOs.Event;
using Application.Interfaces.Handlers.Event;
using Application.UseCases.EVENT.Commands;
using Application.UseCases.EVENT.Queries;
using Application.UseCases.USER.Commands;
using Microsoft.AspNetCore.Mvc;

namespace TPAPIdeProyectoSoftware.Controllers
{
    [ApiController]
    [Route("api/events")]
    public class EventController : ControllerBase
    {
        private readonly ICreateEventHandler _createEventHandler;
        private readonly IDeleteEventHandler _deleteEventHandler;
        private readonly IUpdateEventHandler _updateEventHandler;
        private readonly IGetAllEventHandler _getAllEventHandler;
        private readonly IGetByIdEventHandler _getByIdEventHandler;
        //private readonly IGetSectorsByEventHandler _getSectorsByEventHandler;
        //private readonly IGetSeatsBySectorHandler _getSeatsBySectorHandler;

        public EventController(
            ICreateEventHandler createEventHandler,
            IDeleteEventHandler deleteEventHandler,
            IUpdateEventHandler updateEventHandler,
            IGetAllEventHandler getAllEventHandler,
            IGetByIdEventHandler getByIdEventHandler)
        {
            _createEventHandler = createEventHandler;
            _deleteEventHandler = deleteEventHandler;
            _updateEventHandler = updateEventHandler;
            _getAllEventHandler = getAllEventHandler;
            _getByIdEventHandler = getByIdEventHandler;

        }

        [HttpPost]
        public async Task<IActionResult> CreateEvent([FromBody] EventResquestDto request)
        {
            var command = new CreateEventCommand(
                request.Name,
                request.EventDate,
                request.Venue
            );

            var message = await _createEventHandler.Handle(command);

            if (message != "OK")
                return BadRequest(new { message });

            return StatusCode(201, new
            {
                message = "Evento creado correctamente"
            });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetEventById(int id)
        {
            var query = new GetByIdEventQuery(id);

            var (eventDto, message) = await _getByIdEventHandler.Handle(query);

            if (message != "OK")
                return NotFound(new { message });

            return Ok(eventDto);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllEvents()
        {
            var query = new GetAllEventQuery();

            var (events, message) = await _getAllEventHandler.Handle(query);

            if (message != "OK")
                return NotFound(new { message });

            return Ok(events);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var command = new DeleteEventCommand(id);

            var message = await _deleteEventHandler.Handle(command);

            if (message == "Evento no encontrado")
                return NotFound(new { message });

            return Ok(new { message });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEvent(int id, [FromBody] EventUpdateResquestDto request)
        {
            var command = new UpdateEventCommand(
                id,
                request.Name,
                request.EventDate,
                request.Venue,
                request.Status
            );

            var message = await _updateEventHandler.Handle(command);

            if (message == "Evento no encontrado")
                return NotFound(new { message });

            if (message != "Evento actualizado correctamente")
                return BadRequest(new { message });

            return Ok(new { message });
        }

        //[HttpGet("{eventId}/sectors")]
        //public async Task<IActionResult> GetSectorsByEvent(int eventId)
        //{
        //    var query = new GetSectorsByEventQuery(eventId);

        //    var (sectors, message) = await _getSectorsByEventHandler.Handle(query);

        //    if (message != "OK")
        //        return NotFound(new { message });

        //    return Ok(sectors);
        //}

        //[HttpGet("sector/{sectorId}/seats")]
        //public async Task<IActionResult> GetSeatsBySector(int sectorId)
        //{
        //    var query = new GetSeatsBySectorQuery(sectorId);

        //    var (seats, message) = await _getSeatsBySectorHandler.Handle(query);

        //    if (message != "OK")
        //        return NotFound(new { message });

        //    return Ok(seats);
        //}
    }
}