namespace RecomendationSystemAPI.Models
{
    public class Enrollment
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public Student Student { get; set; }

        public int CourseId { get; set; }
        public Course Course { get; set; }

        public float? Grade { get; set; } // можна null
        public string Semester { get; set; }
    }
}
