using MediatR;
using MangaFlex.Core.Data.Mangas.Services;
using MangaFlex.Core.Data.Mangas.ViewModels;

namespace MangaFlex.Core.Data.Users.Commands;

public class FindMangasHandler : IRequestHandler<FindMangasCommand, MangasViewModel>
{
    private readonly IMangaService mangaService;

    public FindMangasHandler(IMangaService mangaService)
    {
        this.mangaService = mangaService;
    }
    public async Task<MangasViewModel> Handle(FindMangasCommand request, CancellationToken cancellationToken)
    {
        return await this.mangaService.FindMangasAsync(request.Query, request.Page);
    }
}