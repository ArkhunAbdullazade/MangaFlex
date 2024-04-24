using MediatR;
using MangaFlex.Core.Data.Mangas.ViewModels;
using MangaFlex.Core.Data.Mangas.Models;
using System.Net.Http.Json;
using MangaFlex.Core.Data.Mangas.Services;

namespace MangaFlex.Core.Data.Users.Commands;

public class GetMangaByIdHandler : IRequestHandler<GetMangaByIdCommand, Manga>
{
    // private readonly HttpClient httpClient;
    private readonly IMangaService mangaService;

    public GetMangaByIdHandler(IMangaService mangaService)
    {
        this.mangaService = mangaService;
    }
    public async Task<Manga> Handle(GetMangaByIdCommand request, CancellationToken cancellationToken)
    {
        // var response = await httpClient.GetAsync($"http://localhost:5267/api/Manga/About?id={request.Id}");
        // Manga? manga = await response.Content.ReadFromJsonAsync<Manga>();
        // return manga!;

        return await mangaService.GetByIdAsync(request.Id);
    }
} 