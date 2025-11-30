namespace RecomendationSystemAPI.Models
{
    public class Student
    {
        public int Id { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public string PasswordSalt { get; set; } = string.Empty;
        public float GPA { get; set; }

        public ICollection<StudentInterest> Interests { get; set; } = new List<StudentInterest>();
        public ICollection<Enrollment> Enrollments { get; set; } = new List<Enrollment>();
    }
}
