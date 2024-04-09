using MangaFlex.Core.Data.FriendShip.Repository;
using MangaFlex.Core.Data.LastWatches.Command;
using MangaFlex.Core.Data.LastWatches.Repository;
using MangaFlex.Core.Data.Mangas.Models;
using MangaFlex.Core.Data.Mangas.Services;
using MangaFlex.Core.Data.Users.Repository;
using MangaFlex.Core.Data.Users.ViewModels;
using MediatR;

namespace MangaFlex.Core.Data.Users.Handlers;

internal class GetUserProfileHandler : IRequestHandler<GetUserProfileCommand, GetUserProfileViewModel>
{
    private readonly IUserRepository userRepository;
    private readonly IMangaService mangaService;
    private readonly ILastWatchesRepository lastWatchesRepository;
    private readonly IFriendShipRepository friendShipRepository;

    public GetUserProfileHandler(IUserRepository userService, IMangaService mangaService,ILastWatchesRepository repository, IFriendShipRepository friendShipRepository)
    {
        this.userRepository = userService;
        this.mangaService = mangaService;
        this.lastWatchesRepository = repository;
        this.friendShipRepository = friendShipRepository;
    }
    public async Task<GetUserProfileViewModel> Handle(GetUserProfileCommand request, CancellationToken cancellationToken)
    {
        var user = await userRepository.GetByIdAsync(request.UserId!);
        var lastwatched = await lastWatchesRepository.GetLastWatchAsync(request.UserId!);
        var lastWatched = new List<Manga>();
        var friends = await friendShipRepository.GetSubscriptionsAsync(request.UserId!);

        foreach (var manga in lastwatched)
        {
            lastWatched.Add(await mangaService.GetByIdAsync(manga.MangaId!));
        }

        return new GetUserProfileViewModel()
        {
            User = user,
            LastWatched = lastWatched,
            Friends = friends,
        };
    }
}
