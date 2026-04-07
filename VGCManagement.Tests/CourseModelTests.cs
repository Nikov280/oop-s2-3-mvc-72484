using System.ComponentModel.DataAnnotations;
using VGCManagement.DOMAIN;
using Xunit;

namespace VGCManagement.Tests
{
    public class CourseModelTests
    {
        [Fact]
        public void Course_Validation_ShouldFail_WhenNameIsEmpty()
        {
            
            var course = new Course
            {
                Name = "", 
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddMonths(1)
            };

            
            var context = new ValidationContext(course);
            var results = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(course, context, results, true);

            
            Assert.False(isValid);
            Assert.Contains(results, r => r.MemberNames.Contains("Name"));
        }

        [Fact]
        public void Course_Validation_ShouldFail_WhenEndDateIsBeforeStartDate()
        {
            
            var course = new Course
            {
                Name = "Test Course",
                StartDate = new DateTime(2026, 1, 1),
                EndDate = new DateTime(2025, 12, 31) 
            };

            
            var context = new ValidationContext(course);
            var results = new List<ValidationResult>();
            
            var isValid = Validator.TryValidateObject(course, context, results, true);

            
            Assert.False(isValid);
            Assert.Contains(results, r => r.ErrorMessage == "End date must be greater than Start date");
        }

        [Fact]
        public void StudentProfile_Validation_ShouldFail_WhenStudentNumberIsInvalid()
        {
            
            var student = new StudentProfile
            {
                FullName = "Test Student",
                Email = "test@vgc.ie",
                StudentNumber = "123" 
            };

            
            var context = new ValidationContext(student);
            var results = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(student, context, results, true);
                        
            Assert.False(isValid);
        }

        [Fact]
        public void Enrollment_Validation_ShouldFail_WhenIdsAreMissing()
        {
            
            var enrollment = new CourseEnrolment
            {
                StudentProfileId = 0,
                CourseId = 0
            };
            
            var context = new ValidationContext(enrollment);
            var results = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(enrollment, context, results, true);
                        
            Assert.False(isValid);
        }

        [Fact]
        public void AttendanceRecord_Validation_ShouldFail_WhenWeekNumberIsOutOfRange()
        {
            
            var attendance = new AttendanceRecord
            {
                CourseEnrolmentId = 1,
                WeekNumber = 60, 
                Present = true
            };

            
            var context = new ValidationContext(attendance);
            var results = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(attendance, context, results, true);

            
            Assert.False(isValid);
            Assert.Contains(results, r => r.MemberNames.Contains("WeekNumber"));
        }

        [Fact]
        public void StudentProfile_Validation_ShouldFail_WhenEmailIsInvalid()
        {
            
            var student = new StudentProfile
            {
                FullName = "Agustin Test",
                StudentNumber = "VGC-1234",
                Email = "this-is-no-email" 
            };

            
            var context = new ValidationContext(student);
            var results = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(student, context, results, true);

            
            Assert.False(isValid);
            Assert.Contains(results, r => r.MemberNames.Contains("Email"));
        }

        [Fact]
        public void ExamResult_Validation_ShouldPass_WhenDataIsValid()
        {
            
            var examResult = new ExamResult
            {
                ExamId = 1,
                StudentProfileId = 1,
                Score = 85,
                Grade = "A"
            };

            
            var context = new ValidationContext(examResult);
            var results = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(examResult, context, results, true);

            
            Assert.True(isValid);
            Assert.Empty(results);
        }

        [Fact]
        public void CourseEnrolment_Validation_ShouldFail_WhenRequiredNavigationPropertiesAreMissing()
        {
            
            var enrolment = new CourseEnrolment
            {                
                EnrolDate = DateTime.Now
            };
            
            var context = new ValidationContext(enrolment);
            var results = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(enrolment, context, results, true);
                        
            Assert.False(isValid);
        }

        [Fact]
        public void Course_Validation_ShouldPass_WhenDataIsValid()
        {
            
            var course = new Course
            {
                Name = "Web Development",
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddMonths(3)
            };

            
            var context = new ValidationContext(course);
            var results = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(course, context, results, true);

            
            Assert.True(isValid);
        }
    }
}