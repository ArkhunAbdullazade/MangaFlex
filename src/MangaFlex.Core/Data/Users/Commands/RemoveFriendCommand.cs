using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MangaFlex.Core.Data.Users.Commands;

public class RemoveFriendCommand : IRequest
{
    public string? UserId { get; set; }
    public string? FriendId { get; set; }

    public RemoveFriendCommand(string? userId, string? friendId)
    {
        this.UserId = userId;
        this.FriendId = friendId;
    }

    public RemoveFriendCommand() { }
}
