using System;
using System.Collections.Generic;
using System.Text;

namespace MARS.Entities
{
    class Rover
    {
        public int Id { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public char Direction { get; set; }
        public string Moves { get; set; }
    }
}
