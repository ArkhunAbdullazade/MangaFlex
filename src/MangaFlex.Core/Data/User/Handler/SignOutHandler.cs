namespace MangaFlex.Core.Data.User.Handler;

using MediatR;
using MangaFlex.Core.Data.User.Command;
using System.Threading.Tasks;
using System.Threading;
using MangaFlex.Core.Data.User.Services;

internal class SignOutHandler : IRequestHandler<SignOutCommand>
{
    private readonly IUserService _userService;

    public SignOutHandler(IUserService userService)
    {
        _userService = userService;
    }
    public async Task Handle(SignOutCommand request, CancellationToken cancellationToken)
    {
        await _userService.SignOutAsync();
    }
}
