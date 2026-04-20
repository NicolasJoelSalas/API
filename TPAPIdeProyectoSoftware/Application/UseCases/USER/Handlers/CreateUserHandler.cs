using Application.DTOs.User;
using Application.Interfaces.Command.User;
using Application.Interfaces.Handlers.User;
using Application.Interfaces.Queries.User;
using Domain.Entities;

public class CreateUserHandler : ICreateUserHandler
{
    private readonly ICreateUserCommand _command;
    private readonly IEmailExistsUserquery _emailExistsquery;

    public CreateUserHandler(
        ICreateUserCommand command,
        IEmailExistsUserquery emailExistsquery)
    {
        _command = command;
        _emailExistsquery = emailExistsquery;
    }

    public async Task<string> Handle(UserRequestDto dto)
    {
        if (dto == null)
            return "Datos inválidos";

        if (string.IsNullOrWhiteSpace(dto.Name))
            return "El nombre es obligatorio";

        if (string.IsNullOrWhiteSpace(dto.Email))
            return "El email es obligatorio";

        if (string.IsNullOrWhiteSpace(dto.PasswordHash))
            return "La contraseña es obligatoria";

        var exists = await _emailExistsquery.Handle(dto.Email);

        if (exists)
            return "El email ya está registrado, use otro";

        var user = new USER
        {
            Name = dto.Name,
            Email = dto.Email,
            PasswordHash = dto.PasswordHash
        };

        await _command.ExecuteCreateUser(user);

        return "OK";
    }
}