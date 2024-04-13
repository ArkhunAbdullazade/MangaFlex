using MangaFlex.Core.Data.Mangas.Models;
using MangaFlex.Core.Data.UserMangas.Repositories;
using MangaFlex.Core.Data.UserMangas.Models;
using MangaFlex.Infrastructure.Data.DBContext;
using MangaFlex.Infrastructure.Data.Mangas.Services;
using Microsoft.EntityFrameworkCore;

namespace MangaFlex.Infrastructure.Data.UserMangas.Repositories;

public class UserMangaRepository : IUserMangaRepository
{
    private readonly MangaFlexDbContext dbContext;

    public UserMangaRepository(MangaFlexDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public async Task<UserManga?> GetByUserAndMangaIdAsync(string? userId, string? mangaId)
    {
        return await this.dbContext.UserMangas.Where(um => um.UserId == userId && um.MangaId == mangaId).FirstOrDefaultAsync();
    }

    public async Task<bool> AddUserMangaAsync(UserManga userManga)
    {
        await this.dbContext.UserMangas.AddAsync(userManga);
        await this.dbContext.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteUserMangaAsync(string? userId, string? mangaId)
    {
        var umToDelete = await this.GetByUserAndMangaIdAsync(userId, mangaId);
        this.dbContext.UserMangas.Remove(umToDelete!);
        await this.dbContext.SaveChangesAsync();
        return true;
    }

    public async Task<IEnumerable<UserManga>> GetAllMangasByUserIdAsync(string? userId)
    {
        var ums = await this.dbContext.UserMangas.Where(um => um.UserId == userId).ToListAsync();

        return (ums is null || !ums.Any()) ? Enumerable.Empty<UserManga>() : ums;
    }

    public bool IsMangaReadByUser(string? userId, string? mangaId)
    {
        return this.dbContext.UserMangas.Any(um => um.UserId == userId && um.MangaId == mangaId);
    }

    public async Task<bool> UpdateUserMangaAsync(UserManga userManga)
    {
        var found = await this.dbContext.UserMangas.FirstOrDefaultAsync(um => um.UserId == userManga.UserId && um.MangaId == userManga.MangaId);
        
        if(found is null) return false;

        found.Status = userManga.Status;
        await this.dbContext.SaveChangesAsync();
        return true;
    }
}