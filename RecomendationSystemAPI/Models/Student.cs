namespace RecomendatinSystemAPI.Models
{
    public class Student
    {
        public int Id { get; set; }
        public string FullName { get; set; } = string.Empty;
        public float GPA { get; set; }
        public ICollection<Enrollment> Enrollments { get; set; }
        public ICollection<StudentInterest> Interests { get; set; }
    }
}
