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

public class GetFollowersHandler : IRequestHandler<GetFollowersCommand, IEnumerable<User>>
{
    private readonly IUserService userService;
    public GetFollowersHandler(IUserService userService)
    {
        this.userService = userService;
    }

    public async Task<IEnumerable<User>> Handle(GetFollowersCommand request, CancellationToken cancellationToken)
    {
        var result = await userService.GetFollowersAsync(request.UserID);
        return result;
    }
}
