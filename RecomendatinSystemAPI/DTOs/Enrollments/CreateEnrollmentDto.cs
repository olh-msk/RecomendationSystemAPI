namespace RecomendationSystemAPI.DTOs.Enrollments
{
    public class CreateEnrollmentDto
    {
        public int StudentId { get; set; }
        public int CourseId { get; set; }
        public float? Grade { get; set; }
        public string Semester { get; set; }
    }
}
