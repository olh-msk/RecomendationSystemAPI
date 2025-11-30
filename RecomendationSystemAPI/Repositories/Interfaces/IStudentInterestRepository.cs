namespace RecomendationSystemAPI.Repositories.Interfaces
{
    public interface IStudentInterestRepository
    {
        Task AssignAsync(int studentId, List<int> interestIds);
    }
}
