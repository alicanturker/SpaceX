using MARS.Entities;
using MARS.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MARS.Services
{
    class Validations : IValidations
    {
        static readonly char[] areaValidChars = new char[] { '1', '2', '3', '4', '5', '6', '7', '8', '9', '0', ' ' };
        static readonly char[] roverLocationValidChars = new char[] { 'N', 'S', 'E', 'W', '1', '2', '3', '4', '5', '6', '7', '8', '9', '0', ' ' };
        static readonly char[] directionsValidChars = new char[] { 'N', 'E', 'S', 'W' };
        static readonly char[] roverCommmandsValidChars = new char[] { 'L', 'R', 'M' };

        public bool InputsAreValid(List<string> inputLines)
        {
            var result = true;
            if (inputLines.Count() % 2 != 1 && inputLines.Count() < 3)
                result = false;
            return result;
        }
        public bool AreaIsValid(string inp)
        {
            if (!inp.Any(i => areaValidChars.Any(a => a == i)))
                return false;
            if (inp.Split(' ').Count() != 2)
                return false;
            return true;
        }

        public bool RoverCommmandsIsValid(string inp)
        {
            var result = inp.Any(i => roverCommmandsValidChars.Any(r => r == i));
            return result;
        }

        public bool RoverLocationIsValid(string inp)
        {
            if (!inp.Any(i => roverLocationValidChars.Any(r => r == i)))
                return false;

            var inps = inp.Split(' ');
            if (inps.Count() != 3)
                return false;

            int x = -1; int y = -1;
            int.TryParse(inps[0], out x);
            int.TryParse(inps[1], out y);
            if (x == -1 || y == -1)
                return false;

            if (!inps[2].Any(i => directionsValidChars.Any(d => d == i)))
                return false;

            if (inps[2].Count() != 1)
                return false;

            return true;
        }

        public bool AreaCheck(Area a, Rover r)
        {
            if(r.X<0 || r.Y < 0)
                return false;

            if (r.X > a.X || r.Y > a.Y)
                return false;

            return true;
        }

        public bool AccidentControl(Rover r, List<Rover> otherRovers)
        {
            return !otherRovers.Any(o => o.X == r.X && o.Y == r.Y);
        }
    }
}
