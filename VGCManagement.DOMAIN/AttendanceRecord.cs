using System.ComponentModel.DataAnnotations;

namespace VGCManagement.DOMAIN
{
    public class AttendanceRecord
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Please select an enrollment")]
        [Display(Name = "Enrollment")]
        public int CourseEnrolmentId { get; set; }

        [Required]
        [Range(1, 52)]
        public int WeekNumber { get; set; }
        public bool Present { get; set; }

        // Navigation property
        public virtual CourseEnrolment? CourseEnrolment { get; set; }
    }
}
