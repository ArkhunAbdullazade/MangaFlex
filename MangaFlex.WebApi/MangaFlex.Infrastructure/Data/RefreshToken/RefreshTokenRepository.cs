namespace MangaFlex.Infrastructure.Data.RefreshToken;

using MangaFlex.Core.Data.RefreshToken.Repository;
using MangaFlex.Infrastructure.Data.DBContext;
using MangaFlex.Core.Data.RefreshToken.Model;
using Microsoft.EntityFrameworkCore;

public class RefreshTokenRepository : IRefreshTokenRepository
{
    private readonly MangaFlexDbContext mangaFlexDbContext;

    public RefreshTokenRepository(MangaFlexDbContext mangaFlexDbContext)
    {
        this.mangaFlexDbContext = mangaFlexDbContext;
    }

    public async Task<Guid> AddRefreshTokenAsync(string UserId)
    {
        var refreshToken = new RefreshToken()
        {
            Token = Guid.NewGuid(),
            UserId = UserId
        };
        await this.mangaFlexDbContext.RefreshTokens.AddAsync(refreshToken);
        await this.mangaFlexDbContext.SaveChangesAsync();
        return refreshToken.Token;
    }

    public async Task<Guid> UpdateRefreshTokenAsync(Guid Token, string UserId)
    {
        var refreshTokenToChange = this.mangaFlexDbContext.RefreshTokens.FirstOrDefault(refreshToken => refreshToken.Token == Token && refreshToken.UserId == UserId);

        if (refreshTokenToChange == null)
        {
            var refreshTokensToDelete = await this.mangaFlexDbContext.RefreshTokens.Where(rf => rf.UserId == UserId)
                .ToListAsync();

            this.mangaFlexDbContext.RefreshTokens.RemoveRange(refreshTokensToDelete);
            await this.mangaFlexDbContext.SaveChangesAsync();

            throw new ArgumentNullException($"Refresh token '{Token}' doesn't exist for userid '{UserId}'");
        }

        refreshTokenToChange.Token = Guid.NewGuid();
        await this.mangaFlexDbContext.SaveChangesAsync();
        return refreshTokenToChange.Token;
    }

}