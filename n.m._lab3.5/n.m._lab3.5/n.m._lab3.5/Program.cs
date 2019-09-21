using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace n.m._lab3._5
{
    class Program
    {
        static double runge_romberg(double h1, double h2, double y1, double y2,int n = 2)
        {
            return Math.Abs((y1 - y2) / (Math.Pow((h2/h1), n) - 1.0));
        }
        
        static double f(double x)
        {
            //return x / ((2 * x + 7) * (3 * x + 4));
            return x - 5;
        }

        static List<double> arange(double x0, double xk, double step)
        {
            var x = x0;
            List<double> r = new List<double>();

            while(x <= xk)
            {
                r.Add(x);
                x += step;
            }

            return r;
        }

        static double Rectangle(double h, List<double> x)
        {
            double summ = 0;

            for(int i = 1; i < x.Count; i++)
                summ += f((x[i - 1] + x[i]) / 2);

            return h * summ;
        }

        static double Trapezium(double h, List<double> x)
        {
            double summ = 0;

            for (int i = 1; i < x.Count - 1; i++)
                summ += f(x[i]);

            return h * (f(x[0]) / 2 + summ + f(x[x.Count - 1]));
        }

        static double Simpson(double h, List<double> x)
        {
            double summ = 0;

            for (int i = 1; i < x.Count - 1; i++)
            {
                summ += 4 * f(x[i]);
                i++;
            }

            for (int i = 2; i < x.Count - 1; i++)
            {
                summ += 2 * f(x[i]);
                i++;
            }

            return (h / 3) * (f(x[0]) + summ + f(x[x.Count - 1]));
        }

        static void Main(string[] args)
        {
            double x0 = 7.5;
            double xk = 10.5;
            double h1 = 1;
            double h2 = 0.25;

            var val = -0.04; //wolfram alpha

            var x_h1 = arange(x0, xk, h1);
            var x_h2 = arange(x0, xk, h2);
            Console.WriteLine("Метод прямоугольников");
            var first = Rectangle(h1, x_h1);
            var second = Rectangle(h2, x_h2);
            Console.WriteLine("h1");
            Console.WriteLine(first);
            Console.WriteLine("h2");
            Console.WriteLine(second);
            Console.WriteLine("Погрешность:");
            Console.WriteLine("Рунге-Ромберга");
            Console.WriteLine(runge_romberg(h1, h2, first, second, 1));
            Console.WriteLine("Разница с точным решением");
            Console.WriteLine(first - val);
            Console.WriteLine();

            Console.WriteLine("Метод трапеций");
            first = Trapezium(h1, x_h1);
            second = Trapezium(h2, x_h2);
            Console.WriteLine("h1");
            Console.WriteLine(first);
            Console.WriteLine("h2");
            Console.WriteLine(second);
            Console.WriteLine("Погрешность:");
            Console.WriteLine("Рунге-Ромберга");
            Console.WriteLine(runge_romberg(h1, h2, first, second, 2));
            Console.WriteLine("Разница с точным решением");
            Console.WriteLine(first - val);
            Console.WriteLine();

            Console.WriteLine("Метод Симпсона");
            first = Simpson(h1, x_h1);
            second = Simpson(h2, x_h2);
            Console.WriteLine("h1");
            Console.WriteLine(first);
            Console.WriteLine("h2");
            Console.WriteLine(second);
            Console.WriteLine("Погрешность:");
            Console.WriteLine("Рунге-Ромберга");
            Console.WriteLine(runge_romberg(h1, h2, first, second, 4));
            Console.WriteLine("Разница с точным решением");
            Console.WriteLine(first - val);

            Console.ReadKey();

        }
    }
}
