using MangaFlex.Core.Data.LastWatches.Command;
using MangaFlex.Core.Data.LastWatches.Repository;
using MangaFlex.Core.Data.Users.Repository;
using MediatR;

namespace MangaFlex.Core.Data.LastWatches.Handlers;


public class DeleteLastWatchHandler : IRequestHandler<DeleteLastWatchCommand>
{
    private readonly ILastWatchesRepository lastWatchesRepository;
    public DeleteLastWatchHandler(ILastWatchesRepository lastWatchesRepository)
    {
        this.lastWatchesRepository = lastWatchesRepository;
    }

    public async Task Handle(DeleteLastWatchCommand request, CancellationToken cancellationToken)
    {
        await lastWatchesRepository.DeleteLastWatchAsync(request.UserId!, request.MangaId!);
    }
}
