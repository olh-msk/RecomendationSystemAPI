using Microsoft.EntityFrameworkCore;
using RecomendationSystemAPI.Helpers;
using RecomendationSystemAPI.Models;

namespace RecomendationSystemAPI.Data
{
    public static class DbInitializer
    {
        public static void Seed(IApplicationBuilder app)
        {
            using var scope = app.ApplicationServices.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

            // --- NOTE: Test accounts seeded below (dev only) ---
            // Teacher:
            //   Email: v.teacher@example.com
            //   Password: TeacherPass!
            //
            // Students:
            //   Email: ivan@example.com    Password: Password123!
            //   Email: maria@example.com   Password: Passw0rd!
            //   (You can add more in AddUser calls below; these are test users only.)
            //
            // If you do not want seeder to overwrite DB on each run, uncomment the check below:
            // if (db.Courses.Any() || db.Students.Any() || db.InterestTags.Any())
            //     return;

            // Clear existing (dev)
            db.Enrollments.RemoveRange(db.Enrollments.ToList());
            db.StudentInterests.RemoveRange(db.StudentInterests.ToList());
            db.CourseTags.RemoveRange(db.CourseTags.ToList());
            db.Courses.RemoveRange(db.Courses.ToList());
            db.Students.RemoveRange(db.Students.ToList());
            db.InterestTags.RemoveRange(db.InterestTags.ToList());
            db.SaveChanges();

            // Reset identity seeds (safe: check that table exists first)
            var tables = new[] { "Courses", "InterestTags", "Students", "CourseTags", "StudentInterests", "Enrollments" };
            foreach (var t in tables)
            {
                // only run DBCC if table exists (avoids 'Cannot find a table or object' error)
                var sql = $@"
                    IF OBJECT_ID(N'dbo.{t}', N'U') IS NOT NULL
                    BEGIN
                        DBCC CHECKIDENT ('dbo.{t}', RESEED, 0);
                    END
                    ";
                db.Database.ExecuteSqlRaw(sql);
            }

            // Tags
            var ai = new InterestTag { Name = "AI" };
            var backend = new InterestTag { Name = "Backend" };
            var frontend = new InterestTag { Name = "Frontend" };
            var data = new InterestTag { Name = "Data Science" };
            var devops = new InterestTag { Name = "DevOps" };
            var ux = new InterestTag { Name = "UX" };
            db.InterestTags.AddRange(ai, backend, frontend, data, devops, ux);
            db.SaveChanges();

            // Create a teacher first
            // Teacher credentials documented above (TeacherPass!)
            var (teacherHash, teacherSalt) = PasswordHasher.HashPassword("TeacherPass!");
            var teacher = new Student
            {
                FullName = "Dr. Viktor Teacher",
                Email = "v.teacher@example.com",
                PasswordHash = teacherHash,
                PasswordSalt = teacherSalt,
                GPA = 4.0f,
                Role = UserRole.Teacher
            };
            db.Students.Add(teacher);
            db.SaveChanges();

            // Courses authored by teacher
            var c1 = new Course
            {
                Title = "Intro to AI",
                Description = "Basics of artificial intelligence and machine learning.",
                CreditHours = 3,
                Tags = new List<CourseTag> { new CourseTag { InterestTagId = ai.Id }, new CourseTag { InterestTagId = data.Id } },
                CreatedById = teacher.Id
            };
            var c2 = new Course
            {
                Title = "Web Backend Development",
                Description = "C# and ASP.NET Core fundamentals.",
                CreditHours = 3,
                Tags = new List<CourseTag> { new CourseTag { InterestTagId = backend.Id } },
                CreatedById = teacher.Id
            };
            db.Courses.AddRange(c1, c2);
            db.SaveChanges();

            // Additional students (test credentials documented above)
            void AddUser(string fullName, string email, float gpa, List<int> interestIds, string password = "Password123!")
            {
                var (h, s) = PasswordHasher.HashPassword(password);
                var st = new Student
                {
                    FullName = fullName,
                    Email = email,
                    PasswordHash = h,
                    PasswordSalt = s,
                    GPA = gpa,
                    Role = UserRole.Student
                };
                db.Students.Add(st);
                db.SaveChanges();

                var sis = interestIds.Select(id => new StudentInterest { StudentId = st.Id, InterestTagId = id }).ToList();
                db.StudentInterests.AddRange(sis);
                db.SaveChanges();
            }

            // Default test users:
            // Ivan:  ivan@example.com    / Password123!
            // Maria: maria@example.com   / Passw0rd!
            AddUser("Ivan Ivanov", "ivan@example.com", 3.8f, new List<int> { ai.Id, backend.Id }, "Password123!");
            AddUser("Maria Petrenko", "maria@example.com", 3.2f, new List<int> { frontend.Id, ux.Id }, "Passw0rd!");

            // Add more teachers and courses for richer testing (dev only)
            void AddTeacher(string fullName, string email, string password)
            {
                var (h, s) = PasswordHasher.HashPassword(password);
                var t = new Student { FullName = fullName, Email = email, PasswordHash = h, PasswordSalt = s, GPA = 4.0f, Role = UserRole.Teacher };
                db.Students.Add(t);
                db.SaveChanges();

                // create 1-2 courses for this teacher
                var courseA = new Course { Title = $"{fullName} - Course A", Description = "Sample course A", CreditHours = 3, Tags = new List<CourseTag> { new CourseTag { InterestTagId = ai.Id } }, CreatedById = t.Id };
                var courseB = new Course { Title = $"{fullName} - Course B", Description = "Sample course B", CreditHours = 2, Tags = new List<CourseTag> { new CourseTag { InterestTagId = backend.Id } }, CreatedById = t.Id };
                db.Courses.AddRange(courseA, courseB);
                db.SaveChanges();
            }

            // Example teachers
            AddTeacher("Prof. Anna Kov", "anna.kov@example.com", "TeachPass1!");
            AddTeacher("Dr. Boris Lee", "boris.lee@example.com", "TeachPass2!");

            // Example enrollments
            var sIvan = db.Students.First(s => s.Email == "ivan@example.com");
            var enrolls = new List<Enrollment>
            {
                new Enrollment { StudentId = sIvan.Id, CourseId = c1.Id, Grade = 4.0f, Semester = "2024S1" }
            };
            db.Enrollments.AddRange(enrolls);
            db.SaveChanges();

            // add this debug check somewhere (DbInitializer after seeding) to verify hashing works:
            var seed = db.Students.FirstOrDefault(s => s.Email == "ivan@example.com");
            if (seed != null)
            {
                var ok = PasswordHasher.Verify("Password123!", seed.PasswordHash, seed.PasswordSalt);
                Console.WriteLine($"Seed verify ivan@example.com => {ok}");
            }
        }
    }
}
