using MangaFlex.Core.Data.Mangas.Models;
using MangaFlex.Core.Data.Mangas.ViewModels;

namespace MangaFlex.Core.Data.Mangas.Services;
public interface IMangaService
{
    public Task<MangasViewModel> FindMangasAsync(string? query = null, int page = 1);
    public Task<Manga> GetByIdAsync(string id);
    public Task<MangaChapterViewModel> ReadAsync(string mangaId, int chapter = 1, string language = "en");
    public Task<AvailableLanguagesVM> GetAvailableLanguages(string id);
}