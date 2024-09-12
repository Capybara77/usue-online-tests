using System;
using System.Collections.Generic;
using System.Drawing;
using Test_Wrapper;

namespace UserTest
{
    public class BigScrTestMatrixDef001 : ITestCreator, ITest, ITimeLimit, IHidden, ITestGroup
    {
        public int TestID { get; set; }
        public string GroupName { get; set; }= "Matrix Algebra";
        public string Name { get; } = "Определение матрицы 001";
        public string Description { get; } = "Матричные операции";

        public class Data
        {

            public string LetterR = "ABCDFGHKMPRSUVWXYZ";
            public int[] answA = new int[6];
            public int[] MatrA = new int[6];
            public int[] MatrB = new int[6];
            public int[] MatrC = new int[6];
            public int[] MatrD = new int[6];
            public int iA, jA, iB, jB, iCx, jCx, iCy, jCy, Vvv, Vvy;
            public string ElemMatrA = "";
            public string ElemMatrB = "";
            public string ElemMatrC = "";
            public string MCa = "";
            public string MCb = "";
            public string MCc = "";
            public string IndAa = "";
            public string IndAb = "";
            public string IndAc = "";
            public string IndBa = "";
            public string IndBb = "";
            public string IndBc = "";
            public string IndexC = "";
            public string MatrEqA = "";
            public int alpha;
            public int beta;

            public Data(Random random)
            {
                CreateMatrixes(random);
            }
            public void CreateMatrixes(Random random)
            {
                Vvv = random.Next(18);
                MCa = LetterR[Vvv].ToString();
                MCb = MCa.ToLower();
                Vvy = 20;
                int[] Numbers = new int[2*Vvy];
                for (int i = 0; i < Vvy; i++)
                {
                    Numbers[2*i] = -i-1;
                    Numbers[2*i+1] = i+1;
                }
                Vvy = Vvy - 1;
                Vvv = random.Next(Vvy-1);
                MatrA[0] = Numbers[Vvv];
                Numbers[Vvv] = Numbers[Vvy];
                Vvy = Vvy - 1;
                Vvv = random.Next(Vvy-1);
                MatrA[1] = Numbers[Vvv];
                Numbers[Vvv] = Numbers[Vvy];
                Vvy = Vvy - 1;
                Vvv = random.Next(Vvy-1);
                MatrA[2] = Numbers[Vvv];
                Numbers[Vvv] = Numbers[Vvy];
                Vvy = Vvy - 1;
                Vvv = random.Next(Vvy-1);
                MatrA[3] = Numbers[Vvv];
                Numbers[Vvv] = Numbers[Vvy];
                Vvy = Vvy - 1;
                Vvv = random.Next(Vvy-1);
                MatrA[4] = Numbers[Vvv];
                Numbers[Vvv] = Numbers[Vvy];
                Vvy = Vvy - 1;
                Vvv = random.Next(Vvy-1);
                MatrA[5] = Numbers[Vvv];
                MatrB[0] = random.Next(2);
                MatrB[1] = 1-MatrB[0];
                MatrC[0] = random.Next(3);
                MatrC[1] = random.Next(2);
                if (MatrC[0] == MatrC[1])
                {
                    MatrC[1] = MatrC[1]+1;
                    if (MatrC[1] > 2)
                    {
                        MatrC[0] = MatrC[0]-1;
                        MatrC[1] = MatrC[1]-1;
                    }
                }
                MatrC[2] = 0;
                if (MatrC[2] == MatrC[0])
                {
                    MatrC[2] = MatrC[2]+1;
                }
                if (MatrC[2] == MatrC[1])
                {
                    MatrC[2] = MatrC[2]+1;
                }
                if (MatrC[2] == MatrC[0])
                {
                    MatrC[2] = MatrC[2]+1;
                }
                MatrD[0] = random.Next(3);
                MatrD[1] = random.Next(2);
                if (MatrD[0] == MatrD[1])
                {
                    MatrD[1] = MatrD[1]+1;
                    if (MatrD[1] > 2)
                    {
                        MatrD[0] = MatrD[0]-1;
                        MatrD[1] = MatrD[1]-1;
                    }
                }
                MatrD[2] = 0;
                if (MatrD[2] == MatrD[0])
                {
                    MatrD[2] = MatrD[2]+1;
                }
                if (MatrD[2] == MatrD[1])
                {
                    MatrD[2] = MatrD[2]+1;
                }
                if (MatrD[2] == MatrD[0])
                {
                    MatrD[2] = MatrD[2]+1;
                }
                for (int i = 0; i < 6; i++)
                {
                    answA[i] = MatrA[i];
                }
            }
        }
        public ITest CreateTest(int randomSeed)
        {
            ITest result = new BigScrTestMatrixDef001();

            Random random = new Random(randomSeed);
            Data data = new Data(random);
                        
            //int num = random.Next(18);

            result.Text =
                $" Пусть \\({data.MCb}_{{{data.MatrB[0]+1},{data.MatrC[0]+1}}}= {data.MatrA[3*data.MatrB[0]+data.MatrC[0]]},\\quad\\) " +
                $"\\({data.MCb}_{{{data.MatrB[0]+1},{data.MatrC[1]+1}}}= {data.MatrA[3*data.MatrB[0]+data.MatrC[1]]},\\quad\\) " +
                $"\\({data.MCb}_{{{data.MatrB[0]+1},{data.MatrC[2]+1}}}= {data.MatrA[3*data.MatrB[0]+data.MatrC[2]]},\\quad\\) " +
                $"\\({data.MCb}_{{{data.MatrB[1]+1},{data.MatrD[0]+1}}}= {data.MatrA[3*data.MatrB[1]+data.MatrD[0]]},\\quad\\) " +
                $"\\({data.MCb}_{{{data.MatrB[1]+1},{data.MatrD[1]+1}}}= {data.MatrA[3*data.MatrB[1]+data.MatrD[1]]},\\quad\\) " +
                $"\\({data.MCb}_{{{data.MatrB[1]+1},{data.MatrD[2]+1}}}= {data.MatrA[3*data.MatrB[1]+data.MatrD[2]]}.\\quad\\) Тогда\r\n" +
                $"\\({data.MCa}=\\left(\\begin{{array}}{{ccc}} <ans[0]:5> & <ans[1]:5> & <ans[2]:5>\\\\ " +
                $" <ans[3]:5> & <ans[4]:5> & <ans[5]:5>\\\\ \\end{{array}}\\right)\\).\r\n"; //+
                //$"\\(\\left(\\begin{{array}}{{ccc}} {data.MatrA[0]} & {data.MatrA[1]} & {data.MatrA[2]}\\\\ " +
                //$" {data.MatrA[3]} & {data.MatrA[4]} & {data.MatrA[5]}\\\\ \\end{{array}}\\right)\\)." +
                //$"\\(\\left(\\begin{{array}}{{ccc}} {data.answA[0]} & {data.answA[1]} & {data.answA[2]}\\\\ " +
                //$" {data.answA[3]} & {data.answA[4]} & {data.answA[5]}\\\\ \\end{{array}}\\right)\\).";
//
//
                ////$"{data.LetterR}" +
                //$"Представьте формулу для элемента матрицы \\({data.MatrEqA}\\) в виде результата" +
                //$" применения матричных операций к матрицами " +
                //$"\\({data.MatrA}=\\left({data.MatrA}_{{ij}}\\right),\\quad\\) " +
                //$"\\({data.MatrB}=\\left({data.MatrB}_{{ij}}\\right),\\quad \\) " +
                //$" (элемент \\({data.ElemMatrC}_{{ij}}\\) запишите как \\({data.ElemMatrC}ij\\), где все буквы строчные, и т.п., "+
                ////$" результат транспонирования матрицы \\(Z\\) введите как \\(ZT\\), " +
                //$"не пропускайте знак * операции умножения, скобки не используйте, не переставляйте слагаемые, " +
                //$" первый множитель относится к первому индексу, второй - ко второму), \r\n" +
                //$" \\({data.ElemMatrC}_{{{data.IndexC}}}=\\)\\(<ans:20>\\)\r\n"; //+
                ////$" {data.answA}";

            return result;
        }

        public int CheckAnswer(int randomSeed, Dictionary<string, string> answers)
        {
            int total = 0;

            Random random = new Random(randomSeed);
            Data data = new Data(random);
                        
            //int num = random.Next(8);


            //string[] ans = ans[num];
            try
            {
                for (int i = 0; i < 6; i++)
                {
                    // if (answers["ans" + i]  == ans[i].ToString()) total++;
                    //if(answers[ans] == data.ans[num][0]) total++;
                    if (answers["ans["+i+"]"] == data.answA[i].ToString()) total++;
                }
            }
            catch
            {
                // ignored
            }

            return total;
        }

        public string Text { get; set; }
        public string[] CheckBoxes { get; set; }
        public List<Image> Pictures { get; set; }
        public int TimeLimitSeconds { get; set; } = 120;
        public bool IsHidden { get; set; } = false;
    }
}
