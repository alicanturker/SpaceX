using MARS.Entities;
using MARS.Interfaces;
using System.Collections.Generic;

namespace MARS.Services
{
    class Definitions : IDefinitions
    {
        public Area Area(string input)
        {
            var result = new Area();
            var areaInputs = input.Split(' ');
            int.TryParse(areaInputs[0], out int x);
            int.TryParse(areaInputs[1], out int y);
            result.X = x;
            result.Y = y;
            return result;
        }

        public Rover Rover(List<string> inputs, int id)
        {
            var roverCords = inputs[0].Split(' ');
            int.TryParse(roverCords[0], out int x);
            int.TryParse(roverCords[1], out int y);
            char.TryParse(roverCords[2], out char d);
            return new Rover { X = x, Y = y, Direction = d, Moves = inputs[1], Id = id };
        }
    }
}
