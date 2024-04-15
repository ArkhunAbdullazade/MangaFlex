using MangaFlex.Core.Data.Users.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MangaFlex.Core.Data.LastWatches.Models;

public class LastWatch
{
    public int Id { get; set; }
    public string? UserId { get; set; }
    public User? User { get; set; }
    public string? MangaId { get; set; }
}
