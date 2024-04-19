namespace MangaFlex.Core.Data.UserMangas.Commands;

using MediatR;

public class DeleteUserMangaCommand : IRequest<bool>
{
    public string? UserId { get; set; }
    public string? MangaId { get; set; }
    public DeleteUserMangaCommand(string? userId, string? mangaId)
    {
        UserId = userId;
        MangaId = mangaId;
    }

    public DeleteUserMangaCommand() { }
}
