using System.ComponentModel.DataAnnotations;

namespace VGCManagement.DOMAIN
{
    public class CourseEnrolment
    {
        public int Id { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "A valid student must be selected")]
        public int StudentProfileId { get; set; }
        public virtual StudentProfile? StudentProfile { get; set; }

        public string StudentName { get; set; } = string.Empty;

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "A valid course must be selected")]
        public int CourseId { get; set; }

        public virtual Course? Course { get; set; }
        public DateTime EnrolDate { get; set; }         
                      
    }
}
