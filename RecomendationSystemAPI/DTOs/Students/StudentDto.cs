namespace RecomendationSystemAPI.DTOs.Students
{
    public class StudentDto
    {
        public int Id { get; set; }
        public string FullName { get; set; } = string.Empty;
        public float GPA { get; set; }
        public List<string> InterestNames { get; set; } = new();
        public string Role { get; set; } = string.Empty;
    }
}
