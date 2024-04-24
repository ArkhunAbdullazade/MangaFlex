using MediatR;
using MangaFlex.Core.Data.UserMangas.Repositories;
using MangaFlex.Core.Data.UserMangas.Commands;

namespace MangaFlex.Core.Data.UserMangas.Handlers;

public class UpdateUserMangaHandler : IRequestHandler<UpdateUserMangaCommand, bool>
{
    private readonly IUserMangaRepository userMangaRepository;

    public UpdateUserMangaHandler(IUserMangaRepository userMangaRepository)
    {
        this.userMangaRepository = userMangaRepository;
    }
    public async Task<bool> Handle(UpdateUserMangaCommand request, CancellationToken cancellationToken)
    {
        return await this.userMangaRepository.UpdateUserMangaAsync(request.UserManga!);
    }
}