using MangaFlex.Core.Data.FriendShip.Commands;
using MangaFlex.Core.Data.FriendShip.Repository;
using MangaFlex.Core.Data.Users.Models;
using MangaFlex.Core.Data.Users.Repository;
using MediatR;

namespace MangaFlex.Core.Data.FriendShip.Handlers;

public class GetFollowersHandler : IRequestHandler<GetFollowersCommand, IEnumerable<User>>
{
    private readonly IFriendShipRepository friendShipRepository;
    public GetFollowersHandler(IFriendShipRepository friendShipRepository)
    {
        this.friendShipRepository = friendShipRepository;
    }

    public async Task<IEnumerable<User>> Handle(GetFollowersCommand request, CancellationToken cancellationToken)
    {
        var result = await friendShipRepository.GetFollowersAsync(request.UserID);
        return result;
    }
}
