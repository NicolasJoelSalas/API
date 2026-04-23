using Application.DTOs;
using Application.DTOs.User;
using Application.Interfaces.Handlers.User;
using Application.UseCases.USER.Handlers;
using Microsoft.AspNetCore.Mvc;

namespace TPAPIdeProyectoSoftware.Controllers
{
    [ApiController]
    [Route("api/Audit_Logs")]
    public class Audit_LogController : ControllerBase
    {
        private readonly ICreateAudit_LogHandler _createHandler;
        private readonly IDeleteAudit_LogHandler _deleteHandler;
        private readonly IUpdateAudit_LogHandler _updateHandler;
        private readonly IGetAllAudit_LogHandler _getAllAudit_LogHandler;
        private readonly IGetByIdAudit_LogHandler _getByIdAudit_LogHandler;

        public Audit_LogController(
            IDeleteAudit_LogHandler deleteHandler,
            ICreateAudit_LogHandler createHandler,
            IUpdateAudit_LogHandler updateHandler,
            IGetByIdAudit_LogHandler getByIdAudit_LogHandler,
            IGetAllAudit_LogHandler getAllAudit_LogHandler)
        {
            _createHandler = createHandler;
            _deleteHandler = deleteHandler;
            _updateHandler = updateHandler;
            _getAllAudit_LogHandler = getAllAudit_LogHandler;
            _getByIdAudit_LogHandler = getByIdAudit_LogHandler;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Audit_LogRequestDto dto)
        {
            var message = await _createHandler.Handle(dto);

            if (message != "OK")
                return BadRequest(new { message });

            return StatusCode(201, new
            {
                message = "Audit_Log creado correctamente"
            });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var (users, message) = await _getByIdAudit_LogHandler.Handle(id);

            if (message != "OK")
                return Ok(new { message });

            return Ok(users);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var (users, message) = await _getAllAudit_LogHandler.Handle();

            if (message != "OK")
                return Ok(new { message });

            return Ok(users);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var message = await _deleteHandler.Handle(id);

            if (message == "Audit_Log no encontrado")
                return NotFound(new { message });

            return Ok(new { message });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] Audit_LogRequestDto dto)
        {
            var message = await _updateHandler.Handle(id, dto);

            if (message == "Audit_Log no encontrado")
                return NotFound(new { message });

            return Ok(new { message });
        }
    }
}
