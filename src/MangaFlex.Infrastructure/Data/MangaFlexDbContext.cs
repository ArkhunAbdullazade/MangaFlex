using MangaFlex.Core.Data.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace MangaFlex.Infrastructure.Data;

public class MangaFlexDbContext : IdentityDbContext<IdentityUser, IdentityRole, string>
{
    public DbSet<User> users { get; set; }

    public MangaFlexDbContext(DbContextOptions<MangaFlexDbContext> options) : base(options) { }
    
}
