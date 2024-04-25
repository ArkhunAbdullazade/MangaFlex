using MediatR;
using MangaFlex.Core.Data.Mangas.ViewModels;
using MangaFlex.Core.Data.Mangas.Models;
using System.Net.Http.Json;
using MangaFlex.Core.Data.Mangas.Services;

namespace MangaFlex.Core.Data.Users.Commands;

public class ReadMangaHandler : IRequestHandler<ReadMangaCommand, MangaChapterViewModel>
{
    // private readonly HttpClient httpClient;
    private readonly IMangaService mangaService;

    public ReadMangaHandler(IMangaService mangaService)
    {
        this.mangaService = mangaService;
    }
    public async Task<MangaChapterViewModel> Handle(ReadMangaCommand request, CancellationToken cancellationToken)
    {
        // var response = await httpClient.GetAsync($"http://localhost:5267/api/Manga/Read?id={request.Id}&chapter={request.Chapter}&id={request.Language}");
        // MangaChapterViewModel? manga = await response.Content.ReadFromJsonAsync<MangaChapterViewModel>();
        // return manga!;

        
        return await mangaService.ReadAsync(request.Id, request.Chapter, request.Language);
    }
} 