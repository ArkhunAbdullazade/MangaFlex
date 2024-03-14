using MangaFlex.Core.Data.User;
using Microsoft.AspNetCore.Identity;

namespace MangaFlex.Infrastructure.Data.UserService;

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
        var user = await this.userManager.FindByNameAsync(userName);

        if (user is null)
        {
            throw new NullReferenceException("Incorrect Login");
        }

        var result = await this.signInManager.PasswordSignInAsync(user, password, true, true);

        if (!result.Succeeded)
        {
            throw new ArgumentException("Incorrect Password");
        }
    }

    public async Task SignOutAsync()
    {
        await this.signInManager.SignOutAsync();
    }

    public async Task SignupAsync(User user, string password)
    {
        var result = await this.userManager.CreateAsync(user, password);

        if (!result.Succeeded)
        {
            var exceptions = new List<Exception>();

            foreach (var error in result.Errors)
            {
                exceptions.Add(new ArgumentException(error.Description, error.Code));
            }

            throw new AggregateException(exceptions);
        }
    }
}
