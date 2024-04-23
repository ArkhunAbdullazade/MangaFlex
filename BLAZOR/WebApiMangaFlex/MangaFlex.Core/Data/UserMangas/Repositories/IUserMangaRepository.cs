using MangaFlex.Core.Data.Mangas.Models;
using MangaFlex.Core.Data.UserMangas.Models;

namespace MangaFlex.Core.Data.UserMangas.Repositories;
public interface IUserMangaRepository
{
    public Task<bool> AddUserMangaAsync(UserManga userManga);
    public Task<bool> DeleteUserMangaAsync(string? userId, string? mangaId);
    public Task<bool> UpdateUserMangaAsync(UserManga userManga);
    public Task<IEnumerable<UserManga>> GetAllMangasByUserIdAsync(string? userId);
    public Task<UserManga?> GetByUserAndMangaIdAsync(string? userId, string? mangaId);
    public bool IsMangaReadByUser(string? userId, string? mangaId);

}