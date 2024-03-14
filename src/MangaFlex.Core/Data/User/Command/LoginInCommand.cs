namespace MangaFlex.Core.Data.User.Command;

using MediatR;

public class LoginInCommand : IRequest
{
    public string Login { get; set; }
    public string Password { get; set; }

    public LoginInCommand(string login, string password)
    {
        Login = login;
        Password = password;
    }

    public LoginInCommand() { }
}
