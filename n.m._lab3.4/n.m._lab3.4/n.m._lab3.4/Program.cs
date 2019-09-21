using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace n.m._lab3._4
{
    class Program
    {

        static void Derivative(double[] x, double[] y, double val)
        {
            var n = x.GetLength(0);
            var k = 0;

            try
            {
                while (x[k + 1] < val)
                    k += 1;
            }
            catch {
                Console.WriteLine("value does not fall within the interval");
                System.Environment.Exit(1);
            }

            if (k + 1 > n + 1)
                k -= 1;
            var first = (y[k + 1] - y[k]) / (x[k + 1] - x[k]);
            var second = first +
             ((y[k + 2] - y[k + 1]) / (x[k + 2] - x[k + 1]) - (y[k + 1] - y[k]) / (x[k + 1] - x[k])) *
             (2 * val - x[k] - x[k + 1]) /
             (x[k + 2] - x[k]);

            //first.ToString().Select(tmp => (string)tmp.ToString());
            Console.WriteLine("first_Derivative");
            Console.WriteLine(first);
            Console.WriteLine("second_Derivative");
            Console.WriteLine(second);
        }

        static void Main(string[] args)
        {
            double[] x = { -2.0, 0.0, 0.2, 0.4, 0.6 };
            double[] y = { -0.20136, 0.0, 0.20136, 0.41152, 0.64350 };
            double x_0 = 0.2;

            double[] x_test = { 0.0, 0.1, 0.2, 0.3, 0.4 };
            double[] y_test = { 1.0, 1.1052, 1.2214, 1.3499, 1.4918 };
            double x_0_test = 0.2;

            Derivative(x, y, x_0);
            Console.WriteLine("test");
            Derivative(x_test, y_test, x_0_test);
            Console.ReadKey();
        }
    }
}
