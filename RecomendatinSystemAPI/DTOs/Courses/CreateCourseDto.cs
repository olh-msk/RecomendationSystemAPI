namespace RecomendationSystemAPI.DTOs.Courses
{
    public class CreateCourseDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public int CreditHours { get; set; }
        public List<int> InterestTagIds { get; set; }
    }
}
