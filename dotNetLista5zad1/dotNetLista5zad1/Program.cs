using System;

class Program
{
    static void Main(string[] args)
    {
        for (int i = 0; i < 6; i++)
        {
            Console.WriteLine("Enter a: ");
            double a = Convert.ToDouble(Console.ReadLine());
            Console.WriteLine("Enter b: ");
            double b = Convert.ToDouble(Console.ReadLine());
            Console.WriteLine("Enter c: ");
            double c = Convert.ToDouble(Console.ReadLine());

            Console.WriteLine($"Your equation: {a}x^2 + {b}x + {c} = 0");
            (int solutions, double? x1, double? x2) = QuadtraticEquation(a, b, c);

            switch (solutions)
            {
                case 0:
                    Console.WriteLine("no solutions");
                    break;
                case 1:
                    Console.WriteLine($"1 solution: x = {x1:F3}");
                    break;
                case 2:
                    Console.WriteLine($"2 solutions: x = {x1:F3} or x = {x2:F3}");
                    break;
                default:
                    Console.WriteLine("identity equation - infinitely many solutions");
                    break;
            }
            Console.WriteLine("\n\n");
        }

    }

    static (int , double?, double?) QuadtraticEquation(double a, double b, double c)
    {
        //non-quadratic
        if(a==0)
        {
            if(b==0)
                return c==0 ? (int.MaxValue,null,null) : (0,null,null);
            return (1, -c / b, null);
        }

        //quadratic
        else
        {
            double delta = b * b - 4 * a * c;
            if(delta < 0)
                return (0,null,null);
            else if(delta == 0)
                return (1, -b / (2 * a),null);
            else
            {
                double sqrtDelta = Math.Sqrt(delta);
                return (2, (-b - sqrtDelta) / (2 * a), (-b + sqrtDelta) / (2 * a));
            }
        }
    }
}
