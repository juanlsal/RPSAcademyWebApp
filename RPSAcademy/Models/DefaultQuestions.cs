using System.ComponentModel.DataAnnotations.Schema;

namespace RPSAcademy.Models
{
    [Table("DefaultQuestions")]
    public class DefaultQuestions
    {
        public int Id { get; set; }
        public string Question { get; set; }
        public string AnswerA { get; set; }
        public string AnswerB { get; set; }
        public string AnswerC { get; set; }
        public string AnswerD { get; set; }
        public int CorrectAnswer { get; set; }
        public string CorrectAnswerText { get; set; }
        public int DefaultSubjectId { get; set; }
    }
}