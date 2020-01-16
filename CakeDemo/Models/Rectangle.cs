namespace CakeDemo.Models
{
    public class Rectangle
    {
        public Rectangle(int length, int height)
        {
            Length = length;
            Height = height;
        }

        public int Length { get; set; }

        public int Height { get; set; }

        public double Area => Length * Height;
    }
}
