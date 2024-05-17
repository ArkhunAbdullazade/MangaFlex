using System.Text.Json.Serialization;

namespace MangaFlex.Api.Dtos;
public class UserMangaDto
{
    [JsonPropertyName("mangaId")]
    public string? MangaId { get; set; }
    [JsonPropertyName("status")]
    public string? Status { get; set; }
}