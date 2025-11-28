namespace PopQuiz.MVC.Models.ViewModels
{
    public class UserViewModels
    {
        public int UserId { get; set; }

        public string Password { get; set; } = null!;

        public string Name { get; set; } = null!;

        public string LastName { get; set; } = null!;

        public string Email { get; set; } = null!;

        public DateTime DateBirth { get; set; }

        public int RoleId { get; set; }

    }
}
