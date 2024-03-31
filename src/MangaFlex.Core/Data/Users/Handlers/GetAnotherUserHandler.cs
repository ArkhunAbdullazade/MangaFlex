using MangaFlex.Core.Data.Users.Commands;
using MangaFlex.Core.Data.Users.Models;
using MangaFlex.Core.Data.Users.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MangaFlex.Core.Data.Users.Handlers;

public class GetAnotherUserHandler : IRequestHandler<GetAnotherUserCommand, IEnumerable<User>>
{
    private readonly IUserService userService;
    public GetAnotherUserHandler(IUserService userService)
    {
        this.userService = userService;
    }
    public async Task<IEnumerable<User>> Handle(GetAnotherUserCommand request, CancellationToken cancellationToken)
    {
        var result = await userService.GetAnotherPeopleAsync();
        return result;
    }
}
