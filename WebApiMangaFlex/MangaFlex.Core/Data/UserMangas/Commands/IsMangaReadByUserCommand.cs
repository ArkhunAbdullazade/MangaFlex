namespace MangaFlex.Core.Data.UserMangas.Commands;

using MangaFlex.Core.Data.UserMangas.Models;
using MediatR;

public class IsMangaReadByUserCommand : IRequest<bool>
{
    public string? UserId { get; set; }
    public string? MangaId { get; set; }
    public IsMangaReadByUserCommand(string? userId, string? mangaId)
    {
        UserId = userId;
        MangaId = mangaId;
    }

    public IsMangaReadByUserCommand() { }
}
