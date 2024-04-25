namespace MangaFlex.Infrastructure.Data.Users.Repository;

using MangaFlex.Core.Data.Users.Models;
using MangaFlex.Infrastructure.Data.DBContext;
using Microsoft.EntityFrameworkCore;
using MangaFlex.Core.Data.Users.Repository;
using Microsoft.AspNetCore.Identity;

public class UserRepository : IUserRepository
{
    private readonly MangaFlexDbContext dbContext;
    private readonly UserManager<User> userManager;
    public UserRepository(MangaFlexDbContext dbcotext, UserManager<User> userManager)
    {
        dbContext = dbcotext;
        this.userManager = userManager;
    }

    public async Task UpdateAvatarAsync(string url, string id)
    {
        var user = await dbContext.Users.FirstOrDefaultAsync(x => x.Id == id);
        if (user is not null) user.AvatarPath = url;
        
        await dbContext.SaveChangesAsync();
    }

    public async Task<User> GetByIdAsync(string id)
    {
        var user = await dbContext.Users.FirstOrDefaultAsync(x => x.Id == id);
        return user!;
    }

    public async Task<IEnumerable<User>> GetUsersAsync()
    {
        var users = await dbContext.Users.ToArrayAsync();
        return users;
    }

    public async Task UpdateUserData(string nickname, string oldpassword, string newpassword,string id)
    {

        var user = await dbContext.Users.FirstOrDefaultAsync(x => x.Id == id);
        if (string.IsNullOrWhiteSpace(oldpassword) == false &&  string.IsNullOrWhiteSpace(newpassword) == false)
        {
            await userManager.ChangePasswordAsync(user, oldpassword, newpassword);
        }
        if(string.IsNullOrWhiteSpace(nickname) == false && nickname != user.UserName)
        {
            var result = await userManager.SetUserNameAsync(user, nickname);
        }
        await dbContext.SaveChangesAsync();
    }
}
