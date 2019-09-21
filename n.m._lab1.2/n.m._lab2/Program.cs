using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace n.m._lab2
{
    class Program
    {
        static double[] Tridiagonal(double[,] A, double[] B, int n)
        {
            double[] a = new double[n-1];
            double[] b = new double[n];
            double[] c = new double[n-1];
            double[] d;
            d = (double[])B.Clone();
            double m;
            double[] x;
            for (int i = 0; i < n-1; i++)
            {
                a[i] = A[i + 1, i];
                c[i] = A[i, i + 1];
            }

            for(int i = 0; i < n; i++)
                b[i] = A[i, i];

            for (int i = 1; i < n; i++)
            {
                m = a[i - 1] / b[i - 1];
                b[i] = b[i] - m * c[i - 1];
                d[i] = d[i] - m * d[i - 1];
            }

            x = b;
            x[n - 1] = d[n - 1] / b[n - 1];

            for (int i = 0; i < n - 1; i++)
                x[i] = (d[i] - c[i] * x[i + 1]) / b[i];
            x.Reverse();
            return x;
        }

        static void Show(double[] A, int n)
        {
            for (int i = 0; i < n; i++)
            {
                Console.Write(String.Format("{0:0.000  }", A[i]));
                Console.WriteLine();
            }
        }

        static void Main(string[] args)
        {
            int n = 5;
            double[,] A = new double[5, 5]   {{6, -5, 0, 0, 0},
                                        {-6, 16, 9, 0, 0},
                                        {0, 9, -17, -3, 0},
                                        {0, 0, 8, 22, -8},
                                        {0, 0, 0, 6, -13}};
            double[] X = new double[5] { -58, 161, -114, -90, -55};
            double[] result;

            result = Tridiagonal(A, X, n);
            Show(result, n);
            Console.ReadKey();
        }
    }
}
