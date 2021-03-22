
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Battleship.Models;

namespace Battleship.Dtos
{
    public class WarshipMinDto
    {
        [Required]
        public WarshipType Type { get; set; }

        [Required]
        [Range(0, 10)]
        public int X { get; set; }

        [Required]
        [Range(0, 10)]
        public int Y { get; set; }
    }

    public class WarshipsMinDto
    {
        public List<WarshipMinDto> Ships { get; set; }

    }
}