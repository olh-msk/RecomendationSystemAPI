namespace RecomendationSystemAPI.DTOs.Auth;


public class RegisterDto
{
    public string FullName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public float GPA { get; set; }
    public List<int> InterestTagIds { get; set; } = new();
}