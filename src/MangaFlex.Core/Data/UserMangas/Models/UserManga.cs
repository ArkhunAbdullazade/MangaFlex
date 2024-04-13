using System.ComponentModel.DataAnnotations.Schema;
using MangaFlex.Core.Data.Mangas.Models;
using MangaFlex.Core.Data.Users.Models;

namespace MangaFlex.Core.Data.UserMangas.Models;

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