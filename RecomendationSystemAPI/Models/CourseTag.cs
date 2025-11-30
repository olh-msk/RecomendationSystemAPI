namespace RecomendationSystemAPI.Models
{
    public class CourseTag
    {
        public int Id { get; set; }

        public int CourseId { get; set; }
        public Course Course { get; set; }

        public int InterestTagId { get; set; }
        public InterestTag InterestTag { get; set; }
    }
}
