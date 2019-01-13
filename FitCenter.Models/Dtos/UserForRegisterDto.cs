using System.ComponentModel.DataAnnotations;

namespace FitCenter.Models.Dtos
{
    public class UserForRegisterDto
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Surname { get; set; }
        [Required]
        [EmailAddress(ErrorMessage = "Please enter a valid email address")]
        public string Email { get; set; }

        [Required]
        [StringLength(16, MinimumLength = 4, ErrorMessage = "You must specify a password between 4 and 16 characters")]
        public string Password { get; set; }
    }
}