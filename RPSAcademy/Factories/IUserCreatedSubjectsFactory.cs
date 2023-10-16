using RPSAcademy.Models;

namespace RPSAcademy.Factories
{
    public interface IUserCreatedSubjectsFactory
    {
        UserCreatedSubjects CreateUserCreatedSubject(string userId, string subjectName, string Description);

    }
}
