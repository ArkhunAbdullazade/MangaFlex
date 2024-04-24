namespace MangaFlex.Core.Data.Users.Commands;

using MangaFlex.Core.Data.Mangas.Models;
using MediatR;

public class GetMangaByIdCommand : IRequest<Manga>
{
    public string Id { get; set; }

    public GetMangaByIdCommand(string id)
    {
        Id = id;
    }

    public GetMangaByIdCommand() { }
}
