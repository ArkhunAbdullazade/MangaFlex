namespace MangaFlex.Core.Data.Mangas.ViewModels
{
    public class MangaChapterViewModel
    {
        public string? MangaId { get; set; }
        public IEnumerable<string>? Pages { get; set; }
        public int Chapter { get; set; }
        public int TotalChapters { get; set; }
    }
}