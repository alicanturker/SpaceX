using MARS.Entities;
using MARS.Interfaces;
using System;
using System.Collections.Generic;

namespace MARS.Services
{
    class RoverMoves : IRoverMoves
    {
        private static readonly char[] directions = new char[] { 'N', 'E', 'S', 'W' };
        private static readonly LinkedList<char> listOfDirections = new LinkedList<char>(directions);
        public Rover MoveForward(Rover r)
        {
            switch (r.Direction)
            {
                case 'N':
                    r.Y++;
                    break;
                case 'E':
                    r.X++;
                    break;
                case 'S':
                    r.Y--;
                    break;
                case 'W':
                    r.X--;
                    break;
                default:
                    Console.WriteLine("Invalid Command");
                    break;
            }
            return r;
        }

        public char TurnLeft(char curPos)
        {
            
            var curPosNode = listOfDirections.Find(curPos);
            curPosNode = curPosNode.Previous ?? listOfDirections.Last;
            char lastPos = curPosNode.Value;
            return lastPos;
        }

        public char TurnRight(char curPos)
        {
            var curPosNode = listOfDirections.Find(curPos);
            curPosNode = curPosNode.Next ?? listOfDirections.First;
            char lastPos = curPosNode.Value;
            return lastPos;
        }
    }
}
