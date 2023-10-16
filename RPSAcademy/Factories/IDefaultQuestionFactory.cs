using RPSAcademy.Models;

namespace RPSAcademy.Factories
{
    public interface IDefaultQuestionFactory
    {
        DefaultQuestions CreateDefaultQuestion(string question, string answerA, string answerB, string answerC, string answerD, int correctAnswer, int defaultSubjectId);

    }
}
