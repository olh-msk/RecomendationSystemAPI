namespace RecomendationSystemAPI.DTOs.Auth
{
    public class RegisterDto
    {
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public float GPA { get; set; }
        public List<int> InterestTagIds { get; set; } = new();
        public string Role { get; set; } = "Student";
    }
}