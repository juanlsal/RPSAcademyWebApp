using RPSAcademy.Models;

namespace RPSAcademy.Repository
{
    public interface IPlayRepository
    {
        Task<IEnumerable<Opponents>> GetOpponents();
    }
}
