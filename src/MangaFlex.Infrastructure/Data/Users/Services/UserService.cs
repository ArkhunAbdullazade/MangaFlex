namespace MangaFlex.Infrastructure.Data.User.Services;

using MangaFlex.Core.Data.Users.Services;
using Microsoft.AspNetCore.Identity;
using MangaFlex.Core.Data.Users.Models;

public class UserService : IUserService
{
    private readonly UserManager<User> userManager;
    private readonly SignInManager<User> signInManager;
    private readonly RoleManager<IdentityRole> roleManager;
    public UserService(UserManager<User> userManager, SignInManager<User> signInManager, RoleManager<IdentityRole> roleManager)
    {
        this.userManager = userManager;
        this.signInManager = signInManager;
        this.roleManager = roleManager;
    }
    public async Task LoginAsync(string userName, string password)
    {
        var user = await userManager.FindByNameAsync(userName);

        if (user is null)
        {
            throw new NullReferenceException("Incorrect Login");
        }

        var result = await signInManager.PasswordSignInAsync(user, password, true, true);

        if (!result.Succeeded)
        {
            throw new ArgumentException("Incorrect Password");
        }
    }

    public async Task SignOutAsync()
    {
        await signInManager.SignOutAsync();
    }

    public async Task SignupAsync(User user, string password)
    {
        var result = await userManager.CreateAsync(user, password);

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
        await roleManager.CreateAsync(role);
        await userManager.AddToRoleAsync(user, role.Name);
    }
}
