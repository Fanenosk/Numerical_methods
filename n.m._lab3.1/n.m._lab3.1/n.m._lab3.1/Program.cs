using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace n.m._lab3._1
{
    class Program
    {
        static double[] Summ_Polinom(double[] A)
        {
            double[] tmp = new double[A.GetLength(0)];
            for (int i = 0; i < A.GetLength(0); i++)
                tmp[i] = 1;
            for (int i = 0; i < A.GetLength(0); i++)
                for (int j = 0; j < A.GetLength(0); j++)
                    if (i != j)
                        tmp[i] *= (A[i] - A[j]);
            return tmp;
        }

        static double[] f_number(double[] x, int n)
        {
            double[] tmp = new double[n];
            for (int i = 0; i < n; i++)
                ///tmp[i] = Math.Pow(Math.E, x[i]);
                tmp[i] = 1 / (3 * (Math.Pow(x[i] - 1, 2)));
            return tmp;

        }

        static double f(double x)
        {
            //return Math.Pow(Math.E, x);
            return 1 / (3 * (Math.Pow(x - 1, 2)));
        }

        static double[] Division(double[] A, double[] B, int n)
        {
            double[] tmp = (double[])A.Clone();
            for (int i = 0; i < n; i++)
                tmp[i] /= B[i];
            return tmp;
        }

        static List<double> Clone(List<double> listToClone)
        {
            return listToClone.Select(x => (double)x).ToList();
        }

        static void Lagrange(double[] A, double x0)
        {
            double[] x = (double[])A.Clone();
            double[] w = Summ_Polinom(A);
            double[] function = f_number(A, A.GetLength(0));
            double pol = 1;
            double tmp = 0;
            string Lagrange = "";

            double ff = f(x0);
            for (int i = 0; i < A.GetLength(0); i++)
            {
                pol = 1;
                pol *= function[i] / w[i];
                Lagrange += function[i] / w[i];
                for (int j = 0; j < A.GetLength(0); j++)
                {
                    if (i != j)
                    {
                        pol *= (x0 - x[j]);
                        Lagrange += "(" + "x" + "-" + x[j] + ")";
                    }
                }
                if (i != A.GetLength(0) - 1)
                    Lagrange += "+";
                tmp += pol;
            }

            Console.WriteLine(Math.Abs(ff - tmp));
            Console.Write(Lagrange);
        }

        static void Newton(double[] A, double x0)
        {
            var x = (double[])A.Clone();
            string Newton = "";
            double summ = 0;
            List<double> fi = new List<double>();

            double ff = f(x0);

            for(int i = 0; i < A.GetLength(0); i++)
            {
                double mul = 1;
                var tmp = "";

                for (int j = 0; j < i; j++)
                {
                    mul *= x0 - x[j];
                    tmp += "(" + "x" + "-" + x[j] + ")";
                }

                var fi_prev = Clone(fi);
                fi = new List<double>();

                for (int j = 0; j < A.GetLength(0) - i; j++)
                {
                    if (i > 0)
                    {
                        fi.Add((fi_prev[j] - fi_prev[j + 1]) / (x[j] - x[j + i]));
                    }
                    else fi.Add(f(x[j]));
                }

                var w = fi[0];
                mul = mul * w;

                summ += mul;

                if(Newton != "" && w >= 0)
                    Newton += "+";
                if (w != 0 && tmp != " ")
                    Newton += w + tmp;
            }

            Console.WriteLine(Newton);
            Console.WriteLine(Math.Abs(ff - summ));
        }

        static void Main(string[] args)
        {
            double[] x_lagrange = { -2, -1, 0 };
            double[] x_newton = { 2, 3, 5 };
            double x0 = 4;

            Console.WriteLine("Lagrange");
            Lagrange(x_lagrange, x0);
            Console.WriteLine();
            Console.WriteLine("Newton");
            Newton(x_newton, x0);
            Console.ReadKey();
        }
    }
}
