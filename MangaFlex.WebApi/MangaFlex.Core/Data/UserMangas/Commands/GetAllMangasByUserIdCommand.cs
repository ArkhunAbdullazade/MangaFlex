namespace MangaFlex.Core.Data.UserMangas.Commands;

using MangaFlex.Core.Data.Mangas.Models;
using MangaFlex.Core.Data.UserMangas.Models;
using MediatR;

public class GetAllMangasByUserIdCommand : IRequest<IEnumerable<Manga>>
{
    public string? UserId { get; set; }

    public GetAllMangasByUserIdCommand(string? userId)
    {
        UserId = userId;
    }

    public GetAllMangasByUserIdCommand() { }
}
