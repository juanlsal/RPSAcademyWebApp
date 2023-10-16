using RPSAcademy.Models;

namespace RPSAcademy.Factories
{
    public class DefaultQuestionFactory : IDefaultQuestionFactory
    {
        public DefaultQuestions CreateDefaultQuestion(string question, string answerA, string answerB, string answerC, string answerD, int correctAnswer, int defaultSubjectId)
        {
            var correctAnswerText = string.Empty;

            switch (correctAnswer)
            {
                case 1:
                    correctAnswerText = answerA;
                    break;
                case 2:
                    correctAnswerText = answerB;
                    break;
                case 3:
                    correctAnswerText = answerC;
                    break;
                case 4:
                    correctAnswerText = answerD;
                    break;
            }

            return new DefaultQuestions
            {
                Question = question,
                AnswerA = answerA,
                AnswerB = answerB,
                AnswerC = answerC,
                AnswerD = answerD,
                CorrectAnswer = correctAnswer,
                CorrectAnswerText = correctAnswerText,
                DefaultSubjectId = defaultSubjectId
            };
        }
    }
}