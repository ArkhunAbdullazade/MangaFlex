namespace MangaFlex.Core.Data.Users.Commands;

using MediatR;
using MangaFlex.Core.Data.Users.Models;

public class SignUpCommand : IRequest
{
    public User? User { get; set; }
    public string? Password { get; set; }

    public SignUpCommand(User? user, string? password)
    {
        User = user;
        Password = password;
    }

    public SignUpCommand() { }
}
