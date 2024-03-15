using MediatR;
using MangaFlex.Core.Data.Users.Models;

namespace MangaFlex.Core.Data.Users.Command;

public class SignInCommand : IRequest
{
    public User? User { get; set; }
    public string? Password { get; set; }

    public SignInCommand(User user, string password)
    {
        User = user;
        Password = password;
    }

    public SignInCommand() { }
}
