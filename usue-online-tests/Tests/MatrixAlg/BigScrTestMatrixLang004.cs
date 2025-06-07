using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using Test_Wrapper;

namespace UserTest
{
    public class BigScrTestMatrixLang004 : ITestCreator, ITest, ITimeLimit, IHidden, ITestGroup
    {
        public int TestID { get; set; }
        public string GroupName { get; set; }= "Matrix Algebra";
        
        public string Name { get; } = "Перевод с языка элементов на язык матричных операций в целом: произведение 04";
        public string Description { get; } = "Матричные операции";

        public class Data
        {

            public string LetterR = "ABCDFGHKMPRSUVWXYZ";
            public string answA = "0";
            public string MatrA = "";
            public string MatrB = "";
            public string MatrC = "";
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
                CreateVars(random);
                CreateChoices(random);
            }
            public void CreateVars(Random random)
            {
                //Random rndA = new Random(randomSeed);
                int letterCount = LetterR.Length;
                MatrA = $"{LetterR[random.Next(letterCount)]}";
                ElemMatrA = MatrA.ToLower();
                LetterR = LetterR.Replace(MatrA, "");
                letterCount = LetterR.Length;
                MatrB = $"{LetterR[random.Next(letterCount)]}";
                ElemMatrB = MatrB.ToLower();
                LetterR = LetterR.Replace(MatrB, "");
                letterCount = LetterR.Length;
                MatrC = $"{LetterR[random.Next(letterCount)]}";
                ElemMatrC = MatrC.ToLower();
                LetterR = LetterR.Replace(MatrC, "");
                MCa = $"{LetterR[random.Next(letterCount)]}";
                LetterR = LetterR.Replace(MCa, "");
                MCa = MCa.ToLower();
                letterCount = LetterR.Length;
                MCb = $"{LetterR[random.Next(letterCount)]}";
                LetterR = LetterR.Replace(MCb, "");
                MCb = MCb.ToLower();
                letterCount = LetterR.Length;
                MCc = $"{LetterR[random.Next(letterCount)]}";
                LetterR = LetterR.Replace(MCc, "");
                MCc = MCc.ToLower();
                alpha = random.Next(2, 9);
                beta = random.Next(2, 8);
                if ( beta == alpha ) beta++;
            }
   
            public void CreateChoices(Random random)
            {
                IndexC = string.Concat(MCa,MCb);
                int Vvv = random.Next(0, 3);
                switch ( Vvv )
                {
                   case 0: 
                           IndAa = string.Concat(MCa,1);
                           IndAb = string.Concat(MCa,2);
                           IndAc = string.Concat(MCa,3);
                           IndBa = string.Concat(1,MCb);
                           IndBb = string.Concat(2,MCb);
                           IndBc = string.Concat(3,MCb);
                           answA = $"{ElemMatrA}{IndAa}*{ElemMatrB}{IndBa}+{ElemMatrA}{IndAb}*{ElemMatrB}{IndBb}+{ElemMatrA}{IndAc}*{ElemMatrB}{IndBc}";
                           MatrEqA = $"{MatrC}={MatrA}\\cdot {MatrB}";
                           break;
                   case 1: 
                           IndAa = string.Concat(1,MCa);
                           IndAb = string.Concat(2,MCa);
                           IndAc = string.Concat(3,MCa);
                           IndBa = string.Concat(1,MCb);
                           IndBb = string.Concat(2,MCb);
                           IndBc = string.Concat(3,MCb);
                           answA = $"{ElemMatrA}{IndAa}*{ElemMatrB}{IndBa}+{ElemMatrA}{IndAb}*{ElemMatrB}{IndBb}+{ElemMatrA}{IndAc}*{ElemMatrB}{IndBc}";
                           MatrEqA = $"{MatrC}={MatrA}^T\\cdot {MatrB}";
                           break;
                   case 2: 
                           IndAa = string.Concat(MCa,1);
                           IndAb = string.Concat(MCa,2);
                           IndAc = string.Concat(MCa,3);
                           IndBa = string.Concat(MCb,1);
                           IndBb = string.Concat(MCb,2);
                           IndBc = string.Concat(MCb,3);
                           answA = $"{ElemMatrA}{IndAa}*{ElemMatrB}{IndBa}+{ElemMatrA}{IndAb}*{ElemMatrB}{IndBb}+{ElemMatrA}{IndAc}*{ElemMatrB}{IndBc}";
                           MatrEqA = $"{MatrC}={MatrA}\\cdot {MatrB}^T";
                           break;
                   case 3: 
                           IndAa = string.Concat(1,MCa);
                           IndAb = string.Concat(2,MCa);
                           IndAc = string.Concat(3,MCa);
                           IndBa = string.Concat(MCb,1);
                           IndBb = string.Concat(MCb,2);
                           IndBc = string.Concat(MCb,3);
                           answA = $"{ElemMatrA}{IndAa}*{ElemMatrB}{IndBa}+{ElemMatrA}{IndAb}*{ElemMatrB}{IndBb}+{ElemMatrA}{IndAc}*{ElemMatrB}{IndBc}";
                           MatrEqA = $"{MatrC}={MatrA}^T\\cdot {MatrB}^T";
                           break;
                }
            }
        }
        public ITest CreateTest(int randomSeed)
        {
            ITest result = new BigScrTestMatrixLang004();

            Random random = new Random(randomSeed);
            Data data = new Data(random);
                        
            //int num = random.Next(18);

            result.Text = 
                //$"{data.LetterR}" +
                $"Представьте формулу для элемента матрицы \\({data.MatrEqA}\\) в виде результата" +
                $" применения матричных операций к матрицами " +
                $"\\({data.MatrA}=\\left({data.MatrA}_{{ij}}\\right),\\quad\\) " +
                $"\\({data.MatrB}=\\left({data.MatrB}_{{ij}}\\right),\\quad \\) " +
                $" (элемент \\({data.ElemMatrC}_{{ij}}\\) запишите как \\({data.ElemMatrC}ij\\), где все буквы строчные, и т.п., "+
                //$" результат транспонирования матрицы \\(Z\\) введите как \\(ZT\\), " +
                $"не пропускайте знак * операции умножения, скобки не используйте, не переставляйте слагаемые, " +
                $" первый множитель относится к первому индексу, второй - ко второму), \r\n" +
                $" \\({data.ElemMatrC}_{{{data.IndexC}}}=\\)\\(<ans:20>\\)\r\n"; //+
                //$" {data.answA}";

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
                //for (int i = 0; i < 8; i++)
                {
                    // if (answers["ans" + i]  == ans[i].ToString()) total++;
                    //if(answers["ans"] == data.ans[num][0]) total++;
                    if(answers["ans"] == data.answA) total++;
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
        public List<MemoryStream> Pictures { get; set; }
        public int TimeLimitSeconds { get; set; } = 120;
        public bool IsHidden { get; set; } = false;
    }
}
