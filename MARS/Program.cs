using MARS.Entities;
using MARS.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace MARS
{
    class Program
    {
        static Area area=new Area();
        static List<Rover> rovers = new List<Rover>();
        static void Main(string[] args)
        {
            var roverMoveService = new RoverMoves();
            var definitionService = new Definitions();
            var validationService = new Validations();
            Run(roverMoveService, definitionService, validationService);

            Console.WriteLine("Would you like to run the program again? Y/N");
            var line = Console.ReadLine();
            while (line == "Y")
            {
                Run(roverMoveService, definitionService, validationService);
                Console.WriteLine("Would you like to run the program again? Y/N");
                line = Console.ReadLine();
            }
            Environment.Exit(0);
        }
        static void Run(RoverMoves roverMoves, Definitions definitions, Validations validations)
        {
            Console.WriteLine("Please enter data! Leave a blank line to end the entry!");
            List<string> inputLines = GetAndValidateInputs(validations);
            if (!validations.InputsAreValid(inputLines))
            {
                Console.WriteLine("You entered missing data! If you want, you can start the program again.");
                return;
            }
            area = DefineArea(inputLines, definitions);
            rovers = DefineRovers(inputLines, definitions);
            rovers = RunRovers(roverMoves, validations);


            Console.WriteLine("-----Last Locations----");
            for (int i = 0; i < rovers.Count(); i++)
            {
                Console.WriteLine("Rover " + (i + 1) + " : " + rovers[i].X + " " + rovers[i].Y + " " + rovers[i].Direction);
            }

        }

        static List<string> GetAndValidateInputs(Validations validations)
        {

            List<string> inputLines = new List<string>();
            var line = Console.ReadLine();
            var lineCounter = 0;
            while (line != string.Empty)
            {
                if (lineCounter == 0)
                {
                    if (!validations.AreaIsValid(line))
                    {
                        Console.WriteLine("Invalid Character! Please rewrite the line.");
                    }
                    else
                    {
                        inputLines.Add(line);
                        lineCounter++;
                    }
                }
                else if (lineCounter % 2 == 1)
                {
                    if (!validations.RoverLocationIsValid(line))
                    {
                        Console.WriteLine("Invalid Character! Please rewrite the line.");
                    }
                    else
                    {
                        inputLines.Add(line);
                        lineCounter++;
                    }
                }
                else
                {
                    if (!validations.RoverCommmandsIsValid(line))
                    {
                        Console.WriteLine("Invalid Character! Please rewrite the line.");
                    }
                    else
                    {
                        inputLines.Add(line);
                        lineCounter++;
                    }
                }
                line = Console.ReadLine();
            }

            return inputLines;
        }

        static List<Rover> DefineRovers(List<string> inputs, Definitions definitions)
        {
            var rovers = new List<Rover>();
            var roverCount = 1;
            for (int i = 1; i < inputs.Count(); i += 2)
            {
                var rover = definitions.Rover(inputs.GetRange(i, 2),roverCount);
                rovers.Add(rover);
                roverCount++;
            }
            return rovers;
        }
        static Area DefineArea(List<string> inputs, Definitions definitions)
        {
            return definitions.Area(inputs.First());
        }


        static List<Rover> RunRovers(RoverMoves roverMoves, Validations validations)
        {
            for (int i = 0; i < rovers.Count(); i++)
            {
                var otherRovers = rovers.Where(x => x.Id != rovers[i].Id).ToList();
                rovers[i] = RunCommands(roverMoves,validations, rovers[i], otherRovers);
            }
            return rovers;
        }


        static Rover RunCommands(RoverMoves roverMoves, Validations validations, Rover r, List<Rover> otherRovers)
        {

            foreach (var t in r.Moves)
            {
                switch (t)
                {
                    case 'R':
                        r.Direction = roverMoves.TurnRight(r.Direction);
                        break;
                    case 'L':
                        r.Direction = roverMoves.TurnLeft(r.Direction);
                        break;
                    case 'M':
                        r = roverMoves.MoveForward(r);
                        if (!validations.AccidentControl(r, otherRovers))
                            Console.WriteLine("Rover " + r.Id + " : Crash detected! Flight mode is active.");
                        if(!validations.AreaCheck(area,r))
                            Console.WriteLine("Rover " + r.Id + " : Out of the area. Signal is increased.");
                        break;
                    default:
                        Console.WriteLine("Invalid Command");
                        break;
                }
            }
            return r;
        }
    }
}
