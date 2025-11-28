using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace PopQuiz.Models.DTOs
{
    public class RoleDTO
    {
        [JsonPropertyName("roleId")]
        public int RoleId { get; set; }
        [JsonPropertyName("role1")]

        public string Role1 { get; set; } = null!;
    }
}
