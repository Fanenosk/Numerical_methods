using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace n.m._lab2._2
{
    class Program
    {
        static double f1(double x1, double x2)
        {
            return x1 - Math.Cos(x2) - 1;
        }

        static double f2(double x1, double x2)
        {
            return x2 - Math.Log10(x1+1) - 3;
        }

        static double f1_diff_x1(double x1, double x2)
        {
            return 1;
        }

        static double f1_diff_x2(double x1, double x2)
        {
            return Math.Sin(x2);
        }

        static double f2_diff_x1(double x1, double x2)
        {
            return 1/(x1+1);
        }

        static double f2_diff_x2(double x1, double x2)
        {
            return 1;
        }

        static double phi1(double x1, double x2)
        {
            return Math.Cos(x2) + 1;
        }

        static double phi2(double x1, double x2)
        {
            return Math.Log10(x1 + 1) + 3;
        }

        static double Norm(double[] A, double[] B)
        {
            double n = 0;
            for (int i = 0; i < A.GetLength(0); i++)
                n += Math.Pow(A[i] - B[i], 2);
            return Math.Pow(n, 0.5);
        }

        static double[] Minus(double[] A,double[] B)
        {
            var tmp = (double[])A.Clone();
            for (int i = 0; i < A.GetLength(0); i++)
                tmp[i] -= B[i];
            return tmp;
        }

        static double[,] Multiply(double[,] A, double B)
        {
            var tmp = (double[,])A.Clone();
            for (int i = 0; i < A.GetLength(0); i++)
                for (int j = 0; j < A.GetLength(1); j++)
                    tmp[i, j] *= B;
            return tmp;
        }

        static double[] Multiply(double[,] A, double[] B)
        {
            var tmp = new double[A.GetLength(0)];
            for (int i = 0; i < A.GetLength(0); i++)
                for (int j = 0; j < A.GetLength(1); j++)
                    tmp[i] += A[i, j] * B[j];
            return tmp;
        }

        static void Show_x(double[] x) => Console.WriteLine(String.Join(" ", x));
        static void Show_iter(int iter) => Console.WriteLine(String.Join("",iter));

        static double[,] trans_Matrix(double[,] A, int n)
        {
            double t;
            double[,] tr_A = new double[n, n];
            tr_A = (double[,])A.Clone();
            for (int i = 0; i < n; ++i)
            {
                for (int j = i; j < n; ++j)
                {
                    t = tr_A[i, j];
                    tr_A[i, j] = tr_A[j, i];
                    tr_A[j, i] = t;
                }
            }
            return tr_A;
        }

        static double[,] GetMatr(double[,] mas, double[,] p, int i, int j, int n)
        {
            int di, dj;
            di = 0;
            dj = 0;
            for (int ki = 0; ki < n - 1; ki++)
            {
                if (ki == i) di = 1;
                dj = 0;
                for (int kj = 0; kj < n - 1; kj++)
                {
                    if (kj == j) dj = 1;
                    p[ki, kj] = mas[ki + di, kj + dj];
                }
            }
            return p;
        }

        static double Determinant(double[,] mas, int n)
        {
            int k, m;
            double d;
            double[,] p = new double[n, n];
            d = 0;
            k = 1;
            m = n - 1;
            if (n == 1)
            {
                d = mas[0, 0];
                return (d);
            }
            if (n == 2)
            {
                d = mas[0, 0] * mas[1, 1] - (mas[1, 0] * mas[0, 1]);
                return (d);
            }
            if (n > 2)
            {
                for (int i = 0; i < n; i++)
                {
                    GetMatr(mas, p, i, 0, n);
                    d = d + k * mas[i, 0] * Determinant(p, m);
                    k = -k;
                }
            }
            return (d);
        }

        /*static void Newton_system(double a, double b)
        {
            double[] x0 = { a - b, a - b };
            double[] x = (double[])x0.Clone();
            double[] x_prev = (double[])x.Clone();
            double eps = 0.01;
            double[] f = { f1(x[0], x[1]), f2(x[0], x[1]) };
            double[,] J = {{f1_diff_x1(x[0], x[1]), f1_diff_x2(x[0], x[1])},
                           {f2_diff_x1(x[0], x[1]), f2_diff_x2(x[0], x[1])}};
            var iter = 0;
            double e = 1;

            while (Math.Abs(Norm(x, x_prev)) < eps)
            {
                x_prev = (double[])x.Clone();
                double[,] J_inv = {{ f2_diff_x2(x[0], x[1]), -f1_diff_x2(x[0], x[1]) },
                                    { -f2_diff_x1(x[0], x[1]), f1_diff_x1(x[0], x[1]) } };
                var coef = 1/(J[0,0]*J[1,1]-J[0,1]*J[1,0]);

                J_inv = Multiply(J_inv, coef);

                x = Minus(x, Multiply(J_inv, f));
                iter++;
                //e = Math.Abs(Norm(x, x_prev));
            }
            Show_x(x);
            Show_iter(iter);
        }*/

        static void Newton_system(double a, double b)
        {
            double[] x0 = { a - b, a - b };
            double[] x = (double[])x0.Clone();
            double[] x_prev = (double[])x.Clone();
            double eps = 0.00000001;
            var iter = 0;
            double e = 1;

            while (e > eps)
            {
                x_prev = (double[])x.Clone();
                double[,] J = {{ f1_diff_x1(x[0], x[1]), f1_diff_x2(x[0], x[1]) },
                                    { f2_diff_x2(x[0], x[1]), f2_diff_x2(x[0], x[1]) } };
                double[,] A1 = {{f1(x[0], x[1]), f1_diff_x2(x[0], x[1])},
                           {f2(x[0], x[1]), f2_diff_x2(x[0], x[1])}};
                double[,] A2 = {{f1_diff_x1(x[0], x[1]), f1(x[0], x[1])},
                           {f2_diff_x1(x[0], x[1]), f2(x[0], x[1])}};

                var det_A1 = Determinant(A1, 2);
                var det_A2 = Determinant(A2, 2);
                var det_J = Determinant(J, 2);

                var x_1 = x[0] - det_A1 / det_J;
                var x_2 = x[1] - det_A2 / det_J;
                x[0] = x_1;
                x[1] = x_2;
                iter++;
                e = Math.Abs(Norm(x, x_prev));
            }
            Show_x(x);
            Show_iter(iter);
        }

        static void simple_iter_system(double a, double b)
        {
            double[] x0 = { a - b, a - b };
            double[] x = (double[])x0.Clone();
            double[] x_prev = (double[])x.Clone();
            double[] phi = { phi1(x[0], x[1]), phi2(x[0], x[1]) };
            double eps = 0.00000001;
            double e = 1;
            var iter = 0;

            while (e > eps)
            {
                x_prev = (double[])x.Clone();
                var x_1 = phi1(x[0], x[1]);
                var x_2 = phi2(x[0], x[1]);
                x[0] = x_1;
                x[1] = x_2;
                iter++;
                e = Math.Abs(Norm(x, x_prev));
            }

            Show_x(x);
            Show_iter(iter);
        }

        static void Main(string[] args)
        {
            double a = 4;
            double b = 0.1;
            Console.WriteLine("Newton_system");
            Newton_system(a, b);
            Console.WriteLine("simple_iter_system");
            simple_iter_system(a, b);
            Console.ReadKey();
        }
    }
}
