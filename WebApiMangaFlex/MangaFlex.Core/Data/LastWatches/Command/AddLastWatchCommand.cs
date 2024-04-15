using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MangaFlex.Core.Data.LastWatches.Command;

public class AddLastWatchCommand : IRequest
{
    public string? UserId { get; set; }
    public string? MangaId { get; set; }

    public AddLastWatchCommand(string? userId, string? mangaId)
    {
        UserId = userId;
        MangaId = mangaId;
    }

    public AddLastWatchCommand() { }
}
