using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace PopQuiz.Models.DTOs
{
    public class GenresDTO
    {
        [JsonPropertyName("genreId")] public int GenreID { get; set; }
        [JsonPropertyName("name")] public string Name { get; set; }
    }
}
