using System.ComponentModel.DataAnnotations;

namespace BookHaven.Core.DTO
{
    public class RegisterDto
    {
        [Required(ErrorMessage = "First name is required"), MaxLength(100)]
        public string FirstName { get; set; } = "";

        [Required(ErrorMessage = "Last name is required"), MaxLength(100)]
        public string LastName { get; set; } = "";

        [Required(ErrorMessage = "Email is required"), MaxLength(100)]
        [EmailAddress(ErrorMessage = "Please enter a valid email")]
        public string Email { get; set; } = "";

        [Required(ErrorMessage = "Password is required"), MinLength(6, ErrorMessage = "Password must be at least 6 characters long")]
        public string Password { get; set; } = "";

        [Required(ErrorMessage = "Confirming password is required")]
        [Compare("Password", ErrorMessage = "Passwords do not match")]
        public string ConfirmPassword { get; set; } = "";
    }
}
