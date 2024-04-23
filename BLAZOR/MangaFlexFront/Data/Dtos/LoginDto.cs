using System.ComponentModel.DataAnnotations;

namespace MangaFlexFront.Data.Dtos;

public class LoginDto
{
    [Required]
    public string? Login { get; set; }
    [Required]
    public string? Password { get; set; }
}
