using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace chmaGauss
{
    class Program
    {
        // Матрица A
        static double[,] A = new double[4, 4];

        // Столбец свободных членов y
        static double[] y = new double[4];

        private static int findMaxInColumn(int colIndex, double accuracy) {
            double max = Math.Abs(A[colIndex, colIndex]);
            int rowIndex = colIndex;
            for (int i = colIndex + 1; i < 4; i++) {
                if (Math.Abs(A[i, colIndex]) > max)
                {
                    max = Math.Abs(A[i, colIndex]);
                    rowIndex = i;
                }
            }
            if (max < accuracy) return 0;
            return rowIndex;
        }

        private static void swapRows(int row1_index, int row2_index) {

            if (row1_index != row2_index)
            {
                for (int j = row1_index; j < 4; j++)
                {
                    var tmp = A[row1_index, j];
                    A[row1_index, j] = A[row2_index, j];
                    A[row2_index, j] = tmp;
                }
                var tmpY = y[row1_index];
                y[row1_index] = y[row2_index];
                y[row2_index] = tmpY;
            }
        }
        private static void makeDownZeroes(int columnIndex) {
            for (int i = columnIndex + 1; i < 4; i++) {
                double multiplier = A[i, columnIndex] / A[columnIndex, columnIndex];
                for (int j = columnIndex; j < 4; j++) {
                        A[i, j] -= multiplier * A[columnIndex, j];
                      }
                y[i] -= multiplier * y[columnIndex];
            }
            
        }
        private static double[] reverseGauss() {
            double[] x = new double[4];
            x[3] = y[3] / A[3, 3];
            for (int i = 2; i >= 0; i--) {
                var rightPart = y[i];
                for (int j = i+1; j < 4; j++)
                {
                    rightPart -= A[i, j] * x[j];
                }
                x[i] = rightPart / A[i, i];
            }
            return x;
        }

        private static double[] Gauss(double accuracy) {
            int colIndex = 0;
            double[] result = new double[4];
            while (colIndex < 4) {
                if (colIndex == 3) {
                   result = reverseGauss();
                   break;
                }
                int rowIndex = findMaxInColumn(colIndex, accuracy);
                swapRows(colIndex, rowIndex);
                makeDownZeroes(colIndex);
                colIndex++;
            }
            return result;
        }
        

        static void Main(string[] args)
        {
            //Заполняем значениями матрицу A
            A[0, 0] = 0.63; A[0, 1] = 1.00; A[0, 2] = 0.71; A[0, 3] = 0.34;
            A[1, 0] = 1.17; A[1, 1] = 0.18; A[1, 2] = -0.65; A[1, 3] = 0.71;
            A[2, 0] = 2.71; A[2, 1] = -0.75; A[2, 2] = 1.17; A[2, 3] = -2.35;
            A[3, 0] = 3.58; A[3, 1] = 0.21; A[3, 2] = -3.45; A[3, 3] = -1.18;
            // //Заполняем значениями вектор свободных членов y
            y[0] = 2.08; y[1] = 0.17; y[2] = 1.28; y[3] = 0.05;
            var x = Gauss(0.001);
            //Выводим значения координат получившегося вектора
            for (int i = 0; i < 4; i++)
            {
                Console.WriteLine("x" + (i + 1) + " = " + x[i] + " ");
            }
            Console.ReadKey();
        }
    }
}

