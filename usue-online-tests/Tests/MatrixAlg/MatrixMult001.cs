using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using Test_Wrapper;

namespace usue_online_tests.Tests.List
{
    public class MatrixMult001 : ITestCreator, ITest, ITimeLimit, IHidden
    {
        public int TestID { get; set; }
        public string Name { get; } = "Вычисление произведение матриц 001";
        public string Description { get; } = "По строчкам и столбцам";

        public class Data
        {
            public int[,] MatrixA = new int[2, 2];
            public int[,] MatrixB = new int[2, 2];
            public int[,] MatrixC = new int[2, 2];
            public int[,] Answer = new int[2, 2];

            public Data(Random random)
            {
                CreateMatrices(random);
            }

            private void CreateMatrices(Random random)
            {
                for (int i = 0; i < 2; i++)
                {
                    for (int j = 0; j < 2; j++)
                    {
                        MatrixA[i, j] = random.Next(-10, 11);
                        MatrixB[i, j] = random.Next(-10, 11);
                    }
                }

                MatrixC[0, 0] = MatrixA[0, 0] * MatrixB[0, 0] + MatrixA[0, 1] * MatrixB[1, 0];
                MatrixC[0, 1] = MatrixA[0, 0] * MatrixB[0, 1] + MatrixA[0, 1] * MatrixB[1, 1];
                MatrixC[1, 0] = MatrixA[1, 0] * MatrixB[0, 0] + MatrixA[1, 1] * MatrixB[1, 0];
                MatrixC[1, 1] = MatrixA[1, 0] * MatrixB[0, 1] + MatrixA[1, 1] * MatrixB[1, 1];

                for (int i = 0; i < 2; i++)
                {
                    for (int j = 0; j < 2; j++)
                    {
                        Answer[i, j] = MatrixC[i, j];
                    }
                }
            }
        }

        public ITest CreateTest(int randomSeed)
        {
            ITest result = new MatrixMult001();

            Random random = new Random(randomSeed);
            Data data = new Data(random);

            result.Text =
            $"\\(\\left(\\begin{{array}}{{cc}} {data.MatrixA[0, 0]} & {data.MatrixA[0, 1]}\\\\ {data.MatrixA[1, 0]} & {data.MatrixA[1, 1]}\\\\ \\end{{array}}\\right)\\) * " +
            $"\\(\\left(\\begin{{array}}{{cc}} {data.MatrixB[0, 0]} & {data.MatrixB[0, 1]}\\\\ {data.MatrixB[1, 0]} & {data.MatrixB[1, 1]}\\\\ \\end{{array}}\\right)\\) = " +
            $"\\(\\left(\\begin{{array}}{{cc}} <ans[{0},{0}]:5>     & <ans[{0},{1}]:5>    \\\\ <ans[{1},{0}]:5>     & <ans[{1},{1}]:5>    \\\\ \\end{{array}}\\right)\\)\r\n";
            return result;
        }

        public int CheckAnswer(int randomSeed, Dictionary<string, string> answers)
        {
            int total = 0;

            Random random = new Random(randomSeed);
            Data data = new Data(random);

            try
            {
                for (int i = 0; i < 2; i++)
                {
                    for (int j = 0; j < 2; j++)
                    {
                        if (answers[$"ans[{i},{j}]"] == data.Answer[i, j].ToString())
                            total++;
                    }
                }
            }
            catch
            {
                //ignored
            }

            return total;
        }

        public string Text { get; set; }
        public string[] CheckBoxes { get; set; }
        public List<MemoryStream> Pictures { get; set; }
        public int TimeLimitSeconds { get; set; } = 0;
        public bool IsHidden { get; set; } = false;
    }
}

