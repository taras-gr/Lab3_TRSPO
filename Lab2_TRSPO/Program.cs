using MathNet.Numerics.LinearAlgebra;
using System;
using System.Diagnostics;
using System.Threading;

namespace Lab2_TRSPO
{
    class Program
    {
        static void Main(string[] args)
        {
            MatrixFactory matrixFactory = new MatrixFactory();
            VectorFactory vectorFactory = new VectorFactory();

            Console.Write("N: ");
            int n = Convert.ToInt32(Console.ReadLine());

            var matrixA = matrixFactory.GetMatrix(MatrixEnum.A, n);
            var vectorB = vectorFactory.GetVector(VectorEnum.b, n);
            var matrixA1 = matrixFactory.GetMatrix(MatrixEnum.A1, n);
            var vectorB1 = vectorFactory.GetVector(VectorEnum.b1, n);
            var vectorC1 = vectorFactory.GetVector(VectorEnum.c1, n);
            var matrixA2 = matrixFactory.GetMatrix(MatrixEnum.A2, n);
            var matrixB2 = matrixFactory.GetMatrix(MatrixEnum.B2, n);
            var matrixC2 = matrixFactory.GetMatrix(MatrixEnum.C2, n);

            var vectorY1 = matrixA * vectorB;
            var vectorY2 = matrixA1 * ((17 * vectorB1) + vectorC1);
            var matrixY3 = matrixA2 * (matrixB2 + matrixC2);

            Matrix<double> matrixResult = null;

            #region 1
            Thread mainThread = new Thread(() =>
            {
                Console.WriteLine("L41");
                Matrix<double> firstMainAdd = null;
                double secondMainAdd = 0.0;

                Thread forFirstMainAdd = new Thread(() =>
                {
                    Console.WriteLine("L31");
                    Matrix<double> firstAddition = null;
                    Matrix<double> secondAddition = null;

                    Thread forFirstAdd = new Thread(() =>
                    {
                        Console.WriteLine("L21");
                        Matrix<double> firstMul = null;

                        Thread l11 = new Thread(() =>
                        {
                            Console.WriteLine("L11");
                            firstMul = matrixY3 * matrixY3;
                        });

                        l11.Start();
                        l11.Join();

                        firstAddition = firstMul * matrixY3;
                    });

                    Thread forSecondAdd = new Thread(() =>
                    {
                        Console.WriteLine("L22");
                        double firstMul = 0.0;

                        Thread l12 = new Thread(() =>
                        {
                            Console.WriteLine("L12");
                            firstMul = vectorY2 * vectorY1;
                        });

                        l12.Start();
                        l12.Join();

                        secondAddition = firstMul * matrixY3;
                    });

                    forFirstAdd.Start();
                    forSecondAdd.Start();

                    forFirstAdd.Join();
                    forSecondAdd.Join();

                    firstMainAdd = firstAddition + secondAddition;
                });

                Thread forSecondMainAdd = new Thread(() =>
                {
                    Console.WriteLine("L32");
                    double thirdAddition = 0.0;
                    double fourthAddition = 0.0;

                    Thread forThirdAdd = new Thread(() =>
                    {
                        Console.WriteLine("L23");
                        thirdAddition = vectorY1 * vectorY2;
                    });

                    Thread forFourthAdd = new Thread(() =>
                    {
                        Console.WriteLine("L24");
                        Vector<double> firstMul = null;

                        Thread l13 = new Thread(() =>
                        {
                            Console.WriteLine("L13");
                            firstMul = matrixY3 * vectorY2;
                        });

                        l13.Start();
                        l13.Join();

                        fourthAddition = firstMul * vectorY1;
                    });

                    forThirdAdd.Start();
                    forFourthAdd.Start();

                    forThirdAdd.Join();
                    forFourthAdd.Join();

                    secondMainAdd = thirdAddition + fourthAddition;
                });

                forFirstMainAdd.Start();
                forSecondMainAdd.Start();

                forFirstMainAdd.Join();
                forSecondMainAdd.Join();

                matrixResult = secondMainAdd + firstMainAdd;
            });

            #endregion

            Stopwatch sw = new Stopwatch();
            sw.Start();
            mainThread.Start();
            mainThread.Join();
            sw.Stop();
            Console.WriteLine(sw.ElapsedMilliseconds);
            Console.ReadLine();
            //for (int i = 0; i < matrixResult.RowCount; i++)
            //{
            //    for (int j = 0; j < matrixResult.ColumnCount; j++)
            //    {
            //        Console.Write(matrixResult[i,j] + " ");
            //    }
            //    Console.WriteLine();
            //}
        }
    }
}
