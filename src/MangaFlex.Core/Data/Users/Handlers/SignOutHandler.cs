namespace MangaFlex.Core.Data.Users.Handlers;

using MangaFlex.Core.Data.Users.Commands;
using MangaFlex.Core.Data.Users.Models;
using MangaFlex.Core.Data.Users.Repository;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System.Threading;
using System.Threading.Tasks;

internal class SignOutHandler : IRequestHandler<SignOutCommand>
{
    private readonly SignInManager<User> signInManager;

    public SignOutHandler(SignInManager<User> signInManager)
    {
        this.signInManager = signInManager;
    }
    public async Task Handle(SignOutCommand request, CancellationToken cancellationToken)
    {
        await signInManager.SignOutAsync();
    }
}
