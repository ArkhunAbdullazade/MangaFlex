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

public class GetChatIdHandler : IRequestHandler<GetChatIdCommand,int>
{
    private IFriendsChatRepository friendsChatRepository;

    public GetChatIdHandler(IFriendsChatRepository friendsChatRepository)
    {
        this.friendsChatRepository = friendsChatRepository;
    }

    public async Task<int> Handle(GetChatIdCommand request, CancellationToken cancellationToken)
    {
        return await friendsChatRepository.GetChatId(request.UserId, request.FriendId);
    }
}
