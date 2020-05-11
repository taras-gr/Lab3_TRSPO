using MathNet.Numerics.LinearAlgebra;
using System;

namespace Lab2_TRSPO
{
    public class MatrixFactory
    {
        public Matrix<double> GetMatrix(MatrixEnum en, int count)
        {
            switch (en)
            {
                case MatrixEnum.A:
                    return GetSquareRandomMatrix(count);
                case MatrixEnum.A1:
                    return GetSquareRandomMatrix(count);
                case MatrixEnum.B2:
                    return GetSquareRandomMatrix(count);
                case MatrixEnum.A2:
                    return GetSquareRandomMatrix(count);
                case MatrixEnum.C2:
                    return GetC2Matrix(count);
                default:
                    return null;
            }
        }

        private Matrix<double> GetC2Matrix(int n)
        {
            var matrix = Matrix<double>.Build.Dense(n, n);
            for (int i = 0; i < matrix.RowCount; i++)
            {
                for (int j = 0; j < matrix.ColumnCount; j++)
                {
                    matrix[i, j] = 17 / (2 * (i + 1) + j + 1);
                }
            }

            return matrix;
        }

        private Matrix<double> GetSquareRandomMatrix(int n)
        {
            Random r = new Random();
            var matrix = Matrix<double>.Build.Dense(n, n);
            for (int i = 0; i < matrix.RowCount; i++)
            {
                for (int j = 0; j < matrix.ColumnCount; j++)
                {
                    matrix[i, j] = r.Next(0, n);
                }
            }

            return matrix;
        }
        
    }
}
