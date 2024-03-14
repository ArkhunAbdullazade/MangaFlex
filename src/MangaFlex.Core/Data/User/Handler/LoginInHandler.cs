namespace MangaFlex.Core.Data.User.Handler;

using MangaFlex.Core.Data.User.Command;
using MangaFlex.Core.Data.User.Services;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

public class LoginInHandler : IRequestHandler<LoginInCommand>
{
    private readonly IUserService _userService;

    public LoginInHandler(IUserService userService)
    {
        _userService = userService;
    }
    public async Task Handle(LoginInCommand request, CancellationToken cancellationToken)
    {
        await _userService.LoginAsync(request.Login, request.Password);
    }
}
