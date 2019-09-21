using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace n.m._lab4._1
{
    class Program
    {
        static double runge_romberg(double h1, double h2, double y1, double y2, int n = 2)
        {
            return Math.Abs((y1 - y2) / (Math.Pow((h2 / h1), n) - 1.0));
        }

        static double exact(double x)
        {
            return (1 + x) * Math.Pow(Math.E, Math.Pow( -x, 2));
        }

        static double f(double x, double y, double z)
        {
            return z;
        }

        static double g(double x, double y, double z)
        {
            return -4 * x * z - (4 * Math.Pow(x, 2) + 2) * y;
        }

        static List<double> Clone(List<double> listToClone)
        {
            return listToClone.Select(x => (double)x).ToList();
        }

        //static void Show(List<double> output) => Console.WriteLine(String.Format("{0:0.000000  }", String.Join(" ", output)));

        //static double ParseToInt(double a) => a.To

        static void Show_result(List<List<double>> result)
        {
            Console.Write("x: ");
            for (int i = 0; i < result[0].Count; i++)
                Console.Write(String.Format("{0:0.000000  }", result[0][i]));
            Console.WriteLine();

            Console.Write("y: ");
            for (int i = 0; i < result[1].Count; i++)
                Console.Write(String.Format("{0:0.000000  }", result[1][i]));
            Console.WriteLine();

            Console.Write("z = y': ");
            for (int i = 0; i < result[2].Count; i++)
                Console.Write(String.Format("{0:0.000000  }", result[2][i]));
            Console.WriteLine();

            Console.Write("Разница с точным значением: ");
            for (int i = 0; i < result[1].Count; i++)
                Console.Write(String.Format("{0:0.000000  }", Math.Abs(exact(result[0][i])) - result[1][i]));
            Console.WriteLine();

            Console.WriteLine();
        }

        static List<List<double>> euler(List<double> x0, List<double> y0, List<double> z0, double h, int n)
        {
            List<double> x = Clone(x0);
            List<double> y = Clone(y0);
            List<double> z = Clone(z0);

            for(int i = 0; i < n - 1; i++)
            {
                y.Add(y[i] + h * f(x[i], y[i], z[i]));
                z.Add(z[i] + h * g(x[i], y[i], z[i]));
                x.Add(x[i] + h);
            }

            List<List<double>> output = new List<List<double>>();
            output.Add(x);
            output.Add(y);
            output.Add(z);
            //Console.Write("x: ");
            //Show_list(x);
            //Console.Write("y: ");
            //Show_list(y);
            //Console.Write("z = y' :");
            //Show_list(z);

            return output;
        }

        static void Main(string[] args)
        {
            double h = 0.1;
            double x_min = 0;
            double x_max = 1;
            List<double> x = new List<double>();
            List<double> y = new List<double>();
            List<double> z = new List<double>();
            List<List<double>> result = new List<List<double>>();

            x.Add(x_min);
            y.Add(1.0);
            z.Add(1.0);
            var n = (int)Math.Ceiling((x_max - x_min) / h) + 1;

            Console.WriteLine("Euler");
            result = euler(x, y, z, h, n);
            Show_result(result);

            Console.ReadKey();
        }
    }
}
