using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace PopQuiz.Models.DTOs
{
    public class DirectorsDTO
    {
        [JsonPropertyName("directorId")] public int DirectorID { get; set; }
        [JsonPropertyName("fullName")] public string FullName { get; set; }
    }
}
