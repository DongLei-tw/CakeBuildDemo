namespace CakeDemo.Models
{
    public class Circle
    {
        public Circle(int radius)
        {
            Radius = radius;
        }

        public int Radius { get; set; }

        public double Area => Radius * 3.14 * 3.14;
    }
}
