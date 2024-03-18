using MangaFlex.Core.Data.Users.Commands;
using MangaFlex.Core.Data.Users.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MangaFlex.Core.Data.Users.Handlers;


public class DeleteLastWatchHandler : IRequestHandler<DeleteLastWatchCommand>
{
    private readonly IUserService userService;
    public DeleteLastWatchHandler(IUserService userService)
    {
        this.userService = userService;
    }

    public async Task Handle(DeleteLastWatchCommand request, CancellationToken cancellationToken)
    {
        await userService.DeleteLastWatchAsync(request.UserId, request.MangaId);
    }
}
