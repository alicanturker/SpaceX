using MARS.Entities;
using System.Collections.Generic;

namespace MARS.Interfaces
{
    interface IDefinitions
    {
        public Area Area(string input);
        public Rover Rover(List<string> inputs, int id);
    }
}
