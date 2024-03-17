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

    public async Task<IEnumerable<Manga>> FindMangasAsync(string? query = null)
    {
        var mangaFilter = new MangaFilter {
            Title = query ?? string.Empty,
            Limit = 20,
            Offset = 0,
            // ExcludedTagsMode = Mode.or,
        };
        string[] tags = new[] {"Harem", "Ecchi", "Gyaru", "Genderswap", "Incest", "Reverse Harem", "Erotica", "Sexual Violence", "Loli", "Suggestive" , "Pornographic"};
        foreach (var tag in tags) { mangaFilter.ExcludedTags.Append(tag); }
        
        mangaFilter.Order[MangaFilter.OrderKey.rating] = OrderValue.desc;
        MangaList mangaList = await this.apiClient.Manga.List(mangaFilter);
        if(mangaList.ErrorOccurred) {
            var exceptions = new List<Exception>();

            foreach (var error in mangaList.Errors)
            {
                exceptions.Add(new ArgumentException(error.Detail, error.Title));
            }

            throw new AggregateException(exceptions);
        }

        var allMangas = Enumerable.Empty<Manga>();
        foreach (var manga in mangaList.Data)
        {
            allMangas = allMangas.Append(this.Convert(manga, manga.CoverArt().FirstOrDefault()?.Attributes?.FileName));
        }
        
        return allMangas;
    }

    public async Task<Manga> GetByIdAsync(string id)
    {
        var result = await this.apiClient.Manga.Get(id);

        if(result.ErrorOccurred) {
            var exceptions = new List<Exception>();

            foreach (var error in result.Errors)
            {
                exceptions.Add(new ArgumentException(error.Detail, error.Title));
            }

            throw new AggregateException(exceptions);
        }
        var coverFileName = (await apiClient.Cover.Get(id)).Data.Attributes?.FileName;

        return this.Convert(result.Data, coverFileName);
    }

    private Manga Convert(MangaDexSharp.Manga mangaToConvert, string? coverFileName) => new Manga {
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