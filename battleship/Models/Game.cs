using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Battleship.Models
{
    public enum GameStatus
    {
        NEW,
        WINA,
        WINB,
        DRAW
    }

    public class Game
    {

        [Key]
        public int Id { get; set; }

        [Required]
        public Board BoardA { get; set; }

        [Required]
        public Board BoardB { get; set; }

        [Required]
        [DefaultValue(GameStatus.NEW)]

        public GameStatus GameStatus { get; set; }
    }
}