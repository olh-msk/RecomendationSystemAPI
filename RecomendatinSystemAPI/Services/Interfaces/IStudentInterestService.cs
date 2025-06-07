namespace RecomendationSystemAPI.Services.Interfaces
{
    public interface IStudentInterestService
    {
        Task AssignInterestsAsync(int studentId, List<int> interestIds);
    }
}
