namespace MangaFlex.Core.Data.User.Command;

using MediatR;
using MangaFlex.Core.Data.User.Models;

public class SignInCommand : IRequest
{
    public User User { get; set; }
    public string Password { get; set; }

    public SignInCommand(User user, string password)
    {
        User = user;
        Password = password;
    }

    public SignInCommand() { }
}
