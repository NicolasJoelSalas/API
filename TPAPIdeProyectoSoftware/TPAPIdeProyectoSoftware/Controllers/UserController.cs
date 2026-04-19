using Application;
using Application.DTOs.User;
using Application.Interfaces.Handlers.User;
using Application.UseCases.USER.Handlers;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace API.Controllers
{
    [ApiController]
    [Route("api/USERS")]
    public class UserController : ControllerBase
    {
        private readonly ICreateUserHandler _handler;
        public UserController(ICreateUserHandler Createhandler)
        {
            _handler = Createhandler;
        }
       
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] UserRequestDto dto)
        {
            await _handler.Handle(dto);

            return Ok(new
            {
                message = "Usuario creado correctamente"
            });
        }
    }
}