namespace MangaFlex.Core.Data.User.Handler;

using MangaFlex.Core.Data.User.Command;
using MangaFlex.Core.Data.User.Services;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

public class SignInHandler : IRequestHandler<SignInCommand>
{
    private readonly IUserService _userService;

    public SignInHandler(IUserService userService)
    {
        _userService = userService;
    }

    public async Task Handle(SignInCommand request, CancellationToken cancellationToken)
    {
        await _userService.SignupAsync(request.User, request.Password);
    }
}
