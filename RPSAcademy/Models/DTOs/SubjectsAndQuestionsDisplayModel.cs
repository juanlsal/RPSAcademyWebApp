namespace RPSAcademy.Models.DTOs
{
    public class SubjectsAndQuestionsDisplayModel
    {
        public IEnumerable<DefaultSubjects> DefaultSubjects { get; set; }

        public IEnumerable<UserCreatedSubjects> UserCreatedSubjects { get; set; }

        public IEnumerable<DefaultQuestions> DefaultQuestions { get; set; }

        public IEnumerable<UserCreatedQuestions> UserCreatedQuestions { get; set; }

        public UserCreatedSubjects UserCreatedSubject { get; set; }
        public DefaultSubjects DefaultSubject { get; set; }
        public UserInfo UserInfo { get; set; }
        public DefaultQuestions DefaultQuestion { get; set; }

    }
}