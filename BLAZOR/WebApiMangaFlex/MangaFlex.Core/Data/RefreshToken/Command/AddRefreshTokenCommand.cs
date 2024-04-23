using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MangaFlex.Core.Data.RefreshToken.Command;

public class AddRefreshTokenCommand : IRequest<Guid>
{
    public string UserId {  get; set; }
    public AddRefreshTokenCommand(string userId)
    {
        UserId = userId;
    }
}
