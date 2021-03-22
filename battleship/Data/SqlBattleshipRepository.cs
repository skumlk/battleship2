using System.Linq;
using Battleship.Models;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Battleship.Data
{
    public class SqlBattleshipRepository : IBattleshipRepository
    {
        private readonly BattleshipContext _context;

        public SqlBattleshipRepository(BattleshipContext context)
        {
            _context = context;
        }

        public Game GetGameById(int id)
        {
            return _context.Games.Include(m => m.BoardA).ThenInclude(n => n.WarShips).Include(m => m.BoardB).ThenInclude(n => n.WarShips).FirstOrDefault(m => m.Id == id);
        }

        Game IBattleshipRepository.CreateGame(Game game)
        {
            _context.Games.Add(game);
            _context.SaveChanges();
            return game;
        }

        void IBattleshipRepository.UpdateGame(Game game)
        {
            _context.Games.Update(game);
            _context.SaveChanges();
        }
    }
}