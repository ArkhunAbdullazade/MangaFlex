using MangaFlex.Core.Data.Users.Commands;
using MangaFlex.Core.Data.Users.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MangaFlex.Core.Data.Users.Handlers;

public class IsMangaInLastWatchesHandler : IRequestHandler<IsMangaInLastWatchesCommand, bool>
{
    private readonly IUserService userService;
    public IsMangaInLastWatchesHandler(IUserService userService)
    {
        this.userService = userService;
    }

    public async Task<bool> Handle(IsMangaInLastWatchesCommand request, CancellationToken cancellationToken)
    {
        var result = await userService.IsMangaInLastWatchAsync(request.UserId, request.MangaId);
        return result;
    }
}
