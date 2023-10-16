using RPSAcademy.Models;

namespace RPSAcademy.Factories
{
    public class UserCreatedSubjectsFactory : IUserCreatedSubjectsFactory
    {
        public UserCreatedSubjects CreateUserCreatedSubject(string userId, string subjectName, string Description)
        {
            return new UserCreatedSubjects
            {
                UserId = userId,
                SubjectName = subjectName,
                Description = Description
            };
        }
    }
}
