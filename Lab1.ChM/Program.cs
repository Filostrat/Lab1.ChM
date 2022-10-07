using System;

namespace Lab1.ChM
{
    class Program
    {
        static void Main(string[] args)
        {
            var MyMatrix = Addmatrix();
            PrintMatrix(MyMatrix);
            var CopyMatrix = DeepCopy(MyMatrix);
            Step1(CopyMatrix,MyMatrix);
            Step2(CopyMatrix, MyMatrix);
            Step3(CopyMatrix);
            PrintMatrix(CopyMatrix);
            var temp = Step4(CopyMatrix, MyMatrix);
            PrintArray (Step5(CopyMatrix, temp));           
        }
        

        public static void Step1(double[,] matrix,double[,] CopyMatrix)
        {
            matrix[0, 0] = Math.Sqrt(CopyMatrix[0, 0]);
            for (int j = 1; j < matrix.GetLength(1); j++)
            {
                matrix[0, j] = CopyMatrix[0,j]/Math.Sqrt(CopyMatrix[0, 0]);
            }
        }

        public static void Step2(double[,] matrix, double[,] CopyMatrix)
        {
            double temp = 0;
            for (int i = 1; i < matrix.GetLength(0); i++)
            {
                temp = 0;
                for (int k = 0; k < i; k++)
                {
                    temp += Math.Pow(matrix[k, i], 2);
                }
                matrix[i, i] = Math.Sqrt(CopyMatrix[i, i] - temp);

                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    if (i < j)
                    {
                        temp = 0;
                        for (int k = 0; k < i; k++)
                        {
                            temp += matrix[k, i] * matrix[k, j];
                        }
                        matrix[i, j] = (CopyMatrix[i, j] - temp) / matrix[i, i];
                    }
                }
            }
        }

        public static void Step3(double[,] matrix)
        {
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    if (i>j)
                    {
                        matrix[i, j] = 0;
                    }                       
                }
            }
        }

        public static double[] Step4(double[,] matrix, double[,] CopyMatrix)
        {
            double[] temp = new double[matrix.GetLength(0)];
            temp[0] = CopyMatrix[0, CopyMatrix.GetLength(1)-1]/matrix[0,0];

            for (int i = 1; i < matrix.GetLength(0); i++)
            {
                var sum = 0.0;
                for (int k = 0; k < i; k++)
                {
                    sum += matrix[k, i] *temp[k];
                }
                temp[i] = (CopyMatrix[i, CopyMatrix.GetLength(1) - 1] - sum) / matrix[i, i];
            }
            return temp;
        }

        public static double[] Step5(double[,] matrix,double [] tempY)
        {
            double[] tempX = new double[matrix.GetLength(0)];
            tempX[^1] = tempY[^1] / matrix[matrix.GetLength(0)-1, matrix.GetLength(1)-1];

            for (int i = matrix.GetLength(0)-1; i != -1; i--)
            {
                var sum = 0.0;
                for (int k = i+1; k < matrix.GetLength(0); k++)
                {
                    sum += matrix[i, k] * tempX[k];
                }
                tempX[i] = (tempY[i] - sum) / matrix[i, i];
            }
            return tempX;
        }
        public static void PrintArray(double[] temp)
        {
            Console.Write("|");
            for (int i = 0; i < temp.Length; i++)
            {
                temp[i] = Math.Round(temp[i],4);
                Console.Write((i < temp.Length-1) ? $" {temp[i],4} " : $" | {temp[i],4} ");

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
                    Console.Write((j < matrix.GetLength(1)-1) ? $" {matrix[i, j],6} " : $" | {matrix[i, j],6} ");
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
        
        public static double[,] DeepCopy(double[,] matrix)
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
    }
}
