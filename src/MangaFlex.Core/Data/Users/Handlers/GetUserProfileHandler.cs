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

internal class GetUserProfileHandler : IRequestHandler<GetUserProfileCommand, GetUserProfileViewModel>
{
    private readonly IUserService userService;
    private readonly IMangaService mangaService;

    public GetUserProfileHandler(IUserService userService,IMangaService mangaService)
    {
        this.userService = userService;
        this.mangaService = mangaService;
    }
    public async Task<GetUserProfileViewModel> Handle(GetUserProfileCommand request, CancellationToken cancellationToken)
    {
        var user = await userService.GetByIdAsync(request.UserId!);
        var lastwatched = await userService.GetLastWatchAsync(request.UserId!);
        var lastWatched = new List<Manga>();

        foreach(var manga in lastwatched)
        {
            lastWatched.Add(await mangaService.GetByIdAsync(manga.MangaId!));
        }

        return new GetUserProfileViewModel()
        {
            User = user,
            LastWatched = lastWatched,
        };
    }
}
