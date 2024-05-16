namespace MangaFlex.AdminApp.Data.Dtos;

public class UpdateTokenDto
{
    public string AccessToken { get; set; }
    public Guid RefreshToken { get; set; }
}
