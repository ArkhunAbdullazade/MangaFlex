using MediatR;

namespace MangaFlex.Core.Data.Users.Commands;

public class LoginInCommand : IRequest
{
    public string? Login { get; set; }
    public string? Password { get; set; }

    public LoginInCommand(string? login, string? password)
    {
        Login = login;
        Password = password;
    }

    public LoginInCommand() { }
}
