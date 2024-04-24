using MangaFlex.Core.Data.FriendShip.Commands;
using MangaFlex.Core.Data.FriendShip.Repository;
using MangaFlex.Core.Data.Users.Models;
using MediatR;

namespace MangaFlex.Core.Data.FriendShip.Handlers;

public class GetSubscriptionsHandler : IRequestHandler<GetUserSubscriptionsCommand, IEnumerable<User>>
{
    private readonly IFriendShipRepository friendShipRepository;
    public GetSubscriptionsHandler(IFriendShipRepository friendShipRepository)
    {
        this.friendShipRepository = friendShipRepository;
    }

    public async Task<IEnumerable<User>> Handle(GetUserSubscriptionsCommand request, CancellationToken cancellationToken)
    {
        var result = await friendShipRepository.GetSubscriptionsAsync(request.UserId);
        return result;
    }
}
