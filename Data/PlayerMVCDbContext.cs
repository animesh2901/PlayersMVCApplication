using Microsoft.EntityFrameworkCore;
using PlayersMVCApplication.Models.Player;

namespace PlayersMVCApplication.Data
{
    public class PlayerMVCDbContext : DbContext
    {
        public PlayerMVCDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Player> Players { get; set; }
    }
}
