using MangaFlex.Core.Data.Mangas.Models;
using MangaFlex.Core.Data.Users.ViewModels;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MangaFlex.Core.Data.Users.Commands;

public class GetUserLastWatchesCommand : IRequest<IEnumerable<Manga>>
{
    public string? UserId { get; set; }

    public GetUserLastWatchesCommand(string? userId)
    {
        UserId = userId;
    }

    public GetUserLastWatchesCommand() { }
}
