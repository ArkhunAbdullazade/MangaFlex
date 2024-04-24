namespace MangaFlex.Core.Data.Users.Commands;

using MangaFlex.Core.Data.Mangas.ViewModels;
using MediatR;

public class GetAvailableLanguagesCommand : IRequest<string[]?>
{
    public string Id { get; set; }

    public GetAvailableLanguagesCommand(string id)
    {
        Id = id;
    }

    public GetAvailableLanguagesCommand() { }
}
