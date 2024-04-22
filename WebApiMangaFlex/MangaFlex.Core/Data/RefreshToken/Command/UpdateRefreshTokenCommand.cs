using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MangaFlex.Core.Data.RefreshToken.Command;

public class UpdateRefreshTokenCommand : IRequest<Guid>
{
    public Guid Token { get; set; }
    public string UserId { get; set; }
    public UpdateRefreshTokenCommand(string userId, Guid token)
    {
        UserId = userId;
        Token = token;
    }
}
