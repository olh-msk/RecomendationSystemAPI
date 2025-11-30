using RecomendatinSystemAPI.Data;
using RecomendatinSystemAPI.Models;

namespace RecomendationSystemAPI.Data
{
    public static class DbInitializer
    {
        public static void Seed(IApplicationBuilder app)
        {
            using var scope = app.ApplicationServices.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

            if (db.Courses.Any() || db.Students.Any() || db.InterestTags.Any())
                return; // якщо вже є дані — нічого не робимо

            // --- InterestTags ---
            var ai = new InterestTag { Name = "AI" };
            var backend = new InterestTag { Name = "Backend" };
            var frontend = new InterestTag { Name = "Frontend" };
            db.InterestTags.AddRange(ai, backend, frontend);
            db.SaveChanges();

            // --- Courses ---
            var course1 = new Course
            {
                Title = "Intro to AI",
                Description = "Basics of artificial intelligence and machine learning.",
                CreditHours = 3,
                Tags = new List<CourseTag>
            {
                new CourseTag { InterestTagId = ai.Id }
            }
            };

            var course2 = new Course
            {
                Title = "Web Backend Development",
                Description = "C# and ASP.NET Core fundamentals.",
                CreditHours = 3,
                Tags = new List<CourseTag>
            {
                new CourseTag { InterestTagId = backend.Id }
            }
            };

            var course3 = new Course
            {
                Title = "Frontend Basics",
                Description = "HTML, CSS, JavaScript and React.",
                CreditHours = 2,
                Tags = new List<CourseTag>
            {
                new CourseTag { InterestTagId = frontend.Id }
            }
            };

            db.Courses.AddRange(course1, course2, course3);

            // --- Student ---
            var student = new Student
            {
                FullName = "Ivan Ivanov",
                GPA = 88,
                Interests = new List<StudentInterest>
            {
                new StudentInterest { InterestTagId = ai.Id },
                new StudentInterest { InterestTagId = backend.Id }
            }
            };

            db.Students.Add(student);
            db.SaveChanges();
        }
    }
}
