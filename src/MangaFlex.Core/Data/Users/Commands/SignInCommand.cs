using MediatR;

namespace MangaFlex.Core.Data.Users.Commands;

public class SignInCommand : IRequest
{
    public string? Login { get; set; }
    public string? Password { get; set; }

    public SignInCommand(string? login, string? password)
    {
        Login = login;
        Password = password;
    }

    public SignInCommand() { }
}
