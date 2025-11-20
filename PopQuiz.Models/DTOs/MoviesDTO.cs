using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace PopQuiz.Models.DTOs
{
    public class MoviesDTO
    {
        [JsonPropertyName("movieId")] public int MovieID { get; set; }
        [JsonPropertyName("title")] public string Title { get; set; }
        [JsonPropertyName("releaseYear")] public int ReleaseYear { get; set; }
        [JsonPropertyName("genreId")] public int? GenreID { get; set; }
        [JsonPropertyName("directorId")] public int? DirectorID { get; set; }
        [JsonPropertyName("rating")] public decimal? Rating { get; set; }

        public GenresDTO? Genre { get; set; }
        public DirectorsDTO? Director { get; set; }
    }
}
