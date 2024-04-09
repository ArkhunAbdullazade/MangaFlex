namespace MangaFlex.Infrastructure.Data.DBContext;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MangaFlex.Core.Data.Users.Models;
using MangaFlex.Core.Data.FriendShip.Models;
using MangaFlex.Core.Data.LastWatches.Models;

public class MangaFlexDbContext : IdentityDbContext<User, IdentityRole, string>
{
    public DbSet<LastWatch> UserLastWatches { get; set; }
    public DbSet<FriendShip> FriendShips { get; set; }
    public MangaFlexDbContext(DbContextOptions<MangaFlexDbContext> options) : base(options) {}
}

