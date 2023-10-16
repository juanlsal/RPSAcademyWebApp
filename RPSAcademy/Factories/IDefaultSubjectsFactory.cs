using RPSAcademy.Models;

namespace RPSAcademy.Factories
{
    public interface IDefaultSubjectsFactory
    {
        DefaultSubjects CreateDefaultSubject(string userId, string subjectName, string description);
    }
}
