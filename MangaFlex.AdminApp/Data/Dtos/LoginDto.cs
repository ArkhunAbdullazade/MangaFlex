using System.ComponentModel.DataAnnotations;

namespace MangaFlex.AdminApp.Data.Dtos;

public class LoginDto
{
    [Required]
    public string? Login { get; set; }
    [Required]
    public string? Password { get; set; }
}
