using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Battleship.Models
{
    public enum WarshipType
    {
        BATTLESHIP,
        DESTROYER
    }

    public class WarShip
    {

        [Key]
        public int Id { get; set; }

        [Required]
        public int StartX { get; set; }

        [Required]
        public int StartY { get; set; }

        [Required]
        public int EndX { get; set; }

        [Required]
        public int EndY { get; set; }

        public WarshipType Type { get; set; }
    }
}