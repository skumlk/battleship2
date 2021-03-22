using Battleship.Models;

namespace Battleship.Dtos
{
    public class GameDto
    {
        public BoardDto BoardA { get; set; }

        public BoardDto BoardB { get; set; }

        public GameStatus GameStatus { get; set; }
    }
}