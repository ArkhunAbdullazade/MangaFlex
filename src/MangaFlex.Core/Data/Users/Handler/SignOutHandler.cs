namespace MangaFlex.Core.Data.Users.Handler;

using MediatR;
using MangaFlex.Core.Data.Users.Command;
using System.Threading.Tasks;
using System.Threading;
using MangaFlex.Core.Data.Users.Services;

internal class SignOutHandler : IRequestHandler<SignOutCommand>
{
    private readonly IUserService userService;

    public SignOutHandler(IUserService userService)
    {
        this.userService = userService;
    }
    public async Task Handle(SignOutCommand request, CancellationToken cancellationToken)
    {
        await this.userService.SignOutAsync();
    }
}
