namespace MangaFlex.Core.Data.Users.Commands;

using MangaFlex.Core.Data.Mangas.Models;
using MediatR;

public class FindMangasCommand : IRequest<IEnumerable<Manga>>
{
    public string? Query { get; set; }

    public FindMangasCommand(string query)
    {
        Query = query;
    }

    public FindMangasCommand() { }
}
