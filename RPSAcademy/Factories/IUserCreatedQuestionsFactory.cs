using RPSAcademy.Models;

namespace RPSAcademy.Factories
{
    public interface IUserCreatedQuestionsFactory
    {
        UserCreatedQuestions CreateUserCreatedQuestion(string question, string answerA, string answerB, string answerC, string answerD, int correctAnswer, int userCreatedSubjectId);
    }
}
