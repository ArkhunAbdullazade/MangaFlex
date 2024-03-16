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

    public async Task<IEnumerable<Manga>> SearchAsync(string? query = null)
    {
        var mangaFilter = new MangaFilter
        {
            ExcludedTags = new[] {"Harem", "Ecchi", "Gyaru", "Genderswap", "Incest", "Reverse Harem", "Erotica", "Sexual Violence", "Loli", "Suggestive" , "Pornographic"},
            
        };
        if(!string.IsNullOrEmpty(query)) mangaFilter.Title = query;
        mangaFilter.Order[MangaFilter.OrderKey.followedCount] = OrderValue.asc;

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
            allMangas.Append(this.Convert(manga, (await apiClient.Cover.Get(manga.Id)).Data.Attributes?.FileName));
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
            Title = mangaToConvert.Attributes?.Title["English"],
            Description = mangaToConvert.Attributes?.Description["English"],
            IsLocked = mangaToConvert.Attributes?.IsLocked ?? false,
            Links = mangaToConvert.Attributes?.Links["English"],
            OriginalLanguage = mangaToConvert.Attributes?.OriginalLanguage,
            LastVolume = mangaToConvert.Attributes?.LastVolume,
            LastChapter = mangaToConvert.Attributes?.LastChapter,
            Year = mangaToConvert.Attributes?.Year,
            Tags = mangaToConvert.Attributes?.Tags.Select(mg => mg.Attributes!.Name["English"]),
            State = mangaToConvert.Attributes?.State,
            CreatedAt = mangaToConvert.Attributes?.CreatedAt,
            UpdatedAt = mangaToConvert.Attributes?.UpdatedAt,
            LatestUploadedChapter = mangaToConvert.Attributes?.LatestUploadedChapter,
            Cover = $"https://uploads.mangadex.org/covers/{mangaToConvert.Id}/{coverFileName}",
        };
}