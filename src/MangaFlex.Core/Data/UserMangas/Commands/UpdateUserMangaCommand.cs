namespace MangaFlex.Core.Data.UserMangas.Commands;

using MangaFlex.Core.Data.UserMangas.Models;
using MediatR;

public class UpdateUserMangaCommand : IRequest<bool>
{
    public UserManga? UserManga { get; set; }

    public UpdateUserMangaCommand(UserManga userManga)
    {
        UserManga = userManga;
    }

    public UpdateUserMangaCommand() { }
}
