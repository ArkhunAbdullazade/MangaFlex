namespace MangaFlex.Core.Data.Users.Handlers;

using MangaFlex.Core.Data.Users.Commands;
using MangaFlex.Core.Data.Users.Services;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

public class LoginInHandler : IRequestHandler<LoginInCommand>
{
    private readonly IUserService userService;

    public LoginInHandler(IUserService userService)
    {
        this.userService = userService;
    }
    public async Task Handle(LoginInCommand request, CancellationToken cancellationToken)
    {
        await this.userService.LoginAsync(request.Login!, request.Password!);
    }
}
