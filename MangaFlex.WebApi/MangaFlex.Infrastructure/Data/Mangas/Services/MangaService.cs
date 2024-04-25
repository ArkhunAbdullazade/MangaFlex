using MangaDexSharp;
using MangaFlex.Core.Data.Mangas.Services;

namespace MangaFlex.Infrastructure.Data.Mangas.Services;

using MangaFlex.Core.Data.Mangas.Models;
using MangaFlex.Core.Data.Mangas.ViewModels;
using System.Net.Http;
using System.Net.Http.Json;

public class MangaService : IMangaService
{
    private readonly HttpClient httpClient;

    public MangaService(HttpClient httpClient)
    {
        this.httpClient = httpClient;
    }

    public async Task<MangasViewModel> FindMangasAsync(string? query = null, int page = 1)
    {
        MangasViewModel mangasVM;
        var response = await httpClient.GetAsync($"http://localhost:5267/api/Mangas?page={page}&search={query}");
        if (response.IsSuccessStatusCode)
        {
            mangasVM = await response.Content.ReadFromJsonAsync<MangasViewModel>() ?? new MangasViewModel { Mangas = new List<Manga>() };
        }
        else 
        {
            throw new InvalidOperationException($"The Page {page} Is Impossible To Move To");
        }
        return mangasVM;
    }

    public async Task<AvailableLanguagesVM> GetAvailableLanguages(string id)
    {
        var response = await httpClient.GetAsync($"http://localhost:5267/api/Manga/AvailableLanguages?id={id}");
        var availableLanguages = await response.Content.ReadFromJsonAsync<AvailableLanguagesVM>();
        return availableLanguages!;
    }

    public async Task<Manga> GetByIdAsync(string id)
    {
        var response = await httpClient.GetAsync($"http://localhost:5267/api/Manga/About?id={id}");
        Manga? manga = await response.Content.ReadFromJsonAsync<Manga>();
        return manga!;
    }
    public async Task<MangaChapterViewModel> ReadAsync(string mangaId, int chapter = 1, string language = "en")
    {
        var response = await httpClient.GetAsync($"http://localhost:5267/api/Manga/Read?id={mangaId}&chapter={chapter}&language={language}");
        MangaChapterViewModel? manga = await response.Content.ReadFromJsonAsync<MangaChapterViewModel>();
        return manga!;
    }

    private Manga Convert(MangaDexSharp.Manga mangaToConvert, string? coverFileName) => new Manga
    {
        Id = mangaToConvert.Id,
        Title = mangaToConvert.Attributes?.Title.FirstOrDefault().Value,
        Description = mangaToConvert.Attributes?.Description.FirstOrDefault().Value,
        AvailableLanguages = mangaToConvert.Attributes?.AvailableTranslatedLanguages,
        IsLocked = mangaToConvert.Attributes?.IsLocked ?? false,
        OriginalLanguage = mangaToConvert.Attributes?.OriginalLanguage,
        LastVolume = mangaToConvert.Attributes?.LastVolume,
        LastChapter = mangaToConvert.Attributes?.LastChapter,
        Year = mangaToConvert.Attributes?.Year,
        Tags = mangaToConvert.Attributes?.Tags.Select(mg => mg.Attributes!.Name.FirstOrDefault().Value),
        State = mangaToConvert.Attributes?.State,
        CreatedAt = mangaToConvert.Attributes?.CreatedAt,
        UpdatedAt = mangaToConvert.Attributes?.UpdatedAt,
        LatestUploadedChapter = mangaToConvert.Attributes?.LatestUploadedChapter,
        Cover = $"https://uploads.mangadex.org/covers/{mangaToConvert.Id}/{coverFileName}",
    };
}
