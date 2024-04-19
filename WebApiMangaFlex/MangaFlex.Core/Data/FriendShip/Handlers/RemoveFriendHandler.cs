using MangaFlex.Core.Data.FriendShip.Commands;
using MangaFlex.Core.Data.FriendShip.Repository;
using MediatR;

namespace MangaFlex.Core.Data.FriendShip.Handlers;


public class RemoveFriendHandler : IRequestHandler<RemoveFriendCommand>
{
    private readonly IFriendShipRepository friendShipRepository;
    public RemoveFriendHandler(IFriendShipRepository friendShipRepository)
    {
        this.friendShipRepository = friendShipRepository;
    }
    public async Task Handle(RemoveFriendCommand request, CancellationToken cancellationToken)
    {
        await friendShipRepository.RemoveFriendAsync(request?.UserId!, request?.FriendId!);
    }
}
