using System;
using System.Collections.Generic;
using System.Drawing;
using Test_Wrapper;

namespace UserTest
{
    public class BigScrTestMatrixLang002 : ITestCreator, ITest, ITimeLimit, IHidden, ITestGroup
    {
        public int TestID { get; set; }
        public string GroupName { get; set; }= "Matrix Algebra";
        
        public string Name { get; } = "Перевод с языка матричных операций в целом на язык элементов: сумма 02";
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
            public string IndexA = "";
            public string IndexB = "";
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
                           IndexA = string.Concat(MCa,MCb);
                           IndexB = string.Concat(MCa,MCb);
                           MatrEqA = $"{MatrC}={alpha}{MatrA}+{beta}{MatrB}";
                           answA = $"{alpha}*{ElemMatrA}{IndexA}+{beta}*{ElemMatrB}{IndexB}";
                           break;
                   case 1: 
                           IndexA = string.Concat(MCb,MCa);
                           IndexB = string.Concat(MCa,MCb);
                           MatrEqA = $"{MatrC}={alpha}{MatrA}^T+{beta}{MatrB}";
                           answA = $"{alpha}*{ElemMatrA}{IndexA}+{beta}*{ElemMatrB}{IndexB}";
                           break;
                   case 2: 
                           IndexA = string.Concat(MCa,MCb);
                           IndexB = string.Concat(MCb,MCa);
                           MatrEqA = $"{MatrC}={alpha}{MatrA}+{beta}{MatrB}^T";
                           answA = $"{alpha}*{ElemMatrA}{IndexA}+{beta}*{ElemMatrB}{IndexB}";
                           break;
                   case 3: 
                           IndexA = string.Concat(MCb,MCa);
                           IndexB = string.Concat(MCb,MCa);
                           MatrEqA = $"{MatrC}={alpha}{MatrA}^T+{beta}{MatrB}^T";
                           answA = $"{alpha}*{ElemMatrA}{IndexA}+{beta}*{ElemMatrB}{IndexB}";
                           break;
                }
            }
        }
        public ITest CreateTest(int randomSeed)
        {
            ITest result = new BigScrTestMatrixLang002();

            Random random = new Random(randomSeed);
            Data data = new Data(random);
                        
            //int num = random.Next(18);

            result.Text = 
                //$"{data.LetterR}" +
                $"Представьте формулу для элемента \\({data.ElemMatrC}_{{{data.IndexC}}}\\)  матрицы \\({data.MatrEqA}\\) " +
                $" в виде результата применения матричных операций к матрицами " +
                $"\\({data.MatrA}=({data.ElemMatrA}_{{{data.IndexC}}}), {data.MatrB}=({data.ElemMatrB}_{{{data.IndexC}}})\\) " +
                $" (элемент \\({data.ElemMatrA}_{{ij}}\\) запишите как \\({data.ElemMatrA}ij\\), где все буквы строчные, и т.п., "+
                $" не пропускайте знак * операции умножения, скобки не используйте, не переставляйте множители и слагаемые),\r\n " +
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
        public List<Image> Pictures { get; set; }
        public int TimeLimitSeconds { get; set; } = 120;
        public bool IsHidden { get; set; } = false;
    }
}
