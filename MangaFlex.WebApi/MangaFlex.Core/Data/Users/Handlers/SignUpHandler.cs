namespace MangaFlex.Core.Data.Users.Handlers;

using MangaFlex.Core.Data.Users.Commands;
using MangaFlex.Core.Data.Users.Models;
using MangaFlex.Core.Data.Users.Repository;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System.Threading;
using System.Threading.Tasks;

public class SignUpHandler : IRequestHandler<SignUpCommand>
{
    private readonly UserManager<User> userManager;

    public SignUpHandler(UserManager<User> userManager) 
    {
        this.userManager = userManager;    
    }

    public async Task Handle(SignUpCommand request, CancellationToken cancellationToken)
    {
        var result = await userManager.CreateAsync(request.User!, request.Password!);

        if (!result.Succeeded)
        {
            var exceptions = new List<Exception>();

            foreach (var error in result.Errors)
            {
                exceptions.Add(new ArgumentException(error.Description, error.Code));
            }

            throw new AggregateException(exceptions);
        }

        var role = new IdentityRole { Name = "User" };
        await userManager.AddToRoleAsync(request.User!, role.Name);
    }
}
