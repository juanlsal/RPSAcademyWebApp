using Microsoft.EntityFrameworkCore;
using RPSAcademy.Data;
using RPSAcademy.Models;

namespace RPSAcademy.Repository
{
    public class PlayRepository : IPlayRepository
    {
        private readonly ApplicationDbContext _context;

        public PlayRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        //This method should return a list of opponents
        public async Task<IEnumerable<Opponents>> GetOpponents()
        {
            IEnumerable<Opponents> opponents = 
                await (from opponent in _context.Opponents
                       select new Opponents
                       {
                           id = opponent.id,
                           Name = opponent.Name,
                           Description = opponent.Description,
                           ImageUrl = opponent.ImageUrl,
                           Info = opponent.Info
                       }).ToListAsync();

            return opponents;
        }
    }
}
