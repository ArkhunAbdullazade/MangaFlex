using MangaFlexFront.Data.Users.Models;

namespace MangaFlexFront.Data.LastWatches.Models;

public class LastWatch
{
    public int Id { get; set; }
    public string? UserId { get; set; }
    public User? User { get; set; }
    public string? MangaId { get; set; }
}
