using MARS.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace MARS.Interfaces
{
    interface IValidations
    {
        bool InputsAreValid(List<string> inputLines);
        public bool AreaIsValid(string inp);
        public bool RoverLocationIsValid(string inp);
        public bool RoverCommmandsIsValid(string inp);
        bool AreaCheck(Area a, Rover r);
        bool AccidentControl(Rover r, List<Rover> otherRovers);
    }
}
