namespace MangaFlex.Core.Data.Users.Handlers;

using MangaFlex.Core.Data.Users.Commands;
using MangaFlex.Core.Data.Users.Services;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

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
