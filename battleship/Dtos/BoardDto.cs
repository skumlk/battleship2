using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Battleship.Dtos
{
    public class BoardDto
    {

        public int Score { get; set; }

        public ICollection<WarshipDto> WarShips { get; set; }

        public int[] Grid { get; set; }

        public List<int> successFire{ get; set; }
    }
}