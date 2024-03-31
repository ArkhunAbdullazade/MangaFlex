using MangaFlex.Core.Data.Users.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MangaFlex.Core.Data.Users.Commands;

public class GetUserSubscribeCommand : IRequest<IEnumerable<User>>
{
    public string UserID { get; set; }
    public GetUserSubscribeCommand(string userid)
    {
        this.UserID = userid;
    }
    public GetUserSubscribeCommand() { }
}
