namespace RecomendationSystemAPI.DTOs.Students
{
    public class UpdateStudentDto
    {
        public int Id { get; set; }
        public string FullName { get; set; } = string.Empty;
        public double GPA { get; set; }
        public List<int> InterestIds { get; set; } = new();
    }
}
