using MangaFlex.Core.Data.JWT.Options;
using MangaFlex.Core.Data.Users.ViewModels;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MangaFlex.Core.Data.Users.Commands;

public class UpdateTokenCommand : IRequest<TokenVM>
{
    public string Token { get; set; }
    public Guid RefreshToken {  get; set; }
    public JwtOptions JwtOptions { get; set; }
    public UpdateTokenCommand(string token,Guid guid, JwtOptions jwtOptions) 
    {
        Token = token;
        RefreshToken = guid;
        JwtOptions = jwtOptions;
    }
}
