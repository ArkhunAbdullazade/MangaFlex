namespace MangaFlex.Infrastructure.Data.LastWatches.Repository;

using MangaFlex.Core.Data.LastWatches.Models;
using MangaFlex.Core.Data.LastWatches.Repository;
using MangaFlex.Infrastructure.Data.DBContext;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

public class LastWatchesRepository : ILastWatchesRepository
{
    private readonly MangaFlexDbContext dbContext;
    public LastWatchesRepository(MangaFlexDbContext dbcotext)
    {
        dbContext = dbcotext;
    }

    public async Task AddLastWatchAsync(string mangaid, string userid)
    {
        await dbContext.UserLastWatches.AddAsync(new LastWatch()
        {
            UserId = userid,
            MangaId = mangaid,
        });
        await dbContext.SaveChangesAsync();
    }

    public async Task DeleteLastWatchAsync(string userid, string mangaid)
    {
        var todelete = await dbContext.UserLastWatches.FirstOrDefaultAsync(x => x.UserId == userid && x.MangaId == mangaid);
        dbContext.Remove(todelete);
        await dbContext.SaveChangesAsync();
    }

    public async Task<IEnumerable<LastWatch>> GetLastWatchAsync(string userId)
    {
        var result = await dbContext.UserLastWatches.Where(x => x.UserId == userId).ToArrayAsync();
        return result;
    }

    public async Task<bool> IsMangaInLastWatchAsync(string userid, string mangaid)
    {
        var result = await dbContext.UserLastWatches.AnyAsync(x => x.UserId == userid && mangaid == x.MangaId);
        return result;
    }
}
