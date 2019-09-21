using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace n.m._lab2._1
{
    class Program
    {
        static double f(double x)
        {
            return Math.Pow(Math.E, x) - 2 * x - 2;
        }

        static double f_diff(double x)
        {
            return Math.Pow(Math.E, x) - 2;
        }

        static double phi(double x)
        {
            return (Math.Pow(Math.E, x) - 2)/2;
        }

        static void simple_iter(double a, double b)
        {
            double x0 = a - b;
            double x_prev = x0;
            double x = phi(x0);
            double eps = 0.001;
            double iter = 0;
            while (Math.Abs(x - x_prev) > eps)
            {
                x_prev = x;
                x = phi(x);
                iter++;
            }
            Console.WriteLine(x);
            Console.WriteLine("iter");
            Console.WriteLine(iter);
        }

        static void Newton(double a, double b)
        {
            double x0 = a - b;
            double x_prev = x0;
            double x = x0;
            x = x - f(x) / f_diff(x);
            double eps = 0.001;
            double iter = 0;
            while (Math.Abs(x - x_prev) > eps)
            {
                x_prev = x;
                x = x - f(x) / f_diff(x);
                iter++;
            }
            Console.WriteLine(x);
            Console.WriteLine("iter");
            Console.WriteLine(iter);
        }

        static void Main(string[] args)
        {
            double a = -10;
            double b = 10;
            Console.WriteLine("simple_iter");
            simple_iter(a, b);
            Console.WriteLine("Newton");
            Newton(a, b);
            Console.ReadKey();
        }
    }
}
