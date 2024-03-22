using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MangaFlex.Core.Data.Users.Commands;

public class ChangeAvatarCommand : IRequest
{
    public string? UserId { get; set; }
    public string? Path {  get; set; }

    public ChangeAvatarCommand(string? userId, string? path)
    {
        UserId = userId;
        Path = path;
    }
    public ChangeAvatarCommand() { }
}
