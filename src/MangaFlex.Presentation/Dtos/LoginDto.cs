using System.ComponentModel.DataAnnotations;

namespace MangaFlex.Presentation.Dto;

public class LoginDto
{
    [Required]
    public string? Login { get; set; }
    [Required]
    public string? Password { get; set; }
}
