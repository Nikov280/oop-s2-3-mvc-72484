using System.ComponentModel.DataAnnotations;

namespace VGCManagement.DOMAIN
{
    public class AssignmentResult
    {
        public int Id { get; set; }
        public int AssignmentId { get; set; }
        public int StudentProfileId { get; set; }
        public double Score { get; set; }
        public string? Feedback { get; set; }

        public virtual Assignment? Assignment { get; set; }
        public virtual StudentProfile? Student { get; set; }
    }
}
