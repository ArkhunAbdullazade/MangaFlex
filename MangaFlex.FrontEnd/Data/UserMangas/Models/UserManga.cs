using System.ComponentModel.DataAnnotations.Schema;
using MangaFlexFront.Data.Mangas.Models;
using MangaFlexFront.Data.Users.Models;

namespace MangaFlexFront.Data.UserMangas.Models;

public class UserManga
{
    public int Id { get; set; }
    [ForeignKey("UserId")]
    public string? UserId { get; set; }
    public virtual User? User { get; set; }
    public string? MangaId { get; set; }
    [NotMapped]
    public virtual Manga? Manga { get; set; }
    public string? Status { get; set; }
}