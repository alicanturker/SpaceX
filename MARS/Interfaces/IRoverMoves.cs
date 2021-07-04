using MARS.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace MARS.Interfaces
{
    interface IRoverMoves
    {
        public char TurnLeft(char curPos);
        public char TurnRight(char curPos);
        public Rover MoveForward(Rover r);
    }
}
