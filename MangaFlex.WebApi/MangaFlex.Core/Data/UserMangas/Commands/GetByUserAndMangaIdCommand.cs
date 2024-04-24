namespace MangaFlex.Core.Data.UserMangas.Commands;

using MangaFlex.Core.Data.UserMangas.Models;
using MediatR;

public class GetByUserAndMangaIdCommand : IRequest<UserManga?>
{
    public string? UserId { get; set; }
    public string? MangaId { get; set; }

    public GetByUserAndMangaIdCommand(string? userId, string? mangaId)
    {
        UserId = userId;
        MangaId = mangaId;
    }

    public GetByUserAndMangaIdCommand() { }
}
