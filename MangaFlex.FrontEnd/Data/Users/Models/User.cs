using Microsoft.AspNetCore.Identity;

namespace MangaFlexFront.Data.Users.Models;
public class User : IdentityUser
{
    public string? AvatarPath { get; set; }
}
