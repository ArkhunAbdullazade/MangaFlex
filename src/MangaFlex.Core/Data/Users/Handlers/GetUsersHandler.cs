using MangaFlex.Core.Data.Users.Commands;
using MangaFlex.Core.Data.Users.Models;
using MangaFlex.Core.Data.Users.Repository;
using MediatR;

namespace MangaFlex.Core.Data.Users.Handlers;

public class GetUsersHandler : IRequestHandler<GetUsersCommand, IEnumerable<User>>
{
    private readonly IUserRepository userRepository;
    public GetUsersHandler(IUserRepository userService)
    {
        this.userRepository = userService;
    }
    public async Task<IEnumerable<User>> Handle(GetUsersCommand request, CancellationToken cancellationToken)
    {
        var result = await userRepository.GetUsersAsync();
        return result;
    }
}
