
using System;
using System.Collections.Generic;
using Battleship.Data;
using Battleship.Models;
using System.Linq;

namespace Services.Battleship
{
    public class BattleshipService : IBattleshipService
    {
        public readonly IBattleshipRepository _repository;
        public readonly Random _random;

        public BattleshipService(IBattleshipRepository repository)
        {
            this._repository = repository;
            this._random = new Random();
        }

        public Game createGame(IList<WarShip> boardBWarShips)
        {
            var boardAWarShips = generateWarships();

            var game = new Game
            {
                BoardA = new Board { Grid = new int[0], WarShips = boardAWarShips },
                BoardB = new Board { Grid = new int[0], WarShips = boardBWarShips }
            };

            return this._repository.CreateGame(game);
        }
        public Tuple<Game, List<int>, List<int>> markCell(int gameId, int x, int y)
        {
            var game = _repository.GetGameById(gameId);
            Tuple<bool, double, List<int>> wonA;
            Tuple<bool, double, List<int>> wonB;

            if (game.GameStatus != GameStatus.NEW || game.BoardA.Grid.Contains(y * 10 + x))
            {
                wonA = isGameWon(game.BoardA.Grid, game.BoardA.WarShips);
                wonB = isGameWon(game.BoardA.Grid, game.BoardB.WarShips);
                return new Tuple<Game, List<int>, List<int>>(game, wonA.Item3, wonB.Item3);
            }

            game.BoardA.Grid = addToArray(x, y, game.BoardA.Grid);
            Tuple<int, int> nextPosition = calculateNext(game.BoardB);
            game.BoardB.Grid = addToArray(nextPosition.Item1, nextPosition.Item2, game.BoardB.Grid);

            wonA = isGameWon(game.BoardA.Grid, game.BoardA.WarShips);
            if (wonA.Item1)
                game.GameStatus = GameStatus.WINB;
            game.BoardA.Score = (int)wonA.Item2;

            wonB = isGameWon(game.BoardB.Grid, game.BoardB.WarShips);
            if (wonB.Item1)
                game.GameStatus = GameStatus.WINA;
            game.BoardB.Score = (int)wonB.Item2;
            _repository.UpdateGame(game);

            return new Tuple<Game, List<int>, List<int>>(game, wonA.Item3, wonB.Item3);
        }

        public Tuple<Game, List<int>, List<int>> getGameStatus(int gameId)
        {
            var game = _repository.GetGameById(gameId);
            Tuple<bool, double, List<int>> wonA = isGameWon(game.BoardA.Grid, game.BoardA.WarShips);
            Tuple<bool, double, List<int>> wonB = isGameWon(game.BoardB.Grid, game.BoardB.WarShips);
            return new Tuple<Game, List<int>, List<int>>(game, wonA.Item3, wonB.Item3);
        }

        private List<WarShip> generateWarships()
        {
            var hashSet = new HashSet<int>();
            while (hashSet.Count < 3)
                hashSet.Add(_random.Next(0, 10));

            var values = hashSet.ToArray();

            int startX1 = _random.Next(0, 5);
            int startX2 = _random.Next(0, 5);
            int startX3 = _random.Next(0, 5);

            List<WarShip> list = new List<WarShip>{
                new WarShip{ Type = WarshipType.BATTLESHIP, StartX = startX1, StartY = values[0], EndX =  startX1 + 5, EndY =  values[0]},
                new WarShip{ Type = WarshipType.DESTROYER, StartX = startX2, StartY = values[1], EndX = startX2 + 4, EndY =  values[1]},
                new WarShip{ Type = WarshipType.DESTROYER, StartX = startX3, StartY = values[2], EndX = startX3 + 4, EndY =  values[2]},
            };

            return list;
        }

        private bool isInBattleShip(int x, int y, IEnumerable<WarShip> warships)
        {
            if (warships != null)
                foreach (WarShip warship in warships)
                {
                    if (warship.StartX <= x && warship.EndX > x)
                        if (warship.StartY <= y && warship.EndY >= y)
                        {
                            return true;
                        }
                }

            return false;
        }

        public int[] addToArray(int x, int y, int[] array)
        {
            if (array.Contains(y * 10 + x))
                return array;

            int[] result = new int[array.Length + 1];
            Array.Copy(array, 0, result, 0, array.Length);
            result[array.Length] = y * 10 + x;
            return result;
        }

        private Tuple<bool, double, List<int>> isGameWon(int[] grid, IEnumerable<WarShip> warships)
        {
            int count = 0;
            int total = 0;
            bool isWin = true;
            List<int> successFiredList = new List<int>();

            foreach (var warship in warships)
            {
                for (int x = warship.StartX; x < warship.EndX; x++)
                {
                    for (int y = warship.StartY; y <= warship.EndY; y++)
                    {
                        total += 1;
                        if (!grid.Contains(y * 10 + x))
                        {
                            isWin = false;
                        }
                        else
                        {
                            count++;
                            successFiredList.Add(y * 10 + x);
                        }
                    }
                }
            }

            return new Tuple<bool, double, List<int>>(isWin, (count * 100) / total, successFiredList);
        }

        private Tuple<int, int> calculateNext(Board board)
        {
            var grid = board.Grid;

            for (int x = 0; x < 100000; x++)
            {
                int z = _random.Next(0, 100);
                if (!grid.Contains(z))
                    return new Tuple<int, int>(z % 10, z / 10);
            }

            return null;
        }

        private int getGridValue(string grid, int x, int y)
        {
            return grid[y * 10 + x];
        }
    }
}