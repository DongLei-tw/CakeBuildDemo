namespace CakeDemo.Models
{
    public class Triangle
    {
        public Triangle(int a, int b, int c)
        {
            A = a;
            B = b;
            C = c;
        }

        public int A { get; set; }

        public int B { get; set; }

        public int C { get; set; }

        public (int a, int b, int c) GetSides() => (A, B, C);
    }
}
