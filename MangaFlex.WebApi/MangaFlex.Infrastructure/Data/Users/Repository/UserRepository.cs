namespace MangaFlex.Infrastructure.Data.Users.Repository;

using MangaFlex.Core.Data.Users.Models;
using MangaFlex.Infrastructure.Data.DBContext;
using Microsoft.EntityFrameworkCore;
using MangaFlex.Core.Data.Users.Repository;

public class UserRepository : IUserRepository
{
    private readonly MangaFlexDbContext dbContext;
    public UserRepository(MangaFlexDbContext dbcotext)
    {
        dbContext = dbcotext;
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
}
