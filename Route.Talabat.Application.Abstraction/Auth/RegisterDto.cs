using System.ComponentModel.DataAnnotations;

namespace Route.Talabat.Application.Abstraction.Auth
{
    public class RegisterDto
    {
        [Required] 
        public required string DisplayName { get; set; }
        [Required]
        [EmailAddress]
        public required string UserName { get; set; }

        [Required]
        public required string Email { get; set; }
        [Required]
        public required string PhoneNumber { get; set; }

        [Required]
        [RegularExpression(@"^(?=.*[A-Z])(?=.*[a-z])(?=.*\d)(?=.*[^a-zA-Z\d]).{6,}$", ErrorMessage = "Password must be at least 6 characters long and contain one uppercase letter, one lowercase letter, one number, and one non-alphanumeric character.")]
        public required string Password { get; set; }

    }
}
