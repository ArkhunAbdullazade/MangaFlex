namespace MangaFlex.Infrastructure.Data.FriendShip.Repository;

using MangaFlex.Core.Data.FriendShip.Repository;
using MangaFlex.Core.Data.FriendShip.Models;
using MangaFlex.Core.Data.Users.Models;
using MangaFlex.Infrastructure.Data.DBContext;
using Microsoft.EntityFrameworkCore;

public class FriendShipRepository : IFriendShipRepository
{
    private readonly MangaFlexDbContext dbContext;
    public FriendShipRepository(MangaFlexDbContext dbcotext)
    {
        dbContext = dbcotext;
    }
    public async Task AddFriendAsync(string userid, string friendid)
    {
        await dbContext.FriendShips.AddAsync(new FriendShip()
        {
            UserId = userid,
            FriendId = friendid
        });
        await dbContext.SaveChangesAsync();
    }

    public async Task RemoveFriendAsync(string userid, string friendid)
    {
        var result = await dbContext.FriendShips.FirstOrDefaultAsync(x => x.UserId == userid && x.FriendId == friendid);
        Console.WriteLine(userid);
        Console.WriteLine(friendid);
        dbContext.Remove(result);
        await dbContext.SaveChangesAsync();
    }

    public async Task<IEnumerable<User>> GetSubscriptionsAsync(string userid)
    {
        var users = await dbContext.FriendShips
            .Include(fs => fs.User)
            .Where(fs => fs.UserId == userid)
            .Select(fs => fs.Friend)
            .ToArrayAsync();
        return users;
    }

    public async Task<IEnumerable<User>> GetFollowersAsync(string userid)
    {
        var users = await dbContext.FriendShips
            .Include(fs => fs.User)
            .Where(fs => fs.FriendId == userid)
            .Select(fs => fs.User)
            .ToArrayAsync();
        return users;
    }
}
