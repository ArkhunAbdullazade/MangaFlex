namespace MangaFlex.Core.Data.FriendsChat.Commands;

using MangaFlex.Core.Data.Message.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class GetChatCommand : IRequest<IEnumerable<Message>>
{
    public string UserId { get; set; }
    public string FriendId { get; set; }

    public GetChatCommand(string userId, string friendId)
    {
        UserId = userId;
        FriendId = friendId;
    }   
}
