using MangaFlex.Core.Data.Users.Models.ManyToMany;
using Microsoft.AspNetCore.Identity;

namespace MangaFlex.Core.Data.Users.Models;
public class User : IdentityUser
{
    public string? AvatarPath { get; set; }
}
