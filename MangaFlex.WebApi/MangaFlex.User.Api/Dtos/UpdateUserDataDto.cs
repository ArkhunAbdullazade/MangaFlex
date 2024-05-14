using System.ComponentModel.DataAnnotations;

namespace MangaFlex.Api.Dtos;

public class UpdateUserDataDto
{
    public string Password { get; set; }
    public string OldPassword { get; set; }
    public string UserName { get; set; }

}
