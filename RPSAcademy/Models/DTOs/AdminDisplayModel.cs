namespace RPSAcademy.Models.DTOs
{
    public class AdminDisplayModel
    {
        public IEnumerable<Opponents> Opponents { get; set; }

        public IEnumerable<DefaultSubjects> DefaultSubjects { get; set; }

        public IEnumerable<DefaultQuestions> DefaultQuestions { get; set; }

        public DefaultSubjects DefaultSubject { get; set; }

    }
}
