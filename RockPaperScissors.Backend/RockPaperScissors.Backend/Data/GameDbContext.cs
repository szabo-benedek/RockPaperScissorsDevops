using Microsoft.EntityFrameworkCore;
using RockPaperScissors.Backend.Models;

namespace RockPaperScissors.Backend.Data
{
    public class GameDbContext : DbContext
    {
        public DbSet<Game> Games { get; set; }

        public GameDbContext(DbContextOptions<GameDbContext> options) : base(options)
        {
        }

    }
}
