using MangaFlex.Core.Data.Users.Commands;
using MangaFlex.Core.Data.Users.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MangaFlex.Core.Data.Users.Handlers;

public class SubscribeHandler : IRequestHandler<SubscribeCommand>
{
    private readonly IUserService userService;
    public SubscribeHandler(IUserService userService)
    {
        this.userService = userService;
    }

    public async Task Handle(SubscribeCommand request, CancellationToken cancellationToken)
    {
        await userService.AddFriendAsync(request.UserId, request.UserToId);
    }
}
