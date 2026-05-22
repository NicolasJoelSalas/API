using Application;
using Application.DTOs.User;
using Application.Interfaces.Handlers.User;
using Application.UseCases.EVENT.Commands;
using Application.UseCases.USER.Commands;
using Application.UseCases.USER.Queries;
using Microsoft.AspNetCore.Mvc;

namespace TPAPIdeProyectoSoftware.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UserController : ControllerBase
    {
        private readonly ICreateUserHandler _createUserHandler;
        private readonly IDeleteUserHandler _deleteUserHandler;
        private readonly IUpdateUserHandler _updateUserHandler;
        private readonly IGetAllUserHandler _getAllUserHandler;
        private readonly IGetByIdUserHandler _getByIdUserHandler;
        private readonly ILoginUserHandler _loginUserHandler;

        public UserController(
            ICreateUserHandler createUserHandler,
            IDeleteUserHandler deleteUserHandler,
            IUpdateUserHandler updateUserHandler,
            IGetAllUserHandler getAllUserHandler,
            IGetByIdUserHandler getByIdUserHandler,
            ILoginUserHandler loginUserHandler)
        {
            _createUserHandler = createUserHandler;
            _deleteUserHandler = deleteUserHandler;
            _updateUserHandler = updateUserHandler;
            _getAllUserHandler = getAllUserHandler;
            _getByIdUserHandler = getByIdUserHandler;
            _loginUserHandler = loginUserHandler;
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] UserRequestDto request)
        {
            var command = new CreateUserCommand(
                request.Name,
                request.Email,
                request.PasswordHash
            );

            var message = await _createUserHandler.Handle(command);

            if (message != "OK")
                return BadRequest(new { message });

            return StatusCode(201, new
            {
                message = "Usuario creado correctamente"
            });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(int id)
        {
            var query = new GetByIdUserQuery(id);

            var (user, message) = await _getByIdUserHandler.Handle(query);

            if (message != "OK")
                return NotFound(new { message });

            return Ok(user);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            var query = new GetAllUserQuery();

            var (users, message) = await _getAllUserHandler.Handle(query);

            if (message != "OK")
                return NotFound(new { message });

            return Ok(users);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var command = new DeleteUserCommand(id);

            var message = await _deleteUserHandler.Handle(command);

            if (message == "Usuario no encontrado")
                return NotFound(new { message });

            return Ok(new { message });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id, [FromBody] UserRequestDto request)
        {
            var command = new UpdateUserCommand(
                id,
                request.Name,
                request.Email,
                request.PasswordHash
            );

            var message = await _updateUserHandler.Handle(command);

            if (message == "Usuario no encontrado")
                return NotFound(new { message });

            if (message != "Usuario actualizado correctamente")
                return BadRequest(new { message });

            return Ok(new { message });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var user = new GetAllUserLoginQuery (request.Name,request.PasswordHash);
            var result = await _loginUserHandler.Handle(user);

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