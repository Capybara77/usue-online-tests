using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using Test_Wrapper;

namespace usue_online_tests.Tests.List
{
    public class MatrixMult002 : ITestCreator, ITest, ITimeLimit, IHidden
    {
        private Random random;

        public int TestID { get; set; }
        public string Name { get; } = "Вычисление произведение матриц 002";
        public string Description { get; } = "По строчкам и столбцам";
        
        public MatrixMult002()
        {
            random = new Random();
        }

        public ITest CreateTest(int randomSeed)
        {
            Random random = new Random(randomSeed);
            MatrixMult002 result = new MatrixMult002();

            int[,] matrixA = new int[2, 2];
            int[,] matrixB = new int[2, 2];
            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < 2; j++)
                {
                    matrixA[i, j] = random.Next(-10, 10);
                    matrixB[i, j] = random.Next(-10, 10);
                }
            }

            int[,] resultMatrix = new int[2, 2];
            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < 2; j++)
                {
                    for (int k = 0; k < 2; k++)
                    {
                        resultMatrix[i, j] += matrixA[i, k] * matrixB[k, j];
                    }
                }
            }

            int hiddenRowA = random.Next(0, 2);
            int hiddenColA = random.Next(0, 2);
            int hiddenRowB = random.Next(0, 2);
            int hiddenColB = random.Next(0, 2);

            if (hiddenColA == 0 && hiddenRowB == 0)
                hiddenRowB = 1;
            else if (hiddenColA == 1 && hiddenRowB == 1)
                hiddenRowB = 0;
            else if (hiddenColB == 0 && hiddenRowA == 0)
                hiddenRowA = 1;
            else if (hiddenColB == 1 && hiddenRowA == 1)
                hiddenRowA = 0;

            result.Text = $"\\( \\begin{{pmatrix}}{(hiddenRowA == 0 && hiddenColA == 0 ? "<i1>" : matrixA[0, 0].ToString())} & {(hiddenRowA == 0 && hiddenColA == 1 ? "<i2>" : matrixA[0, 1].ToString())}\\\\{(hiddenRowA == 1 && hiddenColA == 0 ? "<i3>" : matrixA[1, 0].ToString())} & {(hiddenRowA == 1 && hiddenColA == 1 ? "<i4>" : matrixA[1, 1].ToString())}\\end{{pmatrix}} * " +
                          $"\\begin{{pmatrix}}{(hiddenRowB == 0 && hiddenColB == 0 ? "<j1>" : matrixB[0, 0].ToString())} & {(hiddenRowB == 0 && hiddenColB == 1 ? "<j2>" : matrixB[0, 1].ToString())}\\\\{(hiddenRowB == 1 && hiddenColB == 0 ? "<j3>" : matrixB[1, 0].ToString())} & {(hiddenRowB == 1 && hiddenColB == 1 ? "<j4>" : matrixB[1, 1].ToString())}\\end{{pmatrix}} = " +
                          $"\\begin{{pmatrix}} {resultMatrix[0, 0]} & {resultMatrix[0, 1]} \\\\ {resultMatrix[1, 0]} & {resultMatrix[1, 1]} \\end{{pmatrix}} \\)";

            return result;
        }


        public int CheckAnswer(int randomSeed, Dictionary<string, string> answers)
        {
            Random random = new Random(randomSeed);
            int total = 0;

            int[,] matrixA = new int[2, 2];
            int[,] matrixB = new int[2, 2];

            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < 2; j++)
                {
                    matrixA[i, j] = random.Next(-10, 10);
                    matrixB[i, j] = random.Next(-10, 10);
                }
            }

            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < 2; j++)
                {
                    string key = $"i{(i * 2 + j) + 1}";
                    if (answers.TryGetValue(key, out string userAnswer))
                    {
                        if (int.TryParse(userAnswer, out int parsedUserAnswer))
                        {
                            if (parsedUserAnswer == matrixA[i, j])
                                total++;
                        }
                    }
                }
            }

            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < 2; j++)
                {
                    string key = $"j{(i * 2 + j) + 1}";
                    if (answers.TryGetValue(key, out string userAnswer))
                    {
                        if (int.TryParse(userAnswer, out int parsedUserAnswer))
                        {
                            if (parsedUserAnswer == matrixB[i, j])
                                total++;
                        }
                    }
                }
            }

            return total;
        }

        public string Text { get; set; }
        public string[] CheckBoxes { get; set; }
        public List<Image> Pictures { get; set; }
        public int TimeLimitSeconds { get; set; } = 0;
        public bool IsHidden { get; set; } = false;
    }
}