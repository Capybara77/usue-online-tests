using System;
using System.Collections.Generic;
using System.Drawing;
using Test_Wrapper;

namespace UserTest
{
    public class BigScrTestMatrixLang001 : ITestCreator, ITest, ITimeLimit, IHidden, ITestGroup
    {
        public int TestID { get; set; }
        public string GroupName { get; set; }= "Matrix Algebra";
                public string Name { get; } = "Перевод с языка элементов на язык матричных операций в целом: сумма 01";
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
                           answA = $"{alpha}*{MatrA}+{beta}*{MatrB}";
                           break;
                   case 1: 
                           IndexA = string.Concat(MCb,MCa);
                           IndexB = string.Concat(MCa,MCb);
                           answA = $"{alpha}*{MatrA}T+{beta}*{MatrB}";
                           break;
                   case 2: 
                           IndexA = string.Concat(MCa,MCb);
                           IndexB = string.Concat(MCb,MCa);
                           answA = $"{alpha}*{MatrA}+{beta}*{MatrB}T";
                           break;
                   case 3: 
                           IndexA = string.Concat(MCb,MCa);
                           IndexB = string.Concat(MCb,MCa);
                           answA = $"{alpha}*{MatrA}T+{beta}*{MatrB}T";
                           break;
                }
            }
        }
        public ITest CreateTest(int randomSeed)
        {
            ITest result = new BigScrTestMatrixLang001();

            Random random = new Random(randomSeed);
            Data data = new Data(random);
                        
            //int num = random.Next(18);

            result.Text = 
                //$"{data.LetterR}" +
                $"Представьте матрицу \\({data.MatrC}\\) с элементами " +
                $" \\({data.ElemMatrC}_{{{data.IndexC}}}={data.alpha}{data.ElemMatrA}_{{{data.IndexA}}}+{data.beta}{data.ElemMatrB}_{{{data.IndexB}}}\\)" +
                $" в виде результата применения матричных операций к матрицам " +
                $"\\({data.MatrA}=\\left({data.ElemMatrA}_{{{data.IndexC}}}\\right),\\quad\\) " +
                $"\\( {data.MatrB}=\\left({data.ElemMatrB}_{{{data.IndexC}}}\\right),\\quad \\) " +
                $" (результат транспонирования матрицы \\(Z\\) введите как \\(ZT\\), скобки не используйте, " +
                $"произведение матрицы \\(Z\\), например, на 7 записывается как \\(7*Z\\), не переставляйте множители и слагаемые):\r\n " +
                $" \\({data.MatrC}=\\)\\(<ans:20>\\)\r\n"; //+
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
