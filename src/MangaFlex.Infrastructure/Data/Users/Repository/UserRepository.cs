namespace MangaFlex.Infrastructure.Data.Users.Repository;

using Microsoft.AspNetCore.Identity;
using MangaFlex.Core.Data.Users.Models;
using MangaFlex.Infrastructure.Data.DBContext;
using Microsoft.EntityFrameworkCore;
using MangaFlex.Core.Data.Users.Repository;
using MangaFlex.Core.Data.LastWatches.Models;
using MangaFlex.Core.Data.FriendShip.Models;

public class UserRepository : IUserRepository
{
    private readonly UserManager<User> userManager;
    private readonly SignInManager<User> signInManager;
    private readonly RoleManager<IdentityRole> roleManager;
    private readonly MangaFlexDbContext dbContext;
    public UserRepository(UserManager<User> userManager, SignInManager<User> signInManager, RoleManager<IdentityRole> roleManager, MangaFlexDbContext dbcotext)
    {
        this.userManager = userManager;
        this.signInManager = signInManager;
        this.roleManager = roleManager;
        dbContext = dbcotext;
    }

    public async Task UpdateAvatarAsync(string url, string id)
    {
        var user = await dbContext.Users.FirstOrDefaultAsync(x => x.Id == id);
        user.AvatarPath = url;
        await dbContext.SaveChangesAsync();
    }

    public async Task<User> GetByIdAsync(string id)
    {
        var user = await dbContext.Users.FirstOrDefaultAsync(x => x.Id == id);
        return user;
    }

    public async Task<IEnumerable<User>> GetUsersAsync()
    {
        var users = await dbContext.Users.ToArrayAsync();
        return users;
    }
}
