using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using VGCManagement.DOMAIN;

namespace VGCManagement.VMC.Data
{
    public static class DbInitializer
    {
        public static async Task SeedData(ApplicationDbContext context, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            // 1. Ensure that the database exists and has the latest migrations
            context.Database.Migrate();

            // 2. Create Roles
            string[] roles = { "Admin", "Faculty", "Student" };
            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    await roleManager.CreateAsync(new IdentityRole(role));
                }
            }

            // 3. Create test users (Helper method below)
            var adminUser = await CreateUser(userManager, "admin@vgc.ie", "Admin123!", "Admin");
            var facultyUser = await CreateUser(userManager, "teacher@vgc.ie", "Faculty123!", "Faculty");
            var studentUser = await CreateUser(userManager, "student@vgc.ie", "Student123!", "Student");

            // 4. Business Data Seed (Only if the table is empty)
            if (!context.Branches.Any())
            {
                var dublinBranch = new Branch { Name = "Dublin Main Campus", Address = "123 O'Connell St" };
                context.Branches.Add(dublinBranch);
                await context.SaveChangesAsync();

                var csharpCourse = new Course
                {
                    Name = "Web Development with ASP.NET",
                    BranchId = dublinBranch.Id,
                    StartDate = DateTime.Now,
                    EndDate = DateTime.Now.AddMonths(3)
                };
                context.Courses.Add(csharpCourse);
                await context.SaveChangesAsync();

                // 5. Link Identity with Domain Profiles
                if (facultyUser != null)
                {
                    context.FacultyProfiles.Add(new FacultyProfile
                    {
                        IdentityUserId = facultyUser.Id,
                        Name = "Dr. John Doe",
                        Email = facultyUser.Email
                    });
                }

                if (studentUser != null)
                {
                    context.StudentProfiles.Add(new StudentProfile
                    {
                        IdentityUserId = studentUser.Id,
                        StudentNumber = "VGC-2024-001",
                        FullName = "Jane Smith",
                        Email = studentUser.Email
                    });
                }

                await context.SaveChangesAsync();

                if (!context.Assignments.Any())
                {
                    context.Assignments.AddRange(
                        new Assignment
                        {
                            Title = "Mid-term Project",
                            MaxScore = 100,
                            DueDate = DateTime.Now.AddDays(14),
                            CourseId = csharpCourse.Id
                        },
                        new Assignment
                        {
                            Title = "Final Exam Theory",
                            MaxScore = 50,
                            DueDate = DateTime.Now.AddMonths(2),
                            CourseId = csharpCourse.Id
                        }
                    );
                }

                
                if (!context.Exams.Any())
                {                    

                        context.Exams.Add(new Exam
                    {
                        Title = "Global Certification Exam",
                        Date = DateTime.Now.AddMonths(3),
                        CourseId = csharpCourse.Id,
                        ResultsReleased = false 
                    });
                }

                await context.SaveChangesAsync();
            }
        }

        private static async Task<IdentityUser?> CreateUser(UserManager<IdentityUser> userManager, string email, string password, string role)
        {
            var user = await userManager.FindByEmailAsync(email);
            if (user == null)
            {
                user = new IdentityUser { UserName = email, Email = email, EmailConfirmed = true };
                var result = await userManager.CreateAsync(user, password);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, role);
                }
            }
            return user;
        }
    }
}
