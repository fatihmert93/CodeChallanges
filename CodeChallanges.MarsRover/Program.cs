using System;

namespace CodeChallanges.MarsRover
{
    class Program
    {
        static void Main(string[] args)
        {
            MarsRover();
        }

        private static void MarsRover()
        {
            var inputDimensions = Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
            // TODO add boundary checking
            while (true)
            {
                var position = ParseFirstInputLine(Console.ReadLine());
                if (position == null)
                    break;
                var commands = Console.ReadLine();
                foreach (var command in commands)
                {
                    ApplyCommand(position, command);
                }
                WritePosition(position);
            }
        }

        private static void WritePosition(Position position)
        {
            Console.WriteLine(position.X + " " + position.Y + " " + GetDirectionString(position.Direction));
        }

        private static void ApplyCommand(Position position, char command)
        {
            var success = TryParseDirection(command.ToString(), out var direction);
            if (!success)
                return;
            switch (command)
            {
                case 'M':
                    Move(position, direction);
                    break;
                case 'L':
                    TurnLeft(position);
                    break;
                case 'R':
                    TurnRight(position);
                    break;
            }
        }

        private static void TurnLeft(Position position)
        {
            switch (position.Direction)
            {
                case Direction.North:
                    position.Direction = Direction.West;
                    break;
                case Direction.West:
                    position.Direction = Direction.South;
                    break;
                case Direction.East:
                    position.Direction = Direction.North;
                    break;
                case Direction.South:
                    position.Direction = Direction.East;
                    break;
            }
        }

        private static void TurnRight(Position position)
        {
            switch (position.Direction)
            {
                case Direction.North:
                    position.Direction = Direction.East;
                    break;
                case Direction.West:
                    position.Direction = Direction.North;
                    break;
                case Direction.East:
                    position.Direction = Direction.South;
                    break;
                case Direction.South:
                    position.Direction = Direction.West;
                    break;
            }
        }

        private static void Move(Position position, Direction direction)
        {
            switch (direction)
            {
                case Direction.North:
                    position.Y++;
                    break;
                case Direction.South:
                    position.Y--;
                    break;
                case Direction.East:
                    position.X++;
                    break;
                case Direction.West:
                    position.X--;
                    break;
            }
        }

        private static Position ParseFirstInputLine(string firstInputLine)
        {
            if (string.IsNullOrWhiteSpace(firstInputLine))
                return null;
            var lineParts = firstInputLine.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            if (lineParts.Length < 3)
                return null;
            var successX = int.TryParse(lineParts[0], out var x);
            var successY = int.TryParse(lineParts[1], out var y);
            var successDirection = TryParseDirection(lineParts[2], out var direction);
            if(!(successX && successY && successDirection))
                return null;
            return new Position
            {
                Direction = direction,
                X = x,
                Y = y
            };
        }

        private static string GetDirectionString(Direction direction)
        {
            switch (direction)
            {
                case Direction.North:
                    return "N";
                case Direction.South:
                    return "S";
                case Direction.East:
                    return "E";
                case Direction.West:
                    return "W";
            }
            return null;
        }

        private static bool TryParseDirection(string directionString, out Direction direction)
        {
            direction = Direction.North;
            var success = false;
            switch (directionString)
            {
                    case "N":
                    case "n":
                        direction = Direction.North;
                        success = true;
                        break;
                    case "E":
                    case "e":
                        direction = Direction.East;
                        success = true;
                        break;
                    case "W":
                    case "w":
                        direction = Direction.West;
                        success = true;
                        break;
                    case "S":
                    case "s":
                        direction = Direction.South;
                        success = true;
                        break;
            }
            return success;
        }
    

        private class Position
        {
            public Direction Direction { get; set; }
            public int X { get; set; }
            public int Y { get; set; }
        }

        private enum Direction
        {
            North,
            West,
            East,
            South
        }
    }
    
}