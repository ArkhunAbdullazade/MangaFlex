using MangaFlex.Core.Data.Mangas.Models;

namespace MangaFlex.Core.Data.Mangas.Services;
public interface IMangaService
{
    public Task<IEnumerable<Manga>> FindMangasAsync(string? query = null);
    public Task<Manga> GetByIdAsync(string id);
}