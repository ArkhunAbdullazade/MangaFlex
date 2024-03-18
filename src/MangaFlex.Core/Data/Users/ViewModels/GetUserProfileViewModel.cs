namespace MangaFlex.Core.Data.Users.ViewModels;

using MangaFlex.Core.Data.Mangas.Models;
using MangaFlex.Core.Data.Users.Models;

public class GetUserProfileViewModel
{
    public User User { get; set; }
    public IEnumerable<Manga> Mangas {  get; set; }    
}
