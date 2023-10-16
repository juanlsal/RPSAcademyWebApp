using RPSAcademy.Models;

namespace RPSAcademy.Factories
{
    public class DefaultSubjectsFactory : IDefaultSubjectsFactory
    {
        public DefaultSubjects CreateDefaultSubject(string userId, string subjectName, string description)
        {
            return new DefaultSubjects
            {
                UserId = userId,
                SubjectName = subjectName,
                Description = description
            };
        }
    }
}
