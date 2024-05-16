using Microsoft.AspNetCore.Identity;

namespace MangaFlex.AdminApp.Data.Users.Models;
public class User : IdentityUser
{
    public string? AvatarPath { get; set; }
}
