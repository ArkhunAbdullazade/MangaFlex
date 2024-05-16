namespace MangaFlex.Core.Data.FriendsChat.Commands;

using MangaFlex.Core.Data.Message.Models;
using MediatR;
using MangaFlex.Core.Data.FriendsChat.Models;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class GetChatsCommand : IRequest<IEnumerable<FriendsChat>>
{
    public string UserId { get; set; }

    public GetChatsCommand(string userId)
    {
        UserId = userId;
    }   
}
