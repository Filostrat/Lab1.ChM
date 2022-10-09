using System;

namespace Lab1.ChM
{
    class Program
    {
        static void Main(string[] args)
        {
            var AMatrix = Addmatrix();
            PrintMatrix(AMatrix);

            var Tmatrix = GetMatrix(AMatrix);
            Down(Tmatrix, AMatrix);
            PrintMatrix(Tmatrix);

            var TransposeTmatrix = new double[Tmatrix.GetLength(0), Tmatrix.GetLength(1)];
            Array.Copy(Tmatrix,TransposeTmatrix,TransposeTmatrix.Length);
            TransposeTmatrix = TransposeMatrix(TransposeTmatrix);
            PrintMatrix(TransposeTmatrix);
            PrintMatrix(MultiplyMatrix(TransposeTmatrix, Tmatrix));

            var tempB = GetArray(AMatrix);
            Console.Write("B = ");
            PrintArray(tempB);

            var tempY = Get_Y(tempB, TransposeTmatrix);
            Console.Write("Y = ");
            PrintArray(tempY);

            var tempX = Get_X(tempY, Tmatrix);
            Console.Write("X = ");
            PrintArray(tempX);
        }

        public static void Down(double[,] Tmatrix, double[,] AMatrix)
        {
            double temp = 0;
            Tmatrix[0, 0] = Math.Sqrt(AMatrix[0, 0]);

            for (int j = 1; j < Tmatrix.GetLength(1); j++)
            {
                Tmatrix[0, j] = AMatrix[0, j] / Math.Sqrt(AMatrix[0, 0]);
            }

            for (int i = 1; i < Tmatrix.GetLength(0); i++)
            {
                temp = 0;
                for (int k = 0; k < i; k++)
                {
                    temp += Math.Pow(Tmatrix[k, i], 2);
                }
                Tmatrix[i, i] = Math.Sqrt(AMatrix[i, i] - temp);

                for (int j = 0; j < Tmatrix.GetLength(1); j++)
                {
                    if (i < j)
                    {
                        temp = 0;
                        for (int k = 0; k < i; k++)
                        {
                            temp += Tmatrix[k, i] * Tmatrix[k, j];
                        }
                        Tmatrix[i, j] = (AMatrix[i, j] - temp) / Tmatrix[i, i];
                    }
                    if (i > j)
                    {
                        Tmatrix[i, j] = 0;
                    }
                }
            }
        }

        public static double[] Get_Y(double[] tempB, double[,] TransposeTmatrix)
        {
            double[] tempY = new double[tempB.Length];
            tempY[0] = tempB[0]/ TransposeTmatrix[0,0];

            for (int i = 1; i < TransposeTmatrix.GetLength(0); i++)
            {
                var sum = 0.0;
                for (int k = 0; k < i; k++)
                {
                    sum += TransposeTmatrix[i, k] * tempY[k];
                }
                tempY[i] = (tempB[i] - sum) / TransposeTmatrix[i, i];
            }
            return tempY;
        }

        public static double[] Get_X(double[] tempY, double[,] Tmatrix)
        {
            double[] tempX = new double[tempY.Length];
            tempX[^1] = tempY[^1] / Tmatrix[Tmatrix.GetLength(0)-1, Tmatrix.GetLength(1)-1];

            for (int i = Tmatrix.GetLength(0)-1; i != -1; i--)
            {
                var sum = 0.0;
                for (int k = i+1; k < Tmatrix.GetLength(0); k++)
                {
                    sum += Tmatrix[i, k] * tempX[k];
                }
                tempX[i] = (tempY[i] - sum) / Tmatrix[i, i];
            }
            return tempX;
        }

        public static void PrintArray(double[] temp)
        {
            Console.Write("|");
            for (int i = 0; i < temp.Length; i++)
            {
                temp[i] = Math.Round(temp[i],4);
                Console.Write( $"{temp[i],10}");

            }
            Console.WriteLine("|");
        }

        public static void PrintMatrix(double[,] matrix)
        {
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                Console.Write("|");
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    matrix[i,j] = Math.Round(matrix[i, j], 4);
                    Console.Write((j < 5) ? $" {matrix[i, j],6} " : $" | {matrix[i, j],6} ");
                }
                Console.WriteLine("|");
            }
            Console.WriteLine();
        }

        public static double[,] Addmatrix()
        {
            double[,] MyMatrix = {{5.18,1.12,0.95,1.32,0.83,7.59},
                                 {1.12,4.28,2.12,0.57,0.91,3.21},
                                 {0.95,2.12,6.13,1.29,1.57,2.88},
                                 {1.32, 0.57,1.29,4.57,1.25,6.25},
                                 {0.83,0.91,1.57,1.25,5.21,6.35}};
            return MyMatrix;
        }

        public static double[,] GetMatrix(double[,] matrix)
        {
            var Copymatrix = new double[matrix.GetLength(0), matrix.GetLength(1) - 1];
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1) - 1; j++)
                {
                    Copymatrix[i, j] = matrix[i, j];
                }
            }
            return Copymatrix;
        }       

        public static double[,] MultiplyMatrix(double[,] A, double[,] B)
        {
            int rA = A.GetLength(0);
            int cA = A.GetLength(1);
            int rB = B.GetLength(0);
            int cB = B.GetLength(1);

            if (cA != rB)
            {
                Console.WriteLine("Matrixes can't be multiplied!!");
                return null;
            }
            else
            {
                double temp = 0;
                double[,] kHasil = new double[rA, cB];

                for (int i = 0; i < rA; i++)
                {
                    for (int j = 0; j < cB; j++)
                    {
                        temp = 0;
                        for (int k = 0; k < cA; k++)
                        {
                            temp += A[i, k] * B[k, j];
                        }
                        kHasil[i, j] = temp;
                    }
                }

                return kHasil;
            }
        }

        public static double[,] TransposeMatrix(double[,] matrix)
        {
            var rows = matrix.GetLength(0);
            var columns = matrix.GetLength(1);

            var result = new double[columns, rows];

            for (var c = 0; c < columns; c++)
            {
                for (var r = 0; r < rows; r++)
                {
                    result[c, r] = matrix[r, c];
                }
            }

            return result;
        }

        public static double[] GetArray(double [,] matrix)
        {
            var Array = new double[matrix.GetLength(0)];
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                Array[i] = matrix[i, matrix.GetLength(1) - 1];
            }
            return Array;
        }
    }
}
