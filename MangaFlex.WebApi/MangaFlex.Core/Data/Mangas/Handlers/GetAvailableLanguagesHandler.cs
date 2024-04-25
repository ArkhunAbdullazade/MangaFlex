using MediatR;
using MangaFlex.Core.Data.Mangas.ViewModels;
using MangaFlex.Core.Data.Mangas.Models;
using System.Net.Http.Json;
using MangaFlex.Core.Data.Mangas.Services;

namespace MangaFlex.Core.Data.Users.Commands;

public class GetAvailableLanguagesHandler : IRequestHandler<GetAvailableLanguagesCommand, AvailableLanguagesVM>
{
    // private readonly HttpClient httpClient;
    private readonly IMangaService mangaService;

    public GetAvailableLanguagesHandler(IMangaService mangaService)
    {
        this.mangaService = mangaService;
    }
    public async Task<AvailableLanguagesVM> Handle(GetAvailableLanguagesCommand request, CancellationToken cancellationToken)
    {
        // string[]? availableLanguages;
        // var response = await httpClient.GetAsync($"http://localhost:5267/api/Manga/AvailableLanguages?id={request.Id}");
        // availableLanguages = await response.Content.ReadFromJsonAsync<string[]>();
        // return availableLanguages;

        return await mangaService.GetAvailableLanguages(request.Id);
    }
} 