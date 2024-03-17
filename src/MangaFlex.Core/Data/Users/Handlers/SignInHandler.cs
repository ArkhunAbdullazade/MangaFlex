namespace MangaFlex.Core.Data.Users.Handlers;

using MangaFlex.Core.Data.Users.Commands;
using MangaFlex.Core.Data.Users.Services;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

public class SignInHandler : IRequestHandler<SignInCommand>
{
    private readonly IUserService userService;

    public SignInHandler(IUserService userService)
    {
        this.userService = userService;
    }

    public async Task Handle(SignInCommand request, CancellationToken cancellationToken)
    {
        await this.userService.SignupAsync(request.User!, request.Password!);
    }
}
