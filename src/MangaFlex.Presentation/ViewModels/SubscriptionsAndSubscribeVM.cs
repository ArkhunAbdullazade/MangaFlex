using MangaFlex.Core.Data.Users.Models;

namespace MangaFlex.Presentation.ViewModels;

public class SubscriptionsAndSubscribeVM
{
    public IEnumerable<User> Subscriptions { get; set;}
    public IEnumerable<User> Subscribe { get; set; }
}
