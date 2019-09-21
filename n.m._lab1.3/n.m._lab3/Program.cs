using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace n.m._lab3
{
    
    class Program
    {

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

        static void inverse_Matrix(double[,] A, double[,] inv_A, int n)
        {
            double[,] tmp = new double[n - 1, n - 1];
            double det = 0;
            double d = 0;
            double[,] At = new double[n, n];
            At = trans_Matrix(A, n);
            d = Determinant(A, n);
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    tmp = GetMatr(At, tmp, i, j, n);
                    det = Determinant(tmp, n - 1);
                    inv_A[i, j] = det / d;
                }
            }
        }

        static double Norma_n_n(double[,] tmp, int n)
        {
            double norma = 0;
            for (int i = 0; i < n; i++)
                for (int j = 0; j < n; j++)
                    norma += tmp[i, j] * tmp[i, j];
            return Math.Sqrt(norma);
        }

        static double Norma_n(double[] tmp, int n)
        {
            double norma = 0;
            for (int i = 0; i < n; i++)
                    norma += tmp[i] * tmp[i];
            return Math.Sqrt(norma);
        }

        static double[] Multi_n(double[,] A, double[] B,int n)
        {
            double[] Result = new double[n];
            for(int i = 0; i < n; i++)
                for(int j = 0; j < n; j++)
                    Result[i] += A[i, j] * B[j];
            return Result;
        }

        static double[,] Multi_n_n(double[,] A, double[,] B, int n)
        {
            double[,] Result = new double[n,n];
            for (int i = 0; i < n; i++)
                for (int j = 0; j < n; j++)
                    for (int k = 0; k < n; k++)
                        Result[i, j] += A[i, k] * B[k, j];
            return Result;
        }

        static double[] Plus(double[] A, double[] B, int n)
        {
            double[] Result = new double[n];
            for(int i = 0; i < n; i++)
                Result[i] = A[i] + B[i];
            return Result;
        }

        static double[] Minus(double[] A, double[] B, int n)
        {
            double[] Result = new double[n];
            for (int i = 0; i < n; i++)
                Result[i] = A[i] - B[i];
            return Result;
        }

        static double End_of_method(double[] A, double[] B, int n)
        {
            double[] tmp = new double[n];
            double e = 0;

            tmp = Minus(A, B, n);
            for(int i = 0; i < n; i++)
                e += tmp[i] * tmp[i]; 
            return Math.Sqrt(e);
        }

        static double[] Simple_iteration(double[,] A, double[] X, int n)
        {
            double[,] alpha = new double[n, n];
            double[] B = new double[n];
            double epsilon = 0.0001;
            double[] result = new double[n];
            double e = End_of_method(X, B, n);
            double iter = 0;

            for (int i = 0; i < n; i++)
            {
                B[i] = X[i] / A[i, i];
                for (int j = 0; j < n - 1; j++)
                    if (j != i)
                        alpha[i, j] = -A[i, j] / A[i, i];
            }

            result = (double[])B.Clone();

            while (e > epsilon)
            {
                double[] prev_result = (double[])result.Clone();
                result = Plus(B, Multi_n(alpha, prev_result, n), n);
                e = End_of_method(result, prev_result, n);
                iter++;
            }
            Console.WriteLine("sim_iter");
            Console.WriteLine(iter);
            return result;
        }

        static double[] Seidel_iteration(double[,] A, double[] X, int n)
        {
            double[,] alpha = new double[n, n];
            double[] B = new double[n];
            double epsilon = 0.0001;
            double e = End_of_method(X, B, n);
            double[] result = new double[n];
            double[] prev_result = new double[n];
            double iter = 0;

            for (int i = 0; i < n; i++)
            {
                B[i] = X[i] / A[i, i];
                for (int j = 0; j < n - 1; j++)
                    if (j != i)
                        alpha[i,j] = -A[i,j] / A[i,i];
            }

            result = (double[])B.Clone();

            while (e > epsilon)
            {
                prev_result = (double[])result.Clone();
                for (int i = 0; i < n; i++)
                {
                    double summ = B[i];
                    for (int j = 0; j < i; j++)
                        summ += alpha[i, j] * result[j];
                    for (int j = i; j < n; j++)
                        summ += alpha[i, j] * prev_result[j];
                    result[i] = summ;       
                }
                e = End_of_method(result, prev_result, n);
                iter++;
            }

            Console.WriteLine("seid_iter");
            Console.WriteLine(iter);
            return result;
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
            double[] simple_result = new double[4];
            double[] seidel_result = new double[4];
            double[,] A = new double[4, 4]   {{23, -6, -5, 9},
                                        {8, 22, -2, 5},
                                        {7, -6, 18, -1},
                                        {3, 5, 5, -19}};
            double[] X = new double[4] { 232, -82, 202, -57 };
            int n = 4;

            simple_result = Simple_iteration(A, X, n);
            Console.WriteLine("Simple_iteretion");
            Show(simple_result, n);
            seidel_result = Seidel_iteration(A, X, n);
            Console.WriteLine("Seidel_iteration");
            Show(seidel_result, n);
            Console.ReadKey();
        }
    }
}
