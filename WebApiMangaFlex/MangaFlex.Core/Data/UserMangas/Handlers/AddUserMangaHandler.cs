using MediatR;
using MangaFlex.Core.Data.UserMangas.Repositories;
using MangaFlex.Core.Data.UserMangas.Commands;

namespace MangaFlex.Core.Data.UserMangas.Handlers;

public class AddUserMangaHandler : IRequestHandler<AddUserMangaCommand, bool>
{
    private readonly IUserMangaRepository userMangaRepository;

    public AddUserMangaHandler(IUserMangaRepository userMangaRepository)
    {
        this.userMangaRepository = userMangaRepository;
    }
    public async Task<bool> Handle(AddUserMangaCommand request, CancellationToken cancellationToken)
    {
        return await this.userMangaRepository.AddUserMangaAsync(request.UserManga!);
    }
}