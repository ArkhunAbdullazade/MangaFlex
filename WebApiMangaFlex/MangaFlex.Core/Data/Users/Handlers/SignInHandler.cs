namespace MangaFlex.Core.Data.Users.Handlers;

using MangaFlex.Core.Data.Users.Commands;
using MangaFlex.Core.Data.Users.Models;
using MangaFlex.Core.Data.Users.Repository;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System.Threading;
using System.Threading.Tasks;

public class SignInHandler : IRequestHandler<SignInCommand>
{
    private readonly SignInManager<User> signInManager;
    private readonly UserManager<User> userManager;
    public SignInHandler(UserManager<User> userManager, SignInManager<User> signInManager)
    {
        this.userManager = userManager;
        this.signInManager = signInManager;
    }
    public async Task Handle(SignInCommand request, CancellationToken cancellationToken)
    {
        var user = await userManager.FindByNameAsync(request.Login!);

        if (user is null)
        {
            throw new NullReferenceException("Incorrect Login");
        }

        var result = await signInManager.PasswordSignInAsync(user, request.Password!, true, true);

        if (!result.Succeeded)
        {
            throw new ArgumentException("Incorrect Password");
        }
    }
}
