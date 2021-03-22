using System.Collections.Generic;
using Battleship.Models;

namespace Battleship.Data
{
    public interface IBattleshipRepository
    {
        Game CreateGame(Game game);

        void UpdateGame(Game game);

        Game GetGameById(int id);
    }
}