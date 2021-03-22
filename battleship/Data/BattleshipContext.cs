
using System.IO;
using System.Threading.Tasks;
using Battleship.Models;
using Microsoft.EntityFrameworkCore;

namespace Battleship.Data
{
    public class BattleshipContext : DbContext
    {
        public BattleshipContext(DbContextOptions<BattleshipContext> opt) : base(opt) { }

        public DbSet<Game> Games { get; set; }
        public DbSet<Board> Boards { get; set; }

        public DbSet<WarShip> Battleships { get; set; }

        private readonly StreamWriter _logStream = new StreamWriter("mylog.txt", append: true);

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.LogTo(_logStream.WriteLine);

        public override void Dispose()
        {
            base.Dispose();
            _logStream.Dispose();
        }

        public override async ValueTask DisposeAsync()
        {
            await base.DisposeAsync();
            await _logStream.DisposeAsync();
        }
    }
}