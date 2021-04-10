using System.ComponentModel.DataAnnotations;

namespace ParentApplication
{
    public class LoginViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email Address")]
        public string Email { get; set; }
    }
}