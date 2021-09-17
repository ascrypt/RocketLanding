using System.Collections.Generic;

namespace RocketLanding
{
    public class Position
    {
        public Position(int[] xy)
        {
            Xy = xy;
        }

        public Position(int[] xy, int xSize, int ySize)
        {
            Left = xy[0];
            Right = xy[0] + xSize;
            Top = xy[1];
            Bottom = xy[1] + ySize;
        }

        public int[] Xy { get; }
        public int Left { get; }
        public int Right { get; }
        public int Top { get; }
        public int Bottom { get; }

        public override bool Equals(object obj)
        {
            if (obj is not Position position)
                return false;

            return Xy[0] == position.Xy[0] && Xy[1] == position.Xy[1];
        }

        public override int GetHashCode()
        {
            return $"{Xy[0]},{Xy[1]}".GetHashCode();
        }

        public IEnumerable<Position> GetNearPositions()
        {
            return new List<Position>
            {
                new(new[] { Xy[0] - 1, Xy[1] - 1 }),
                new(new[] { Xy[0] + 1, Xy[1] - 1 }),
                new(new[] { Xy[0] - 1, Xy[1] }),
                new(new[] { Xy[0] + 1, Xy[1] }),
                new(new[] { Xy[0] - 1, Xy[1] + 1 }),
                new(new[] { Xy[0] + 1, Xy[1] + 1 }),
                new(new[] { Xy[0], Xy[1] + 1 }),
                new(new[] { Xy[0], Xy[1] - 1 })
            };
        }
    }
}