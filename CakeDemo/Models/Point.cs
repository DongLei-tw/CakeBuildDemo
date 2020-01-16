namespace CakeDemo.Models
{
    public class Point
    {
        public Point(int x, int y)
        {
            X = x;
            Y = y;
        }

        public int X { get; set; }

        public int Y { get; set; }

        public (int x, int y) GetCoordinatesTuple => (X, Y);

        public int MoveUp(int step) => Y += step;

        public int MoveRight(int step) => X += step;

        public void GetCoordinates(out int x, out int y)
        {
            x = X;
            y = Y;
        }
    }
}
