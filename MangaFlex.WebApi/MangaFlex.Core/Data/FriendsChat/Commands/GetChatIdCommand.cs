using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MangaFlex.Core.Data.FriendsChat.Commands;

public class GetChatIdCommand : IRequest<int>
{
    public string UserId { get; set; }
    public string FriendId { get; set; }

    public GetChatIdCommand(string userId, string friendId)
    {
        UserId = userId;
        FriendId = friendId;
    }
}
