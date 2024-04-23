using MediatR;
using MangaFlex.Core.Data.UserMangas.Repositories;
using MangaFlex.Core.Data.UserMangas.Commands;
using MangaFlex.Core.Data.UserMangas.Models;

namespace MangaFlex.Core.Data.UserMangas.Handlers;

public class GetByUserAndMangaIdHandler : IRequestHandler<GetByUserAndMangaIdCommand, UserManga?>
{
    private readonly IUserMangaRepository userMangaRepository;

    public GetByUserAndMangaIdHandler(IUserMangaRepository userMangaRepository)
    {
        this.userMangaRepository = userMangaRepository;
    }
    public async Task<UserManga?> Handle(GetByUserAndMangaIdCommand request, CancellationToken cancellationToken)
    {
        return await userMangaRepository.GetByUserAndMangaIdAsync(request.UserId, request.MangaId);
    }
}