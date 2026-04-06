using System.ComponentModel.DataAnnotations;

namespace VGCManagement.DOMAIN
{
    public class Exam
    {
        public int Id { get; set; }
        public int CourseId { get; set; }
        public string? Title { get; set; }
        public int MaxScore { get; set; }
        public bool ResultsReleased { get; set; }
        public Course? Course { get; set; }

        [Required(ErrorMessage = "Please select a date")]
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }
    }
}
