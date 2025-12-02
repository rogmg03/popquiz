using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace PopQuiz.MVCs.Models.ViewModels
{
    public class DirectorViewModels
    {
       
        public int DirectorId { get; set; }

        public string FullName { get; set; } = null!;

    }
}
