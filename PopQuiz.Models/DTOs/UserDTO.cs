using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace PopQuiz.Models.DTOs
{
    public class UserDTO
    {
        [JsonPropertyName("userId")]
        public int UserId { get; set; }

        [JsonPropertyName("password")]
        public string Password { get; set; } = null!;
        [JsonPropertyName("name")]

        public string Name { get; set; } = null!;
        [JsonPropertyName("lastName")]

        public string LastName { get; set; } = null!;
        [JsonPropertyName("email")]

        public string Email { get; set; } = null!;
        [JsonPropertyName("dateBirth")]

        public DateTime DateBirth { get; set; }
        [JsonPropertyName("roleId")]

        public int RoleId { get; set; }
        [JsonPropertyName("role")]

        public virtual RoleDTO Role { get; set; } = null!;
    }
}
