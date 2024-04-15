namespace MangaFlex.Core.Data.Users.ViewModels;

using MangaFlex.Core.Data.Mangas.Models;
using MangaFlex.Core.Data.Users.Models;

public class GetUserProfileViewModel
{
    public User? User { get; set; }
    public IEnumerable<Manga>? LastWatched {  get; set; }    
    public IEnumerable<User>? Friends {  get; set; }
    public bool IsSub { get; set; } = false;
    public bool IsFriends { get; set; } = false;
}
