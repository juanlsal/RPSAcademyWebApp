using Microsoft.EntityFrameworkCore;
using RPSAcademy.Data;
using RPSAcademy.Models;
using System.Collections.Generic;

namespace RPSAcademy.Repository
{
    public class QuestionRepository : IQuestionRepository
    {

        private readonly ApplicationDbContext _context;

        public QuestionRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<DefaultQuestions>> GetDefaultQuestions(int subjectId)
        {
            IEnumerable<DefaultQuestions> defaultQuestions =
                await (from defaultQuestion in _context.DefaultQuestions
                       where defaultQuestion.DefaultSubjectId == subjectId
                       select new DefaultQuestions
                       {
                           Id = defaultQuestion.Id,
                           Question = defaultQuestion.Question,
                           AnswerA = defaultQuestion.AnswerA,
                           AnswerB = defaultQuestion.AnswerB,
                           AnswerC = defaultQuestion.AnswerC,
                           AnswerD = defaultQuestion.AnswerD,
                           CorrectAnswerText = defaultQuestion.CorrectAnswerText
                       }).ToListAsync();

            return defaultQuestions;

        }

        public async Task<IEnumerable<UserCreatedQuestions>> GetUserCreatedQuestions(int subjectId)
        {
            IEnumerable<UserCreatedQuestions> userCreatedQuestions =
                await (from userCreatedQuestion in _context.UserCreatedQuestions
                       where userCreatedQuestion.UserCreatedSubjectId == subjectId
                       select new UserCreatedQuestions
                       {
                           Id = userCreatedQuestion.Id,
                           Question = userCreatedQuestion.Question,
                           AnswerA = userCreatedQuestion.AnswerA,
                           AnswerB = userCreatedQuestion.AnswerB,
                           AnswerC = userCreatedQuestion.AnswerC,
                           AnswerD = userCreatedQuestion.AnswerD,
                           CorrectAnswerText = userCreatedQuestion.CorrectAnswerText
                       }).ToListAsync();

            return userCreatedQuestions;
        }
    }
}
