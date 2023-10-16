namespace RPSAcademy.Models.DTOs
{
    public class SelectOpponentDisplayModel
    {
        public IEnumerable<Opponents> Opponents { get; set; }

        public UserInfo UserInfo { get; set; }
    }
}
