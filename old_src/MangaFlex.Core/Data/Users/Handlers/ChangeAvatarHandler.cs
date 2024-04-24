using MangaFlex.Core.Data.Users.Commands;
using MangaFlex.Core.Data.Users.Repository;
using MediatR;

namespace MangaFlex.Core.Data.Users.Handlers;

public class ChangeAvatarHandler : IRequestHandler<ChangeAvatarCommand>
{
    private readonly IUserRepository userRepository;
    public ChangeAvatarHandler(IUserRepository userService)
    {
        this.userRepository = userService;
    }
    public async Task Handle(ChangeAvatarCommand request, CancellationToken cancellationToken)
    {
        await userRepository.UpdateAvatarAsync(request?.Path!, request?.UserId!);
    }
}
