using MangaFlex.Core.Data.Mangas.Models;

namespace MangaFlex.Core.Data.Mangas.Services;
public interface IMangaService
{
    public Task<IList<Manga>> FindMangasAsync(string? query = null, int page = 1);
    public Task<Manga> GetByIdAsync(string id);
    public Task<IList<MangaPageViewModel>> ReadAsync(string mangaId, int chapter = 1);
}