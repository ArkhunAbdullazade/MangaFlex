using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MangaFlex.Core.Data.FriendsChat.Commands;

public class CreateChatCommand : IRequest
{
    public string UserId {  get; set; }
    public string FriendId { get; set; }
    public CreateChatCommand(string UserID,string FriendID)
    {
        this.UserId = UserID;
        this.FriendId = FriendID;
    }

}
