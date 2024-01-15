using Microsoft.EntityFrameworkCore;
using PlayersMVCApplication.Models.Player;
using PlayersMVCApplication.Models;

namespace PlayersMVCApplication.Data
{
    public class PlayerMVCDbContext : DbContext
    {
        public PlayerMVCDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Player> Players { get; set; }

        public DbSet<Team> Team { get; set; }
    }
}
