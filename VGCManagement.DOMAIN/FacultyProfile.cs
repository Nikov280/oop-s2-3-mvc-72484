using System.ComponentModel.DataAnnotations;

namespace VGCManagement.DOMAIN
{
    public class FacultyProfile
    {
        public int Id { get; set; }

        
        public string EmployeeNumber { get; set; } = string.Empty;
        
        public string Name { get; set; } = string.Empty;
        public string? Email { get; set; }

        public string? PhoneNumber { get; set; }

        public string? IdentityUserId { get; set; }
    }
}
