namespace MangaFlex.Infrastructure.Data.DBContext;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MangaFlex.Core.Data.Users.Models;


public class MangaFlexDbContext : IdentityDbContext<User, IdentityRole, string>
{
    public DbSet<LastWatch> UserLastWatch { get; set; }
    public MangaFlexDbContext(DbContextOptions<MangaFlexDbContext> options) : base(options) {}   
}
