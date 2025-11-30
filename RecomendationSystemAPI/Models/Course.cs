namespace RecomendationSystemAPI.Models
{
    public class Course
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; }
        public int CreditHours { get; set; }
        public ICollection<Enrollment> Enrollments { get; set; }
        public ICollection<CourseTag> Tags { get; set; }
    }
}
