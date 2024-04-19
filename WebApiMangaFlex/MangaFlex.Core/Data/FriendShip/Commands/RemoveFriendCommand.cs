using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MangaFlex.Core.Data.FriendShip.Commands;

public class RemoveFriendCommand : IRequest
{
    public string? UserId { get; set; }
    public string? FriendId { get; set; }

    public RemoveFriendCommand(string? userId, string? friendId)
    {
        UserId = userId;
        FriendId = friendId;
    }

    public RemoveFriendCommand() { }
}
