using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace n.m._lab4
{
    class Program
    {
        static double[,] Multi_n_n(double[,] A, double[,] B, int n)
        {
            double[,] Result = new double[n,n];
            for (int i = 0; i < n; i++)
                for (int j = 0; j < n; j++)
                    for (int k = 0; k < n; k++)
                        Result[i, j] += A[i, k] * B[k, j];
            return Result;
        }

        static double[,] Trans_Matrix(double[,] A, int n)
        {
            double tmp;
            double[,] At = (double[,])A.Clone();
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < i; j++)
                {
                    tmp = At[i, j];
                    At[i, j] = At[j, i];
                    At[j, i] = tmp;
                }
            }
            return At;
        }

        static void Show_lambda(double[,] A, int n)
        {
            Console.WriteLine("lambda");
            for (int i = 0; i < n; i++)
            {
                Console.Write(String.Format("{0:0.000}", A[i,i]));
                Console.WriteLine();
            }
        }

        static void Show_own_vector(double[,] A, int n)
        {
            for(int i = 0; i < n; i++)
            {
                Console.WriteLine("vector " + i);
                for(int j = 0; j < n; j++)
                {
                    Console.WriteLine(String.Format("{0:0.0000  }", A[j, i]));
                }
            }
        }

        static void Show(double[,] A, int n)
        {
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    Console.Write(String.Format("{0:0.000  }", A[i, j]));
                }
                Console.WriteLine();
            }
        }

        static double End_of_method(double[,] A,int n)
        {
            double e = 0;
            for(int i = 0; i < n; i++)
                for(int j = 0; j < n; j++)
                    if (i < j)
                        e += Math.Pow(A[i, j],2);

            return Math.Pow(e, 0.5);
        }

        static void Rotate_Jacobi(double[,] A, int n)
        {
            double[,] U = new double[n, n];
            double epsilon = 0.01;
            double e = End_of_method(A, n);
            double fi = 0;
            int ik = 0;
            int jk = 0;
            int iter = 0;

            while (e > epsilon)
            {
                double max = 0;
                for (int i = 0; i < n; i++)
                    for (int j = 0; j < n; j++)
                        if (i < j && max < Math.Abs(A[i, j]))
                        {
                                max = Math.Abs(A[i, j]);
                                ik = i;
                                jk = j;
                        }

                if (A[ik, ik] == A[jk, jk])
                {
                    fi = Math.PI / 4;
                } else fi = (0.5) * (Math.Atan(2 * A[ik, jk] / (A[ik, ik] - A[jk, jk])));

                double[,] Uk = new double[n, n];
                for (int i = 0; i < n; i++)
                    Uk[i, i] = 1;

                Uk[ik, jk] = -Math.Sin(fi);
                Uk[jk, ik] = -Uk[ik,jk];
                Uk[ik, ik] = Math.Cos(fi);
                Uk[jk, jk] = Math.Cos(fi);

                if (iter == 0)
                {
                    U = Uk;
                    iter += 1;
                }
                else { U = Multi_n_n(U, Uk, n); iter += 1; }

                double[,] Ut = new double[n,n];
                Ut = Trans_Matrix(Uk, n);
                A = Multi_n_n(Multi_n_n(Ut, A, n),Uk,n);
                e = End_of_method(A, n);
            }
            Console.WriteLine("A");
            Show(A, n);
            Show_lambda(A, n);
            Show_own_vector(U, n);
            Console.WriteLine("U");
            Show(U, n);
        }
        
        static void Main(string[] args)
        {
            double[,] A = new double[3, 3]   {{4, 2, 1},
                                        {2, 4, 3},
                                        {1, 3, 6}};
            int n = 3;

            Rotate_Jacobi(A, n);
            Console.ReadKey();
        }
    }
}
