namespace MangaFlex.Core.Data.FriendsChat.Handlers;

using MangaFlex.Core.Data.FriendsChat.Commands;
using MangaFlex.Core.Data.FriendsChat.Repository;
using MangaFlex.Core.Data.FriendsChat.Models;
using MangaFlex.Core.Data.Message.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

public class GetChatsHandler : IRequestHandler<GetChatsCommand, IEnumerable<FriendsChat>>
{
    private IFriendsChatRepository friendsChatRepository;

    public GetChatsHandler(IFriendsChatRepository friendsChatRepository)
    {
        this.friendsChatRepository = friendsChatRepository;
    }

    public async Task<IEnumerable<FriendsChat>> Handle(GetChatsCommand request, CancellationToken cancellationToken)
    {
        var result = await friendsChatRepository.GetChats(request.UserId);
        return result;
    }
}
