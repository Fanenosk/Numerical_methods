using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace n.m._CurseProject
{
    class Program
    {
        static void Show_x(double[] x) => Console.WriteLine(String.Join(" ", x));

        static void conj_gead(double[,] A, double[] B)
        {
            var M = A.GetLength(0);
            double[] Xk = new double[M];
            double[] Zk = new double[M];
            double[] Sz = new double[M];
            double[] Rk = new double[M];
            double alpha = 0;
            double beta = 0;
            double mf = 0;
            double Spr = 0;
            double Spr1 = 0;
            double Spz = 0;
            double max_iter = 1000000;
            double eps = 0.0000000000000000000000000000001;

            for (int i = 0; i < M; i++)
                for (int j = 0; j < M; j++)
                    if (A[i, j] != A[j, i])
                        System.Environment.Exit(1);

            /* Вычисляем сумму квадратов элементов вектора B*/
            for (int i = 0; i < M; i++)
                mf += B[i] * B[i];

            /* Задаем начальное приближение корней. В Хk хранятся значения корней
            * к-й итерации. */
            for (int i = 0; i < M; i++)
                Xk[i] = 0.2;

            /* Задаем начальное значение r0 и z0. */
            for(int i = 0; i < M; i++)
            {
                for (int j = 0; j < M; j++)
                    Sz[i] += A[i, j] * Xk[j];
                Rk[i] = B[i] - Sz[i];
                Zk[i] = Rk[i];
            }
            var iter = 0;
            do
            {
                iter++;
                /* Вычисляем числитель и знаменатель для коэффициента
                * alpha = (rk-1,rk-1)/(Azk-1,zk-1) */
                Spz = 0;
                Spr = 0;
                for (int i = 0; i < M; i++)
                {
                    Sz[i] = 0;
                    for (int j = 0; j < M; j++)
                    {
                        Sz[i] += A[i,j] * Zk[j];
                    }
                    Spz += Sz[i] * Zk[i];
                    Spr += Rk[i] * Rk[i];
                }
                alpha = Spr / Spz;             /*  alpha    */

                /* Вычисляем вектор решения: xk = xk-1+ alpha * zk-1, 
                вектор невязки: rk = rk-1 - alpha * A * zk-1 и числитель для betaa равный (rk,rk) */
                Spr1 = 0;
                for (int i = 0; i < M; i++)
                {
                    Xk[i] += alpha * Zk[i];
                    Rk[i] -= alpha * Sz[i];
                    Spr1 += Rk[i] * Rk[i];
                }

                /* Вычисляем  beta  */
                beta = Spr1 / Spr;

                /* Вычисляем вектор спуска: zk = rk+ beta * zk-1 */
                for (int i = 0; i < M; i++)
                    Zk[i] = Rk[i] + beta * Zk[i];

            } while (Spr1 / mf > eps * eps && iter < max_iter);

            Show_x(Xk);
        }

        static void Show(double[,] A, int n)
        {
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                    Console.Write(String.Format("{0:0.000  }", A[i, j]));
                Console.WriteLine();
            }
        }

        static void Main(string[] args)
        {
            //double[,] A = {{3, -1},
            //               {-1, 3}};
            //double[] B = { 3, 7 };

            //conj_gead(A, B);

            string[] lines = System.IO.File.ReadAllLines("C:/Users/Fanen/OneDrive/Рабочий стол/6 семестр/n.m._CurseProject/n.m._CurseProject/input_A.txt").ToArray();
            string[] line = System.IO.File.ReadAllLines("C:/Users/Fanen/OneDrive/Рабочий стол/6 семестр/n.m._CurseProject/n.m._CurseProject/input_B.txt").ToArray();

            int size = Int32.Parse(lines[0]);
            double[,] A = new double[size, size];
            double[] B = new double[size];

            int[] row = line[0].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).Select(Int32.Parse).ToArray();
            for (int i = 0; i < size; i++)
                B[i] = row[i];

            for (int i = 1; i < size + 1; i++)
            {
                row = lines[i].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).Select(Int32.Parse).ToArray();
                for (int j = 0; j < size; j++)
                    A[i - 1, j] = row[j];
            }

            Show(A, size);
            Show_x(B);

            conj_gead(A, B);

            Console.ReadKey();
        }
    }
}
