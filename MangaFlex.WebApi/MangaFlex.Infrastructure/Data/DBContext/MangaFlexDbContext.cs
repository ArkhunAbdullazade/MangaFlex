namespace MangaFlex.Infrastructure.Data.DBContext;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MangaFlex.Core.Data.Users.Models;
using MangaFlex.Core.Data.FriendShip.Models;
using MangaFlex.Core.Data.LastWatches.Models;
using MangaFlex.Core.Data.UserMangas.Models;
using MangaFlex.Core.Data.RefreshToken.Model;
using MangaFlex.Core.Data.FriendsChat.Models;
using MangaFlex.Core.Data.Message.Models;

public class MangaFlexDbContext : IdentityDbContext<User, IdentityRole, string>
{
    public DbSet<LastWatch> UserLastWatches { get; set; }
    public DbSet<FriendShip> FriendShips { get; set; }
    public DbSet<UserManga> UserMangas { get; set; }
    public DbSet<RefreshToken> RefreshTokens { get; set; }
    public DbSet<FriendsChat> FriendsChats { get; set; }
    public DbSet<Message> Messages { get; set; }
    public MangaFlexDbContext(DbContextOptions<MangaFlexDbContext> options) : base(options) {}
}

