using RPSAcademy.Models;

namespace RPSAcademy.Factories
{
    public class UserCreatedQuestionsFactory : IUserCreatedQuestionsFactory
    {
        public UserCreatedQuestions CreateUserCreatedQuestion(string question, string answerA, string answerB, string answerC, string answerD, int correctAnswer, int userCreatedSubjectId)
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

            return new UserCreatedQuestions
            {
                Question = question,
                AnswerA = answerA,
                AnswerB = answerB,
                AnswerC = answerC,
                AnswerD = answerD,
                CorrectAnswer = correctAnswer,
                CorrectAnswerText = correctAnswerText,
                UserCreatedSubjectId = userCreatedSubjectId
            };
        }
    }
}
