namespace RecomendationSystemAPI.Models
{
    public class StudentInterest
    {
        public int Id { get; set; }
        public int StudentId { get; set; } = 0;
        public Student Student { get; set; } = null!;
        public int InterestTagId { get; set; }
        public InterestTag InterestTag { get; set; } = null!;
    }
}
