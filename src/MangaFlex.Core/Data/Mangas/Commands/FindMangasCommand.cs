namespace MangaFlex.Core.Data.Users.Commands;

using MangaFlex.Core.Data.Mangas.Models;
using MediatR;

public class FindMangasCommand : IRequest<IEnumerable<Manga>>
{
    public string? Query { get; set; }
    public int Page { get; set; }

    public FindMangasCommand(string query, int page)
    {
        Query = query;
        Page = page;
    }

    public FindMangasCommand() { }
}
