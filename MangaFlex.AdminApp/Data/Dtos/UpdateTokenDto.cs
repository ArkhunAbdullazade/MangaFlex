namespace MangaFlex.Api.Dtos;

public class UpdateTokenDto
{
    public string AccessToken { get; set; }
    public Guid RefreshToken { get; set; }
}
