namespace PopQuiz.MVC.Models.ViewModels
{
    public class MovieViewModels
    {
        public int MovieId { get; set; }

        public string Title { get; set; } = null!;

        public int ReleaseYear { get; set; }

        public int? GenreId { get; set; }

        public int? DirectorId { get; set; }

        public decimal? Rating { get; set; }

        public bool? IsAdult { get; set; }

    }
}
