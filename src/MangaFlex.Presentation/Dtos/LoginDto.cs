﻿using System.ComponentModel.DataAnnotations;

namespace MangaFlex.Presentation.Dto;

public class RegistrationDto
{
    [EmailAddress]
    [Required]
    public string? Email { get; set; }
    [Required]
    public string? Login { get; set; }
    [Required]
    public string? Password { get; set; }
}
