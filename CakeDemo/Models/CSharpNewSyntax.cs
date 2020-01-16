using System;
using System.Collections.Generic;
using System.Linq;

namespace CakeDemo.Models
{
    public class CSharpNewSyntax
    {
        public int GetSum(List<int> list)
        {
            return list?.Sum() ?? -1;
        }

        /// <summary>
        /// NewFeature_On_OutVariables_WithoutDiscard
        /// </summary>
        public string ReturnThePositionStringAccordingTheGivenData(int positionX, int positionY)
        {
            var p = new Point(positionX, positionY);

            // declare variables whit out
            p.GetCoordinates(out var x, out var y);

            return $"You are at ({x}, {y})";
        }

        /// <summary>
        /// CanGivenStringParseToInteger
        /// </summary>
        public bool CanGivenStringParseToInteger(string numStr)
        {
            // numStr can parse to int
            return int.TryParse(numStr, out _);
        }

        public string ReturnTheYPositionForGivenData(int positionX, int positionY)
        {
            var p = new Point(positionX, positionY);

            // I only care about Y position
            var (_, y) = p.GetCoordinatesTuple;

            return $"The Coordinate Y is {y}";
        }

        public string ReturnThenPositionAfterMoved(int x, int y, int steps)
        {
            var p = new Point(x, y);

            // I don't care about return value
            _ = steps > 10 ? p.MoveUp(steps) : p.MoveRight(steps);

            return $"You are at ({p.X}, {p.Y})";
        }

        /// <summary>
        /// return a length of obj '*'
        /// </summary>
        public string GetALengthOfStarsForGivenObject(dynamic obj)
        {
            // constant pattern "null"
            if (obj is null)
            {
                return string.Empty;
            }

            // type pattern "int i"
            if (!(obj is int i))
            {
                return string.Empty;
            }

            return new string('*', i);
        }

        public string GetDescriptionForGivenShape(dynamic shape)
        {
            if (shape is Circle c)
            {
                return $"circle with radius {c.Radius}";
            }

            if (shape is Rectangle r)
            {
                return $"{r.Length} x {r.Height} rectangle";
            }

            return "<unknown shape>";
        }

        public string ReturnThePropertyOfGiveObject(dynamic shape)
        {
            string output;

            switch (shape)
            {
                case Circle c:
                    output = $"circle with radius {c.Radius}";
                    break;
                case Rectangle s when s.Length == s.Height:
                    output = $"{s.Length} x {s.Height} square";
                    break;
                case Rectangle r:
                    output = $"{r.Length} x {r.Height} rectangle";
                    break;
                default:
                    output = "<unknown shape>";
                    break;
                case null:
                    throw new ArgumentNullException(nameof(shape));
            }

            return output;
        }

        public string ShouldReturnEachSideLengthOfATriangle(int a, int b, int c)
        {
            var (lengthA, lengthB, lengthC) = new Triangle(a, b, c).GetSides();

            return $"triangle with sides: {lengthA}, {lengthB}, {lengthC}";
        }

        public int Fibonacci(int x)
        {
            if (x < 0)
            {
                throw new ArgumentException("Less negativity please!", nameof(x));
            }

            return Fib(x).current;

            (int current, int previous) Fib(int i)
            {
                if (i == 0)
                {
                    return (1, 0);
                }

                var (p, pp) = Fib(i - 1);

                return (p + pp, p);
            }
        }

        public ref int Find(int number, int[] numbers)
        {
            for (int i = 0; i < numbers.Length; i++)
            {
                if (numbers[i] == number)
                {
                    // return the storage location, not the value
                    return ref numbers[i];
                }
            }

            throw new IndexOutOfRangeException($"{nameof(number)} not found");
        }
    }
}
