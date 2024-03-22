namespace MangaFlex.Infrastructure.Data.User.Services;

using MangaFlex.Core.Data.Users.Services;
using Microsoft.AspNetCore.Identity;
using MangaFlex.Core.Data.Users.Models;
using MangaFlex.Infrastructure.Data.DBContext;
using Microsoft.EntityFrameworkCore;

public class UserService : IUserService
{
    private readonly UserManager<User> userManager;
    private readonly SignInManager<User> signInManager;
    private readonly RoleManager<IdentityRole> roleManager;
    private readonly MangaFlexDbContext dbContext;
    public UserService(UserManager<User> userManager, SignInManager<User> signInManager, RoleManager<IdentityRole> roleManager,MangaFlexDbContext dbcotext)
    {
        this.userManager = userManager;
        this.signInManager = signInManager;
        this.roleManager = roleManager;
        this.dbContext = dbcotext;
    }

    public async Task UpdateAvatar(string url, string id)
    {
        var user = await dbContext.Users.FirstOrDefaultAsync(x => x.Id == id);
        user.AvatarPath = url;
        await dbContext.SaveChangesAsync();
    }

    public async Task AddLastWatchAsync(string mangaid,string userid)
    {
        await dbContext.UserLastWatch.AddAsync(new LastWatch()
        {
            UserId = userid,
            MangaId = mangaid,
        });
        await dbContext.SaveChangesAsync();
    }

    public async Task DeleteLastWatchAsync(string userid, string mangaid)
    {
        var todelete = await dbContext.UserLastWatch.FirstOrDefaultAsync(x => x.UserId == userid && x.MangaId == mangaid);
        dbContext.Remove(todelete);
        await dbContext.SaveChangesAsync();
    }

    public async Task<User> GetByIdAsync(string id)
    {
        var user = await dbContext.Users.FirstOrDefaultAsync(x => x.Id == id);
        return user;
    }

    public async Task<IEnumerable<LastWatch>> GetLastWatchAsync(string userId)
    {
        var result = await dbContext.UserLastWatch.Where(x => x.UserId == userId).ToArrayAsync();
        return result;
    }

    public async Task<bool> IsMangaInLastWatchAsync(string userid, string mangaid)
    {
        var result = await dbContext.UserLastWatch.AnyAsync(x => x.UserId == userid && mangaid == x.MangaId);
        return result;
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
