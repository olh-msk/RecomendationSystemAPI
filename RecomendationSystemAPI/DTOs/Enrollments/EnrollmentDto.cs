namespace RecomendationSystemAPI.DTOs.Enrollments
{
    public class EnrollmentDto
    {
        public int Id { get; set; }
        public string StudentName { get; set; }
        public string CourseTitle { get; set; }
        public float? Grade { get; set; }
        public string Semester { get; set; }
    }
}
