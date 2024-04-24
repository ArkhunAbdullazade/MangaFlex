using MediatR;
using MangaFlex.Core.Data.Mangas.ViewModels;
using MangaFlex.Core.Data.Mangas.Models;
using System.Net.Http.Json;
using MangaFlex.Core.Data.Mangas.Services;

namespace MangaFlex.Core.Data.Users.Commands;

public class FindMangasHandler : IRequestHandler<FindMangasCommand, MangasViewModel>
{
    // private readonly HttpClient httpClient;
    private readonly IMangaService mangaService;

    public FindMangasHandler(IMangaService mangaService)
    {
        this.mangaService = mangaService;
    }
    public async Task<MangasViewModel> Handle(FindMangasCommand request, CancellationToken cancellationToken)
    {
        // MangasViewModel mangasVM;
        // var response = await httpClient.GetAsync($"http://localhost:5267/api/Mangas?page={request.Page}&search={request.Query}");
        // if (response.IsSuccessStatusCode)
        // {
        //     mangasVM = await response.Content.ReadFromJsonAsync<MangasViewModel>() ?? new MangasViewModel { Mangas = new List<Manga>() };
        // }
        // else 
        // {
        //     throw new InvalidOperationException($"The Page {request.Page} Is Impossible To Move To");
        // }
        // return mangasVM;

        return await mangaService.FindMangasAsync(request.Query, request.Page);
    }
}