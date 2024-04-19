using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MangaFlex.Core.Data.LastWatches.Command;

public class IsMangaInLastWatchesCommand : IRequest<bool>
{
    public string? UserId { get; set; }
    public string? MangaId { get; set; }

    public IsMangaInLastWatchesCommand(string? userId, string? mangaId)
    {
        UserId = userId;
        MangaId = mangaId;
    }

    public IsMangaInLastWatchesCommand() { }
}
