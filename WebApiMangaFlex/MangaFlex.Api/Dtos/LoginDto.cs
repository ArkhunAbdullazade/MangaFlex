using System.ComponentModel.DataAnnotations;

namespace MangaFlex.Api.Dto;

public class LoginDto
{
    [Required]
    public string? Login { get; set; }
    [Required]
    public string? Password { get; set; }
}
