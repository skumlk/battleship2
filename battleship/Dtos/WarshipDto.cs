using System.Collections.Generic;

namespace Battleship.Dtos
{
    public class WarshipDto
    {
        public string Type { get; set; }

        public int StartX { get; set; }
        public int StartY { get; set; }

        public int EndX { get; set; }
        public int EndY { get; set; }
    }

    public class WarshipsDto
    {
        public List<WarshipDto> Ships { get; set; }
    }
}