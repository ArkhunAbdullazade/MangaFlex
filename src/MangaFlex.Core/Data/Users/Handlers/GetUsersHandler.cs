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

public class GetUsersHandler : IRequestHandler<GetUsersCommand, IEnumerable<User>>
{
    private readonly IUserService userService;
    public GetUsersHandler(IUserService userService)
    {
        this.userService = userService;
    }
    public async Task<IEnumerable<User>> Handle(GetUsersCommand request, CancellationToken cancellationToken)
    {
        var result = await userService.GetUsersAsync();
        return result;
    }
}
