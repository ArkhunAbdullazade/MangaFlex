using MangaDexSharp;
using MangaFlex.Core.Data.Mangas.Services;

namespace MangaFlex.Infrastructure.Data.Mangas.Services;

using MangaFlex.Core.Data.Mangas.Models;

public class MangaService : IMangaService
{
    private IMangaDex apiClient;

    public MangaService()
    {
        apiClient = MangaDex.Create();
    }

    public async Task<IList<Manga>> FindMangasAsync(string? query = null, int page = 1)
    {
        var mangaFilter = new MangaFilter
        {
            Title = query ?? string.Empty,
            Limit = 20,
            Offset = 20 * (page - 1),
            ContentRating = new[] { ContentRating.safe },
            AvailableTranslatedLanguage = new[] { "en" },
            HasAvailableChapters = true,
        };

        mangaFilter.Order[MangaFilter.OrderKey.rating] = OrderValue.desc;

        MangaList mangaList = await this.apiClient.Manga.List(mangaFilter);
        if (mangaList.ErrorOccurred)
        {
            var exceptions = new List<Exception>();

            foreach (var error in mangaList.Errors)
            {
                exceptions.Add(new ArgumentException(error.Detail, error.Title));
            }

            throw new AggregateException(exceptions);
        }

        var allMangas = new List<Manga>();
        foreach (var manga in mangaList.Data)
        {
            allMangas.Add(this.Convert(manga, manga.CoverArt().FirstOrDefault()?.Attributes?.FileName));
        }

        return allMangas;
    }

    public async Task<Manga> GetByIdAsync(string id)
    {
        var result = await this.apiClient.Manga.Get(id);

        if (result.ErrorOccurred)
        {
            var exceptions = new List<Exception>();

            foreach (var error in result.Errors)
            {
                exceptions.Add(new ArgumentException(error.Detail, error.Title));
            }

            throw new AggregateException(exceptions);
        }
        var coverFileName = result.Data.CoverArt().FirstOrDefault()?.Attributes?.FileName;
        return this.Convert(result.Data, coverFileName);
    }
    public async Task<IList<MangaPageViewModel>> ReadAsync(string mangaId, int chapter = 1)
    {
        var mangaPages = new List<MangaPageViewModel>();

        var mangaFeedFilter = new MangaFeedFilter
        {
            Order = new()
            {
                [MangaFeedFilter.OrderKey.volume] = OrderValue.asc,
                [MangaFeedFilter.OrderKey.chapter] = OrderValue.asc,
            },
        };
        mangaFeedFilter.TranslatedLanguage = new[] { "en" };
        // Fetch manga chapters
        var chapters = await apiClient.Manga.Feed(mangaId, mangaFeedFilter);

        // Fetch pages for the specified chapter
        var pages = await apiClient.Pages.Pages(chapterId: chapters.Data?[chapter-1]?.Id!);

        var imageUrlBase = $"{pages.BaseUrl}/data/{pages.Chapter.Hash}/";
        foreach (var chapterFileName in pages.Chapter.Data)
        {
            var imageUrl = $"{imageUrlBase}{chapterFileName}";
            mangaPages.Add(new MangaPageViewModel { ImageUrl = imageUrl });
        }

        return mangaPages;
    }

    private Manga Convert(MangaDexSharp.Manga mangaToConvert, string? coverFileName) => new Manga
    {
        Id = mangaToConvert.Id,
        Title = mangaToConvert.Attributes?.Title.FirstOrDefault().Value,
        Description = mangaToConvert.Attributes?.Description.FirstOrDefault().Value,
        IsLocked = mangaToConvert.Attributes?.IsLocked ?? false,
        Links = mangaToConvert.Attributes?.Links.FirstOrDefault().Value,
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

