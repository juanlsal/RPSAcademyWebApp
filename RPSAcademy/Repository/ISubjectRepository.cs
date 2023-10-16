using Microsoft.EntityFrameworkCore;
using RPSAcademy.Models;

namespace RPSAcademy.Repository
{
    public interface ISubjectRepository
    {
        Task<IEnumerable<DefaultSubjects>> GetDefaultSubjects();

        DefaultSubjects GetRelatedDefaultSubject(int subjectId);

        Task<IEnumerable<UserCreatedSubjects>> GetUsersCreatedSubjects(string userId);

        UserCreatedSubjects GetRelatedUserCreatedSubject(int subjectId);

    }
}
