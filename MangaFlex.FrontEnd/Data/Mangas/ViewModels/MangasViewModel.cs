using MangaFlexFront.Data.Mangas.Models;

namespace MangaFlexFront.Data.Mangas.ViewModels
{
    public class MangasViewModel
    {
        public IEnumerable<Manga>? Mangas { get; set; }
        public int Page { get; set; }
        public int TotalPages { get; set; }
        public string? Search { get; set; }
    }
}