namespace MangaFlex.Core.Data.Users.Commands;

using MangaFlex.Core.Data.Mangas.ViewModels;
using MediatR;

public class ReadMangaCommand : IRequest<MangaChapterViewModel>
{
    public string Id { get; set; }
    public int Chapter { get; set; }
    public string Language { get; set; }

    public ReadMangaCommand(string id, int chapter, string language) 
    {
        this.Id = id;
        this.Chapter = chapter;
        this.Language = language;
    }
    public ReadMangaCommand() {}
}
