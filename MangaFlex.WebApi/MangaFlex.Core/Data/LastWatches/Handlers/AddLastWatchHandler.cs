using MangaFlex.Core.Data.LastWatches.Command;
using MangaFlex.Core.Data.LastWatches.Repository;
using MangaFlex.Core.Data.Users.Repository;
using MediatR;

namespace MangaFlex.Core.Data.LastWatches.Handlers;

public class AddLastWatchHandler : IRequestHandler<AddLastWatchCommand>
{
    private readonly ILastWatchesRepository lastWatchesRepository;
    public AddLastWatchHandler(ILastWatchesRepository lastWatchesRepository)
    {
        this.lastWatchesRepository = lastWatchesRepository;
    }

    public async Task Handle(AddLastWatchCommand request, CancellationToken cancellationToken)
    {
        await lastWatchesRepository.AddLastWatchAsync(request.MangaId!, request.UserId!);
    }
}
