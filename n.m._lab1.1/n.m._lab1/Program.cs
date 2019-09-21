using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;




namespace n.m._lab1
{
    class Program
    {
        static public void LU(double[,] L, double[,] U, double[,] A, int n)
        {
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    double sum_u = 0;
                    for (int k = 0; k < i; k++)
                    {
                        sum_u += U[k, j] * L[i, k];
                    }
                    U[i, j] = A[i, j] - sum_u;
                }
                for(int j = 0; j < n; j++)
                {
                    double sum_l = 0;
                    for (int k = 0; k < i; k++)
                    {
                        sum_l += U[k, i] * L[j, k];
                    }
                    L[j, i] = (A[j, i] - sum_l) / U[i, i];
                }
            }
        }

        static double[] find_x(double[,] L, double[,] U,double[] b, int n)
        {
            double[] z = new double[n];
            
            for(int i = 0; i < n; i++)
            {
                double z_sum = 0;
                for (int j = 0; j < n; j++)
                    z_sum += L[i, j] * z[j];
                z[i] = b[i] - z_sum;
            }

            double[] x = new double[n];

            for(int i = 0; i < n; i++)
            {
                x[i] = z[i];
                double x_sum = 0;
                for(int j = 0; j < n; j++)
                {
                    x_sum += U[i, j] * x[j];
                    x[i] -= x_sum;
                    x[i] /= U[i, i];
                }
            }
            return x;
        }

        static void Multi(double[,] A, double[,] B, double[,] R, int n)
        {
            for (int i = 0; i < n; i++)
                for (int j = 0; j < n; j++)
                    for (int k = 0; k < n; k++)
                        R[i,j] += A[i,k] * B[k,j];
        }

        static void Show(double[,] A, int n)
        {
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    Console.Write(String.Format("{0:0.000  }",A[i,j]));
                }
                Console.WriteLine();
            }
        }

        static void Show_string(double[] A, int n)
        {
            for (int i = 0; i < n; i++)
            {
               Console.Write(String.Format("{0:0.000  }", A[i]));
            }
        }

        static double[,] trans_Matrix(double[,] A, int n)
        {
            double t;
            double[,] tr_A = new double[n, n];
            tr_A = (double[,])A.Clone();
            for (int i = 0; i < n; ++i)
            {
                for (int j = i; j < n; ++j)
                {
                    t = tr_A[i,j];
                    tr_A[i,j] = tr_A[j,i];
                    tr_A[j,i] = t;
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
                    p[ki,kj] = mas[ki + di,kj + dj];
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
                d = mas[0,0];
                return (d);
            }
            if (n == 2)
            {
                d = mas[0,0] * mas[1,1] - (mas[1,0] * mas[0,1]);
                return (d);
            }
            if (n > 2)
            {
                for (int i = 0; i < n; i++)
                {
                    GetMatr(mas, p, i, 0, n);
                    d = d + k * mas[i,0] * Determinant(p, m);
                    k = -k;
                }
            }
            return (d);
        }

        static void inverse_Matrix(double[,] A, double[,] inv_A,int n)
        {
            double[,] tmp = new double[n-1,n-1];
            double det = 0;
            double d = 0;
            double[,] tr_A = new double[n, n];
            tr_A = trans_Matrix(A, n);
            d = Determinant(A, n);
            for(int i = 0; i < n; i++)
            {
                for(int j = 0; j < n; j++)
                {
                    tmp = GetMatr(tr_A, tmp, i, j, n);
                    det = Determinant(tmp, n - 1);
                    inv_A[i, j] = det / d;
                }
            }
        }





        static void Main(string[] args)
        {
            double[,] L = new double[4, 4];
            double[,] H = new double[4, 4];
            double[,] U = new double[4, 4];
            double[,] R = new double[4, 4];
            double[,] inv_A = new double[4, 4];
            double[,] A = new double[4, 4]   {{1, 2, 1, 7},
                                        {8, 9, 0, 3},
                                        {2, 3, 7, 1},
                                        {1, 5, 6, 8}};
            double[] B = new double[4] { -23, 39, -7, 30 };
            double[] x = new double[4];
            int n = 4;

            LU(L, U, A, n);
            Console.WriteLine("A");
            Show(A,n);
            Console.WriteLine("U");
            Show(U, n);
            Console.WriteLine("L");
            Show(L, n);
            Multi(L, U, R, n);
            Console.WriteLine("Result");
            Show(R, n);
            Console.WriteLine("Inverse Matrix");
            inverse_Matrix(A, inv_A, n);
            Show(inv_A,n);
            Console.WriteLine("Determinant");
            double d = Determinant(A, n);
            Console.WriteLine(d);
            x = find_x(L, U, B, n);
            Console.WriteLine("Solution");
            Show_string(x, n);
            Console.ReadKey();
        }
    }
}