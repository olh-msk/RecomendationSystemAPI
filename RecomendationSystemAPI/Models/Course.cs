namespace RecomendationSystemAPI.Models
{
    public class Course
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int CreditHours { get; set; }

        // Navigation
        public ICollection<Enrollment> Enrollments { get; set; } = new List<Enrollment>();
        public ICollection<CourseTag> Tags { get; set; } = new List<CourseTag>();

        // Author / teacher who created the course (optional)
        public int? CreatedById { get; set; }
        public Student? CreatedBy { get; set; }
    }
}
