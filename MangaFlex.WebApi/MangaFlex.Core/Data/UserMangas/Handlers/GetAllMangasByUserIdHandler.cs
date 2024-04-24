using MediatR;
using MangaFlex.Core.Data.UserMangas.Repositories;
using MangaFlex.Core.Data.UserMangas.Commands;
using MangaFlex.Core.Data.Mangas.Models;
using MangaFlex.Core.Data.Mangas.Services;

namespace MangaFlex.Core.Data.UserMangas.Handlers;

public class GetAllMangasByUserIdHandler : IRequestHandler<GetAllMangasByUserIdCommand, IEnumerable<Manga>>
{
    private readonly IUserMangaRepository userMangaRepository;
    private readonly IMangaService mangaService;

    public GetAllMangasByUserIdHandler(IUserMangaRepository userMangaRepository, IMangaService mangaService)
    {
        this.userMangaRepository = userMangaRepository;
        this.mangaService = mangaService;
    }
    public async Task<IEnumerable<Manga>> Handle(GetAllMangasByUserIdCommand request, CancellationToken cancellationToken)
    {
        var ums = await this.userMangaRepository.GetAllMangasByUserIdAsync(request.UserId!);
        var movies = await Task.WhenAll(ums.Select(async um => await mangaService.GetByIdAsync(um.MangaId!)));
        return movies!;
    }
}