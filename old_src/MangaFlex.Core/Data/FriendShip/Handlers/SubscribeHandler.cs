using MangaFlex.Core.Data.FriendShip.Commands;
using MangaFlex.Core.Data.FriendShip.Repository;
using MediatR;

namespace MangaFlex.Core.Data.FriendShip.Handlers;

public class SubscribeHandler : IRequestHandler<SubscribeCommand>
{
    private readonly IFriendShipRepository friendShipRepository;
    public SubscribeHandler(IFriendShipRepository friendShipRepository)
    {
        this.friendShipRepository = friendShipRepository;
    }

    public async Task Handle(SubscribeCommand request, CancellationToken cancellationToken)
    {
        await friendShipRepository.AddFriendAsync(request.UserId, request.UserToId);
    }
}
