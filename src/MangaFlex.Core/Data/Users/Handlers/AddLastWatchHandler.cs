using MangaFlex.Core.Data.Users.Commands;
using MangaFlex.Core.Data.Users.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MangaFlex.Core.Data.Users.Handlers;

public class AddLastWatchHandler : IRequestHandler<AddLastWatchCommand>
{
    private readonly IUserService userService;
    public AddLastWatchHandler(IUserService userService)
    {
        this.userService = userService;
    }

    public async Task Handle(AddLastWatchCommand request, CancellationToken cancellationToken)
    {
        await userService.AddLastWatchAsync(request.MangaId!, request.UserId!);
    }
}
