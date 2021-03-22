
using System;
using System.Collections.Generic;
using Battleship.Models;

namespace Services.Battleship
{
    public interface IBattleshipService
    {
        public Game createGame(IList<WarShip> boardAWarShips);

        public Tuple<Game, List<int>, List<int>> markCell(int gameId, int x, int y);

        public  Tuple<Game, List<int>, List<int>> getGameStatus(int gameId);
    }
}