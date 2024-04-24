using MangaFlexFront.Data.Users.Models;

namespace MangaFlexFront.Data.ViewModels;

public class FollowersVM
{
    public IEnumerable<User>? Followers { get; set;}
    public IEnumerable<User>? Subscriptions { get; set; }
}