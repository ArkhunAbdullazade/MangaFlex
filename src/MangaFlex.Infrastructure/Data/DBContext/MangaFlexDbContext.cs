namespace MangaFlex.Infrastructure.Data.DBContext;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MangaFlex.Core.Data.Users.Models;
using MangaFlex.Core.Data.Users.Models.ManyToMany;

public class MangaFlexDbContext : IdentityDbContext<User, IdentityRole, string>
{
    public DbSet<LastWatch> UserLastWatch { get; set; }
    public DbSet<FriendShip> FriendShip { get; set; }
    public MangaFlexDbContext(DbContextOptions<MangaFlexDbContext> options) : base(options) {}
  
}

