using MangaFlex.Core.Data.LastWatches.Command;
using MangaFlex.Core.Data.LastWatches.Repository;
using MangaFlex.Core.Data.Users.Repository;
using MediatR;

namespace MangaFlex.Core.Data.LastWatches.Handlers;

public class IsMangaInLastWatchesHandler : IRequestHandler<IsMangaInLastWatchesCommand, bool>
{
    private readonly ILastWatchesRepository lastWatchesRepository;
    public IsMangaInLastWatchesHandler(ILastWatchesRepository lastWatchesRepository)
    {
        this.lastWatchesRepository = lastWatchesRepository;
    }

    public async Task<bool> Handle(IsMangaInLastWatchesCommand request, CancellationToken cancellationToken)
    {
        var result = await lastWatchesRepository.IsMangaInLastWatchAsync(request.UserId!, request.MangaId!);
        return result;
    }
}
