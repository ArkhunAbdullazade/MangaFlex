using MangaFlex.Core.Data.Mangas.Models;

namespace MangaFlex.Core.Data.Mangas.Services;
public interface IMangaService
{
    public Task<IEnumerable<Manga>> FindMangasAsync(string? query = null);
    public Task<Manga> GetByIdAsync(string id);
    public Task<List<MangaPageViewModel>> ReadAsync(string mangaId, string chapterNumber = "1");
}

public class MangaPageViewModel
{
    public string ImageUrl { get; set; }
}