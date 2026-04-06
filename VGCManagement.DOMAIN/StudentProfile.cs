using System.ComponentModel.DataAnnotations;

namespace VGCManagement.DOMAIN
{
    public class StudentProfile
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Student Number is required")]        
        [RegularExpression(@"^VGC-.*", ErrorMessage = "Student Number must start with 'VGC-'")]
        public string StudentNumber { get; set; } = string.Empty;
        [Required(ErrorMessage = "Full Name is required")]
        public string FullName { get; set; } = string.Empty;
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email format")]
        public string Email { get; set; } = string.Empty;
        public string? Phone { get; set; }
        public string? Address { get; set; }
        public DateTime DateOfBirth { get; set; }

        public ICollection<CourseEnrolment>? Enrolments { get; set; }

        public string? IdentityUserId { get; set; } // FK a AspNetUsers        
    }
}
