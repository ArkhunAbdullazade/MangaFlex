using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MangaFlex.Core.Data.FriendShip.Commands;

public class SubscribeCommand : IRequest
{
    public string UserId { get; set; }
    public string UserToId { get; set; }
    public SubscribeCommand(string userId, string userToId)
    {
        UserId = userId;
        UserToId = userToId;
    }
}
