
using System.ComponentModel.DataAnnotations;

namespace DrivingExamScheduler.Domain.DTO
{
    public class CandidateRegistrationDto
    {
        [Required(ErrorMessage = "Name is required")]
        [StringLength(100)]
        public string? Name { get; set; }


        [EmailAddress(ErrorMessage = "Invalid email address")]
        [Required(ErrorMessage = "Email is required")]
        public string? Email { get; set; }
        [Required(ErrorMessage = "Password is required")]
        public string? Password { get; set; }

        [Required(ErrorMessage = "Confirm password is required")]
        [Compare("Password", ErrorMessage = "The Password and Confirm Password do not match.")]
        public string? ConfirmPassword { get; set; }

        [Required(ErrorMessage = "EMBG is required")]
        [StringLength(13)]
        public string? EMBG { get; set; }

        [Required(ErrorMessage = "Address is required")]
        [StringLength(100)]
        public string? Address { get; set; }
        [Required(ErrorMessage = "Phone is required")]
        public string? PhoneNumber { get; set; }

        [Required(ErrorMessage = "Driving school is required")]
        public string? DrivingSchool { get; set; }
    }
}