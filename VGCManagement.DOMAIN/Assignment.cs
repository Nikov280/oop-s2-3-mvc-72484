using System.ComponentModel.DataAnnotations;

namespace VGCManagement.DOMAIN
{
    public class Assignment
    {
        public int Id { get; set; }
        public int CourseId { get; set; }
        public string? Title { get; set; }
        public double MaxScore { get; set; }
        public DateTime DueDate { get; set; }

        public virtual Course? Course { get; set; }
    }
}
