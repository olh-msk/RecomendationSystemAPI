namespace RecomendatinSystemAPI.Models
{
    public class StudentInterest
    {
        public int Id { get; set; }

        public int StudentId { get; set; }
        public Student Student { get; set; }

        public int InterestTagId { get; set; }
        public InterestTag InterestTag { get; set; }
    }
}
