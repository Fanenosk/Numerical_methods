using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace n.m._lab1._5
{
    class Complex
    {
        public double re;
        public double im;

        public Complex(double x, double y)
        {
            re = x;
            im = y;
        }

        public void Complex_Show(Complex tmp)
        {
            if (tmp.im > 0)
            {
                Console.WriteLine(tmp.re + "+" + tmp.im + "i");
            }else Console.WriteLine(tmp.re + "" + tmp.im + "i");
        }
    }

    class Program
    {
        static double[,] Plus_number(double[,] A, double B, int n)
        {
            var Result = (double[,])A.Clone();
            for (int i = 0; i < n; i++)
                for (int j = 0; j < n; j++)
                    Result[i,j] += B;
            return Result;
        }

        static double[,] Minus(double[,] A, double[,] B, int n)
        {
            var Result = new double[n,n];
            for (int i = 0; i < n; i++)
                for (int j = 0; j < n; j++)
                    Result[i,j] = A[i,j] - B[i,j];
            return Result;
        }

        static double[,] Multiply(double[,] A, double[,] B, int n)
        {
            var Result = new double[n, n];
            for (int i = 0; i < n; i++)
                for (int j = 0; j < n; j++)
                    for (int k = 0; k < n; k++)
                        Result[i, j] += A[i, k] * B[k, j];
            return Result;
        }

        static double Multi_string(double[] A, double[] B, int n)
        {
            var Result = 0.0;
            for (int i = 0; i < n; i++)
                        Result += A[i] * B[i];
            return Result;
        }

        static double[,] Multiply(double[,] A, double B, int n)
        {
            var Result = (double[,])A.Clone();
            for (int i = 0; i < n; i++)
                for (int j = 0; j < n; j++)
                    Result[i, j] *= B;
            return Result;
        }

        static double[] Multiply(double[] A, double B, int n)
        {
           var Result = (double[])A.Clone();
            for (int i = 0; i < n; i++)
                for (int j = 0; j < n; j++)
                    Result[i] *= B;
            return Result;
        }

        static double[,] Multiply(double[] A, double[] B, int n)
        {
            var Result = new double[n,n];
            for (int i = 0; i < n; i++)
                for (int j = 0; j < n; j++)
                    Result[i, j] = A[i] * B[j];
            return Result;
        }

        static double[,] Division_number(double[,] A, double B, int n)
        {
            var Result = (double[,])A.Clone();
            for (int i = 0; i < n; i++)
                for (int j = 0; j < n; j++)
                    Result[i, j] /= B;
            return Result;
        }

        static double Summ_elem(double[,] A,int n, int k)
        {
            double tmp = 0;
            for (int i = k; i < n; i++)
                    tmp += Math.Pow(A[i, k], 2);
            return Math.Pow(tmp, 0.5);
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

        static double[] Reverse_string(double[] A, int n)
        {
            double t = 0;
            double[] tmp = (double[])A.Clone();
            for(int i = 0; i < n / 2; i++)
            {
                t = tmp[i];
                tmp[i] = tmp[n - i - 1];
                tmp[n - i - 1] = t;
            }
            return tmp;
        }

        static double[,] Trans_Matrix(double[,] A, int n)
        {
            double tmp;
            var At = (double[,])A.Clone();
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

        static int Sign(double key)
        {
            if (key > 0)
            {
                return 1;
            }
            else
            {
                if (key < 0)
                {
                    return -1;
                }
                else return 0;
            }
        }

        static double End_of_method(double[,] A, int n)
        {
            double e = 0;
            //for (int i = 0; i < n; i++)
                //for (int j = 0; j < n; j++)
                    //if (i > j)
                        e += Math.Pow(A[2, 0], 2);
            return Math.Pow(e, 0.5);
        }

        static void QR_decomposition(double[,] A, int n)
        {
            double[,] R = new double[n, n];
            double[,] Q = new double[n, n];
            double[] v = new double[n];
            double[,] Ak = (double[,])A.Clone();
            double iterator = 0;
            double eps = 0.01;
            double e = End_of_method(Ak, n);

            while (e > eps)
            {
                for (int k = 0; k < n - 1; k++)
                {
                    for (int i = 0; i < n; i++)
                    {
                        if (i < k)
                        {
                            v[i] = 0;
                        }
                        else if (i == k)
                        {
                            v[i] = Ak[i, i] + (Sign(Ak[i, i])) * Summ_elem(Ak, n, k);
                        }
                        else v[i] = Ak[i, k];
                    }


                    var Hk = new double[n, n];
                    for (int i = 0; i < n; i++)
                        Hk[i, i] = 1;

                    var vt = (double[])v.Clone();
                    vt = Reverse_string(vt, n);
                    var v_vt = Multiply(v, vt, n);
                    var vt_v = Multi_string(vt, v, n) / 2;
                    v_vt = Division_number(v_vt, vt_v, n);
                    Hk = Minus(Hk, v_vt, n);

                    if (iterator == 0)
                    {
                        Q = (double[,])Hk.Clone();
                        iterator++;
                    }
                    else { Q = Multiply(Q, Hk, n); iterator++; }

                    Ak = Multiply(Hk, Ak, n);
                    e = End_of_method(Ak, n);
                    R = (double[,])Ak.Clone();

                    //var selectEnd = e.ToString(x=>x.Tostring());
                    //Console.WriteLine("Ak");
                    //Show(Ak, n);
                }
            }

            int j = 0;
            while (j < n) {
                if (j < n - 1 && Math.Abs(Ak[j + 1, j]) > 1) {
                    double b = Ak[j, j] + Ak[j + 1, j + 1];
                    double c = (Ak[j, j] * Ak[j + 1, j + 1] - Ak[j, j + 1] * Ak[j + 1, j]);
                    double D = Math.Pow(b, 2) - 4 * c;
                    Complex y1 = new Complex(-b / 2, Math.Pow(D, 0.5) / 2);
                    Complex y2 = new Complex(-b / 2, -Math.Pow(D, 0.5) / 2);
                    Console.WriteLine("lamda 1");
                    y1.Complex_Show(y1);
                    Console.WriteLine("lamda 2");
                    y2.Complex_Show(y2);
                    j += 2;
                }
                else
                {
                    Console.WriteLine("lamda 3");
                    Console.WriteLine(Ak[j, j]);
                    j += 1; }
            }

            Console.WriteLine("R");
            Show(R, n);
            Console.WriteLine("Q");
            Show(Q, n);
            Console.WriteLine("A");
            Show(Multiply(Q,Ak,n), n);
            Console.WriteLine("iterator");
            Console.WriteLine(iterator);
        }


        static void Main(string[] args)
        {
            double[,] A = new double[3, 3]   {{1, 3, 1},
                                        {1, 1, 4},
                                        {4, 3, 1}};
            int n = 3;


            QR_decomposition(A, n);
            Console.ReadKey();
        }
    }
}
