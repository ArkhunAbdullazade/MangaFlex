using MangaFlex.Core.Data.Users.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MangaFlex.Core.Data.FriendShip.Commands;

public class GetUserSubscriptionsCommand : IRequest<IEnumerable<User>>
{
    public string UserId { get; set; }
    public GetUserSubscriptionsCommand(string userId)
    {
        UserId = userId;
    }
}
