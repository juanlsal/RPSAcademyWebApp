using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RPSAcademy.Data;
using RPSAcademy.Models;

namespace RPSAcademy.Repository
{
    public class SubjectRepository : ISubjectRepository
    {
        private readonly ApplicationDbContext _context;

        public SubjectRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<DefaultSubjects>> GetDefaultSubjects()
        {
            IEnumerable<DefaultSubjects> defaultSubjects =
                await (from defaultSubject in _context.DefaultSubjects
                       select new DefaultSubjects
                       {
                           id = defaultSubject.id,
                           SubjectName = defaultSubject.SubjectName,
                           Description = defaultSubject.Description
                       }).ToListAsync();

            return defaultSubjects;
        }

        public DefaultSubjects GetRelatedDefaultSubject(int subjectId)
        {
            var relatedDefaultSubject = _context.DefaultSubjects.FirstOrDefault(u => u.id == subjectId);

            return relatedDefaultSubject;
        }

        public async Task<IEnumerable<UserCreatedSubjects>> GetUsersCreatedSubjects(string userId)
        {
            var usersCreatedSubjects = await _context.UserCreatedSubjects.Where(u => u.UserId == userId).ToListAsync();

            return usersCreatedSubjects;
        }

        public UserCreatedSubjects GetRelatedUserCreatedSubject(int subjectId)
        {
            var relatedUserCreatedSubject = _context.UserCreatedSubjects.FirstOrDefault(r => r.Id == subjectId);

            return relatedUserCreatedSubject;
        }

        
        
    }
}
