using MangaFlexFront.Data.Mangas.Models;
using MangaFlexFront.Data.Users.Models;

namespace MangaFlexFront.Data.Users.ViewModels;

public class GetUserProfileViewModel
{
    public User? User { get; set; }
    public IEnumerable<Manga>? LastWatched {  get; set; }    
    public IEnumerable<User>? Friends {  get; set; }
    public bool IsSub { get; set; } = false;
    public bool IsFriends { get; set; } = false;
}
