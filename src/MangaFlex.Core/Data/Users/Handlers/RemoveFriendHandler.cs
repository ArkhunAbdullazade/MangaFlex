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


public class RemoveFriendHandler : IRequestHandler<RemoveFriendCommand>
{
    private readonly IUserService userService;
    public RemoveFriendHandler(IUserService userService)
    {
        this.userService = userService;
    }
    public async Task Handle(RemoveFriendCommand request, CancellationToken cancellationToken)
    {
        await userService.RemoveFriendAsync(request.UserId, request.FriendId);
    }
}
