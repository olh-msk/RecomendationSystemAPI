namespace RecomendationSystemAPI.DTOs.Students
{
    public class CreateStudentDto
    {
        public string FullName { get; set; }
        public float GPA { get; set; }
        public List<int> InterestTagIds { get; set; }
    }
}
