namespace MangaFlex.Core.Data.UserMangas.Commands;

using MangaFlex.Core.Data.UserMangas.Models;
using MediatR;

public class AddUserMangaCommand : IRequest<bool>
{
    public UserManga? UserManga { get; set; }

    public AddUserMangaCommand(UserManga userManga)
    {
        UserManga = userManga;
    }

    public AddUserMangaCommand() { }
}
