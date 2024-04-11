using MediatR;
using MangaFlex.Core.Data.UserMangas.Repositories;
using MangaFlex.Core.Data.UserMangas.Commands;

namespace MangaFlex.Core.Data.UserMangas.Handlers;

public class IsMangaReadByUserHandler : IRequestHandler<IsMangaReadByUserCommand, bool>
{
    private readonly IUserMangaRepository userMangaRepository;

    public IsMangaReadByUserHandler(IUserMangaRepository userMangaRepository)
    {
        this.userMangaRepository = userMangaRepository;
    }
    
    #pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
    public async Task<bool> Handle(IsMangaReadByUserCommand request, CancellationToken cancellationToken)
    {
        return this.userMangaRepository.IsMangaReadByUser(request.UserId, request.MangaId);
    }
    #pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously
}