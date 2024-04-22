using MangaFlex.Core.Data.JWT.Options;
using MangaFlex.Core.Data.Users.ViewModels;
using MediatR;

namespace MangaFlex.Core.Data.Users.Commands;

public class SignInCommand : IRequest<TokenVM>
{
    public string? Login { get; set; }
    public string? Password { get; set; }
    public JwtOptions Jwt { get; set; }

    public SignInCommand(string? login, string? password, JwtOptions jwt)
    {
        Login = login;
        Password = password;
        Jwt = jwt;
    }

    public SignInCommand() { }
}
