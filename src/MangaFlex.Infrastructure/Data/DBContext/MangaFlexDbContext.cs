namespace MangaFlex.Infrastructure.Data.DBContext;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MangaFlex.Core.Data.User.Models;


public class MangaFlexDbContext : IdentityDbContext<IdentityUser, IdentityRole, string>
{
    public DbSet<User> users { get; set; }

    public MangaFlexDbContext(DbContextOptions<MangaFlexDbContext> options) : base(options) { }

}
