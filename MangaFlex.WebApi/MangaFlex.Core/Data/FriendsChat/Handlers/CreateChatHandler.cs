using MangaFlex.Core.Data.FriendsChat.Commands;
using MangaFlex.Core.Data.FriendsChat.Repository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MangaFlex.Core.Data.FriendsChat.Handlers;

public class CreateChatHandler : IRequestHandler<CreateChatCommand>
{
    private IFriendsChatRepository friendsChatRepository;

    public CreateChatHandler(IFriendsChatRepository friendsChatRepository)
    {
        this.friendsChatRepository = friendsChatRepository;
    }

    public async Task Handle(CreateChatCommand request, CancellationToken cancellationToken)
    {
        await friendsChatRepository.CreateChat(request.UserId, request.FriendId);
    }
}
