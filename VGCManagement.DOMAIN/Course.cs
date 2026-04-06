using System.ComponentModel.DataAnnotations;

namespace VGCManagement.DOMAIN
{
    public class Course : IValidatableObject
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "The name is mandatory")]
        [StringLength(100, MinimumLength = 3)]
        public string Name { get; set; } = string.Empty;
        public int BranchId { get; set; }
        public virtual Branch? Branch { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public virtual ICollection<Exam>? Exam { get; set; }
        public virtual ICollection<CourseEnrolment>? CourseEnrolment { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (EndDate <= StartDate)
            {
                yield return new ValidationResult(
                    "End date must be greater than Start date",
                    new[] { nameof(EndDate) }
                );
            }
        }
    }
}
