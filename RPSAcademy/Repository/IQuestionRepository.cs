using RPSAcademy.Models;

namespace RPSAcademy.Repository
{
    public interface IQuestionRepository
    {
        Task<IEnumerable<DefaultQuestions>> GetDefaultQuestions(int subjectId);

        Task<IEnumerable<UserCreatedQuestions>> GetUserCreatedQuestions(int subjectId);
    }
}
