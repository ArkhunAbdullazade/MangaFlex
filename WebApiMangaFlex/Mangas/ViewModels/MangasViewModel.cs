using  WebApiMangaFlex.Mangas.Data.Mangas.Models;

namespace WebApiMangaFlex.Mangas.Data.Mangas.ViewModels
{
    public class MangasViewModel
    {
        public IEnumerable<Manga>? Mangas { get; set; }
        public int Page { get; set; }
        public int TotalPages { get; set; }
        public string? Search { get; set; }
    }
}