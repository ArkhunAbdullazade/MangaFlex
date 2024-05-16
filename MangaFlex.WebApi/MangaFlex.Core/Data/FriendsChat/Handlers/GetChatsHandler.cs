namespace MangaFlex.Core.Data.FriendsChat.Handlers;

using MangaFlex.Core.Data.FriendsChat.Commands;
using MangaFlex.Core.Data.FriendsChat.Repository;
using MangaFlex.Core.Data.Message.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

public class GetChatHandler : IRequestHandler<GetChatCommand, IEnumerable<Message>>
{
    private IFriendsChatRepository friendsChatRepository;

    public GetChatHandler(IFriendsChatRepository friendsChatRepository)
    {
        this.friendsChatRepository = friendsChatRepository;
    }

    public async Task<IEnumerable<Message>> Handle(GetChatCommand request, CancellationToken cancellationToken)
    {
        var result = await friendsChatRepository.GetMessagesInChat(request.UserId, request.FriendId);
        return result;
    }
}
