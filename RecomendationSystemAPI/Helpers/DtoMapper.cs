using RecomendationSystemAPI.Models;
using RecomendationSystemAPI.DTOs.Courses;
using RecomendationSystemAPI.DTOs.Enrollments;
using RecomendationSystemAPI.DTOs.InterestTags;
using RecomendationSystemAPI.DTOs.Students;

namespace RecomendationSystemAPI.Helpers
{
    public static class DtoMapper
    {
        public static StudentDto ToDto(Student stud) => new()
        {
            Id = stud.Id,
            FullName = stud.FullName,
            GPA = stud.GPA,
            InterestNames = stud.Interests?.Select(i => i.InterestTag.Name).ToList() ?? new(),
            Role = stud.Role.ToString()
        };

        public static CourseDto ToDto(Course course) => new()
        {
            Id = course.Id,
            Title = course.Title,
            Description = course.Description,
            CreditHours = course.CreditHours,
            Tags = course.Tags?.Select(t => t.InterestTag.Name).ToList() ?? new(),
            CreatedByName = course.CreatedBy?.FullName
        };

        public static InterestTagDto ToDto(InterestTag tag) => new()
        {
            Id = tag.Id,
            Name = tag.Name
        };

        public static EnrollmentDto ToDto(Enrollment enroll) => new()
        {
            Id = enroll.Id,
            StudentName = enroll.Student.FullName,
            CourseTitle = enroll.Course.Title,
            Grade = enroll.Grade,
            Semester = enroll.Semester
        };
    }

}
