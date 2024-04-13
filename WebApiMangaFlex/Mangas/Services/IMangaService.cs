using  WebApiMangaFlex.Mangas.Data.Mangas.Models;
using  WebApiMangaFlex.Mangas.Data.Mangas.ViewModels;

namespace  WebApiMangaFlex.Mangas.Data.Mangas.Services;
public interface IMangaService
{
    public Task<MangasViewModel> FindMangasAsync(string? query = null, int page = 1);
    public Task<Manga> GetByIdAsync(string id);
    public Task<MangaChapterViewModel> ReadAsync(string mangaId, int chapter = 1, string language = "en");
    public Task<string[]> GetAvailableLanguages(string id);
}