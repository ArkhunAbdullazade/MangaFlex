using MediatR;
using MangaFlex.Core.Data.Users.Models;
using MangaFlex.Core.Data.Mangas.Services;
using MangaFlex.Core.Data.Mangas.Models;

namespace MangaFlex.Core.Data.Users.Commands;

public class FindMangasHandler : IRequestHandler<FindMangasCommand, IEnumerable<Manga>>
{
    private readonly IMangaService mangaService;

    public FindMangasHandler(IMangaService mangaService)
    {
        this.mangaService = mangaService;
    }
    public async Task<IEnumerable<Manga>> Handle(FindMangasCommand request, CancellationToken cancellationToken)
    {
        return await this.mangaService.FindMangasAsync(request.Query, request.Page);
    }
}