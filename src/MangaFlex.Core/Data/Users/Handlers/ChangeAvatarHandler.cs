using MangaFlex.Core.Data.Users.Commands;
using MangaFlex.Core.Data.Users.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MangaFlex.Core.Data.Users.Handlers;

public class ChangeAvatarHandler : IRequestHandler<ChangeAvatarCommand>
{
    private readonly IUserService userService;
    public ChangeAvatarHandler(IUserService userService)
    {
        this.userService = userService;
    }
    public async Task Handle(ChangeAvatarCommand request, CancellationToken cancellationToken)
    {
        await userService.UpdateAvatar(request.Path, request.UserId);
    }
}
