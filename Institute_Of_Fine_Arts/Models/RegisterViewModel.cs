using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Institute_Of_Fine_Arts.Models
{
    public class RegisterViewModel : IdentityUser
    {
        [Required]
        [Display(Name = "Full Name")]
        public string? FullName { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string? Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string? Password { get; set; }

        [Display(Name = "Role")]
        public string? Role { get; set; }

        public DateTime? AdmissionDate { get; set; }
        public DateTime? JoiningDate { get; set; }

        [Display(Name = "Subject Handled")]
        public string? SubjectHandled { get; set; }
    }

}
