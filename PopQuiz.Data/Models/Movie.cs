using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PopQuiz.Data.Models;

public partial class Movie
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int MovieId { get; set; }

    public string Title { get; set; } = null!;

    public int ReleaseYear { get; set; }

    public int? GenreId { get; set; }

    public int? DirectorId { get; set; }

    public decimal? Rating { get; set; }

    public bool? IsAdult { get; set; }

    public  Director? Director { get; set; }

    public  Genre? Genre { get; set; }
}
