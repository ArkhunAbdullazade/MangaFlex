using MangaFlex.Core.Data.LastWatches.Repository;
using MangaFlex.Core.Data.Mangas.Models;
using MangaFlex.Core.Data.Mangas.Services;
using MangaFlex.Core.Data.Users.Commands;
using MangaFlex.Core.Data.Users.Repository;
using MediatR;

namespace MangaFlex.Core.Data.LastWatches.Handlers;

internal class GetUserLastWatchesHandler : IRequestHandler<GetUserLastWatchesCommand, IEnumerable<Manga>>
{
    private readonly IUserRepository userRepository;
    private readonly ILastWatchesRepository lastWatchesRepository;
    private readonly IMangaService mangaService;

    public GetUserLastWatchesHandler(ILastWatchesRepository lastWatchesRepository, IMangaService mangaService, IUserRepository userRepository)
    {
        this.lastWatchesRepository = lastWatchesRepository;
        this.mangaService = mangaService;
        this.userRepository = userRepository;
    }
    public async Task<IEnumerable<Manga>> Handle(GetUserLastWatchesCommand request, CancellationToken cancellationToken)
    {
        var user = await userRepository.GetByIdAsync(request.UserId!);
        var lastwatched = await lastWatchesRepository.GetLastWatchAsync(request.UserId!);
        var lastWatched = new List<Manga>();

        foreach (var manga in lastwatched)
        {
            lastWatched.Add(await mangaService.GetByIdAsync(manga.MangaId!));
        }

        return lastWatched;
    }
}

