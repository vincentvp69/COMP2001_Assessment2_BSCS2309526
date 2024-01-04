using System.ComponentModel.DataAnnotations;

namespace trialapp.Models
{
    public class UserProfileViewModel
    {
        [Required(ErrorMessage = "Username is required.")]
        [StringLength(50, ErrorMessage = "Username cannot exceed 50 characters.")]
        public string Username { get; set; }

        [Required(ErrorMessage = "User Height is required.")]
        [Range(1, 300, ErrorMessage = "User Height must be between 1 and 300.")]
        public decimal? UserHeight { get; set; } // Change type to decimal?

        [Required(ErrorMessage = "User Weight is required.")]
        [Range(1, 1000, ErrorMessage = "User Weight must be between 1 and 1000.")]
        public decimal? UserWeight { get; set; } // Change type to decimal?

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email address.")]
        [StringLength(100, ErrorMessage = "Email cannot exceed 100 characters.")]
        public string Email { get; set; }
    }
}
