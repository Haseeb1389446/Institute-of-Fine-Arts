using System.ComponentModel.DataAnnotations;

namespace Institute_Of_Fine_Arts.Models
{
    public class LoginViewModel
    {
        [Required]
        [EmailAddress]
        public string? Email { get; set; }

        [Required]
        [MinLength(8,ErrorMessage = "Password must contain 8 characters")]
        public string? Password { get; set; }

        public bool RememberMe { get; set; }

    }
}
