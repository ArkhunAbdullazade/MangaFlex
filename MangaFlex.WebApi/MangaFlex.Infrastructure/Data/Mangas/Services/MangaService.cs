using MangaDexSharp;
using MangaFlex.Core.Data.Mangas.Services;

namespace MangaFlex.Infrastructure.Data.Mangas.Services;

using MangaFlex.Core.Data.Mangas.Models;
using MangaFlex.Core.Data.Mangas.ViewModels;
using MangaFlex.Infrastructure.Data.Mangas.Settings;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System.Net.Http;
using System.Net.Http.Json;

public class MangaService : IMangaService
{
    private readonly IMongoCollection<Manga> mangasCollection;

    public MangaService(IOptions<MangaDbSettings> mangaDbSettings)
    {
        System.Console.WriteLine(mangaDbSettings.Value.ConnectionString);
        var mongoClient = new MongoClient(mangaDbSettings.Value.ConnectionString);

        var mongoDatabase = mongoClient.GetDatabase(mangaDbSettings.Value.DatabaseName);

        mangasCollection = mongoDatabase.GetCollection<Manga>(mangaDbSettings.Value.MangasCollectionName);
    }

    public async Task<MangasViewModel> FindMangasAsync(string? query = null, int page = 1)
    {
        MangasViewModel mangasVM;
        var mangasFound = mangasCollection.Find(m => query == null || m.Title.Contains(query));
        var mangas = mangasFound.Skip((page-1) * 20).ToEnumerable().Take(20);
        if (mangas.Count() > 0)
        {
            mangasVM = new MangasViewModel {
                Mangas = mangas,
                Page = page,
                Search = query,
                TotalPages = mangasFound.ToEnumerable().Count() / 20 + ((mangasFound.ToEnumerable().Count() % 20) > 0 ? 1 : 0),
            };
        }
        else 
        {
            throw new InvalidOperationException($"The Page {page} Is Impossible To Move To");
        }
        return mangasVM;
    }

    public async Task<AvailableLanguagesVM> GetAvailableLanguages(string id)
    {
        // var response = await httpClient.GetAsync($"http://localhost:5267/api/Manga/AvailableLanguages?id={id}");
        // var availableLanguages = await response.Content.ReadFromJsonAsync<AvailableLanguagesVM>();
        // return availableLanguages!;
        throw new NotImplementedException();
    }

    public async Task<Manga> GetByIdAsync(string id)
    {
        var manga = mangasCollection.Find(Builders<Manga>.Filter.Eq("Id", id)).ToEnumerable().FirstOrDefault();
        return manga!;
    }
    public async Task<MangaChapterViewModel> ReadAsync(string mangaId, int chapter = 1, string language = "en")
    {
        var manga = mangasCollection.Find(Builders<Manga>.Filter.Eq("Id", mangaId)).ToEnumerable().FirstOrDefault();
        var mangaChapter = manga.Chapters?[chapter-1];
        MangaChapterViewModel? mangaChapterViewModel = new MangaChapterViewModel {
            MangaId = mangaId,
            Pages = mangaChapter?.Pages,
            ChapterTitle = mangaChapter?.ChapterTitle,
            Chapter = chapter,
            TotalChapters = manga.Chapters.Count,
        };
        return mangaChapterViewModel!;
    }
}
