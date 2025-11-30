namespace RecomendationSystemAPI.DTOs.Students
{
    public class StudentDto
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public float GPA { get; set; }
        public List<string> InterestNames { get; set; }
    }
}
