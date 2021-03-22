using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Battleship.Models
{
    public class Board
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string GridString { get; set; }

        [Required]
        public int Score { get; set; }

        public ICollection<WarShip> WarShips { get; set; }

        [NotMapped]
        public int[] Grid
        {
            get
            {
                var result = GridString.Split(';');
                if (GridString.Length == 0 || result.Length == 0) return new int[0];

                return Array.ConvertAll(result, Int32.Parse);
            }
            set
            {
                GridString = String.Join(";", value);
            }
        }
    }
}