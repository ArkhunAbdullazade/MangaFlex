using MangaFlex.Core.Data.Users.ViewModels;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MangaFlex.Core.Data.LastWatches.Command;

public class GetUserProfileCommand : IRequest<GetUserProfileViewModel>
{
    public string? UserId { get; set; }

    public GetUserProfileCommand(string? userId)
    {
        UserId = userId;
    }

    public GetUserProfileCommand() { }
}
