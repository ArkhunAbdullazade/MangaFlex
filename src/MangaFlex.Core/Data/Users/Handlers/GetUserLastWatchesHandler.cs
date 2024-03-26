using MangaFlex.Core.Data.Mangas.Models;
using MangaFlex.Core.Data.Mangas.Services;
using MangaFlex.Core.Data.Users.Commands;
using MangaFlex.Core.Data.Users.Services;
using MangaFlex.Core.Data.Users.ViewModels;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MangaFlex.Core.Data.Users.Handlers;

internal class GetUserLastWatchesHandler : IRequestHandler<GetUserLastWatchesCommand, IEnumerable<Manga>>
{
    private readonly IUserService userService;
    private readonly IMangaService mangaService;

    public GetUserLastWatchesHandler(IUserService userService, IMangaService mangaService)
    {
        this.userService = userService;
        this.mangaService = mangaService;
    }
    public async Task<IEnumerable<Manga>> Handle(GetUserLastWatchesCommand request, CancellationToken cancellationToken)
    {
        var user = await userService.GetByIdAsync(request.UserId!);
        var lastwatched = await userService.GetLastWatchAsync(request.UserId!);
        var lastWatched = new List<Manga>();

        foreach (var manga in lastwatched)
        {
            lastWatched.Add(await mangaService.GetByIdAsync(manga.MangaId!));
        }


        return lastWatched;
    }
}

