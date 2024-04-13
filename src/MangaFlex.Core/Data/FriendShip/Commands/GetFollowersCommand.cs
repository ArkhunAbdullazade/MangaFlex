using MangaFlex.Core.Data.Users.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MangaFlex.Core.Data.FriendShip.Commands;

public class GetFollowersCommand : IRequest<IEnumerable<User>>
{
    public string UserID { get; set; }
    public GetFollowersCommand(string userid)
    {
        UserID = userid;
    }
}