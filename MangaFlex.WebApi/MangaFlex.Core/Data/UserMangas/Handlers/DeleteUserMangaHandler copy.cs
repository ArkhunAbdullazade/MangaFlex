using MediatR;
using MangaFlex.Core.Data.UserMangas.Repositories;
using MangaFlex.Core.Data.UserMangas.Commands;

namespace MangaFlex.Core.Data.UserMangas.Handlers;

public class DeleteUserMangaHandler : IRequestHandler<DeleteUserMangaCommand, bool>
{
    private readonly IUserMangaRepository userMangaRepository;

    public DeleteUserMangaHandler(IUserMangaRepository userMangaRepository)
    {
        this.userMangaRepository = userMangaRepository;
    }
    public async Task<bool> Handle(DeleteUserMangaCommand request, CancellationToken cancellationToken)
    {
        return await this.userMangaRepository.DeleteUserMangaAsync(request.UserId, request.MangaId);
    }
}