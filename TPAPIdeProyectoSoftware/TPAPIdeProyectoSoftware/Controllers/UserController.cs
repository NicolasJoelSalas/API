using Application.DTOs.User;
using Application.Interfaces.Handlers.User;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;

namespace TPAPIdeProyectoSoftware.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UserController : ControllerBase
    {
        private readonly ICreateUserHandler _createHandler;
        private readonly IDeleteUserHandler _deleteHandler;
        private readonly IUpdateUserHandler _updateHandler;
        private readonly IGetAllUserHandler _getAllUserHandler;
        private readonly IGetByIdUserHandler _getByIdUserHandler;
        private readonly ILoginUserHandler _loginUserHandler;

        public UserController(
            IDeleteUserHandler deleteHandler,
            ICreateUserHandler createHandler,
            IUpdateUserHandler updateHandler,
            IGetByIdUserHandler getByIdUserHandler,
            IGetAllUserHandler getAllUserHandler,
            ILoginUserHandler loginUserHandler  )
        {
            _createHandler = createHandler;
            _deleteHandler = deleteHandler;
            _updateHandler = updateHandler;
            _getAllUserHandler = getAllUserHandler;
            _getByIdUserHandler = getByIdUserHandler;
            _loginUserHandler = loginUserHandler;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] UserRequestDto dto)
        {
            var message = await _createHandler.Handle(dto);

            if (message != "OK")
                return StatusCode(409,new { message });

            return StatusCode(201, new
            {
                message = "Usuario creado correctamente"
            });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var (users, message) = await _getByIdUserHandler.Handle(id);

            if (message != "OK")
                return Ok(new { message });

            return Ok(users);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var (users, message) = await _getAllUserHandler.Handle();

            if (message != "OK")
                return Ok(new { message });

            return Ok(users);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var message = await _deleteHandler.Handle(id);

            if (message == "Usuario no encontrado")
                return NotFound(new { message });

            return Ok(new { message });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UserRequestDto dto)
        {
            var message = await _updateHandler.Handle(id, dto);

            if (message == "Usuario no encontrado")
                return NotFound(new { message });

            return Ok(new { message });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] Application.DTOs.User.LoginRequest dto)
        {
            var result = await _loginUserHandler.Handle(dto);

            if (!result.Success)
                return Unauthorized(new { message = result.Message });

            return Ok(new
            {
                message = result.Message,
                userId = result.UserId,
                username = result.Username
            });
        }
    }
}