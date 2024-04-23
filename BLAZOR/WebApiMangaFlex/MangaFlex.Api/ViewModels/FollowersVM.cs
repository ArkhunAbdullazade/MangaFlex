using MangaFlex.Core.Data.Users.Models;

namespace MnagaFlex.Api.ViewModels;

public class FollowersVM
{
    public IEnumerable<User>? Followers { get; set;}
    public IEnumerable<User>? Subscriptions { get; set; }
}