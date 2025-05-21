using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Institute_Of_Fine_Arts.Models
{
    public class RegisterViewModel : IdentityUser
    {
        [Required]
        public string? FullName { get; set; }

        [Required]
        [EmailAddress]
        public string? Email { get; set; }

        [Required]
        public string? Password { get; set; }

        [Required]
        public string? Role { get; set; }

        // public DateTime? AdmissionDate { get; set; }

        // public DateTime? JoiningDate { get; set; }

        // public string? SubjectHandled { get; set; }
    }

}
