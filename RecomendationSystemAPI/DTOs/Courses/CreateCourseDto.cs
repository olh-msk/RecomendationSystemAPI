namespace RecomendationSystemAPI.DTOs.Courses
{
    public class CreateCourseDto
    {
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int CreditHours { get; set; }
        // tags by id to relate course -> interest tags
        public List<int> InterestTagIds { get; set; } = new();

        // ID of teacher (Student.Id) who creates the course
        public int? CreatedById { get; set; }
    }
}
