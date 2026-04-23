using Application.DTOs.Event;
using Application.DTOs.User;
using Application.Interfaces.Handlers.Event;
using Application.UseCases;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace TPAPIdeProyectoSoftware.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventController : ControllerBase
    {
        private readonly ICreateEventHandler _createEventHandler;
        private readonly IDeleteEventHandler _deleteEventHandler;
        private readonly IUpdateEventHandler _updateEventHandler;
        private readonly IGetAllEventHandler _getAllEventHandler;
        private readonly IGetByIdEventHandler _getByIdEventHandler;

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

    }
}