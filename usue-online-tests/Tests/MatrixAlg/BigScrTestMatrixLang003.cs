using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using Test_Wrapper;

namespace UserTest
{
    public class BigScrTestMatrixLang003 : ITestCreator, ITest, ITimeLimit, IHidden, ITestGroup
    {
        public int TestID { get; set; }
        public string GroupName { get; set; }= "Matrix Algebra";
        
        public string Name { get; } = "Перевод с языка элементов на язык матричных операций в целом: произведение 03";
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
            public string IndexA = "";
            public string IndexB = "";
            public string IndexC = "";
            public string ElemMatrEqA = "";
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
                int Vvv = random.Next(0, 7);
                switch ( Vvv )
                {
                   case 0: 
                           IndexA = string.Concat(MCa,MCc);
                           IndexB = string.Concat(MCc,MCb);
                           ElemMatrEqA = $"{ElemMatrC}_{{{IndexC}}}=\\sum\\limits_{{{MCc}=1}}^{3}{ElemMatrA}_{{{IndexA}}}{ElemMatrB}_{{{IndexB}}}";
                           answA = $"{MatrA}*{MatrB}";
                           break;
                   case 1: 
                           IndexA = string.Concat(MCc,MCa);
                           IndexB = string.Concat(MCc,MCb);
                           ElemMatrEqA = $"{ElemMatrC}_{{{IndexC}}}=\\sum\\limits_{{{MCc}=1}}^{3}{ElemMatrA}_{{{IndexA}}}{ElemMatrB}_{{{IndexB}}}";
                           answA = $"{MatrA}T*{MatrB}";
                           break;
                   case 2: 
                           IndexA = string.Concat(MCa,MCc);
                           IndexB = string.Concat(MCb,MCc);
                           ElemMatrEqA = $"{ElemMatrC}_{{{IndexC}}}=\\sum\\limits_{{{MCc}=1}}^{3}{ElemMatrA}_{{{IndexA}}}{ElemMatrB}_{{{IndexB}}}";
                           answA = $"{MatrA}*{MatrB}T";
                           break;
                   case 3: 
                           IndexA = string.Concat(MCc,MCa);
                           IndexB = string.Concat(MCb,MCc);
                           ElemMatrEqA = $"{ElemMatrC}_{{{IndexC}}}=\\sum\\limits_{{{MCc}=1}}^{3}{ElemMatrA}_{{{IndexA}}}{ElemMatrB}_{{{IndexB}}}";
                           answA = $"{MatrA}T*{MatrB}T";
                           break;
                   case 4: 
                           IndexA = string.Concat(MCb,MCc);
                           IndexB = string.Concat(MCc,MCa);
                           ElemMatrEqA = $"{ElemMatrC}_{{{IndexC}}}=\\sum\\limits_{{{MCc}=1}}^{3}{ElemMatrA}_{{{IndexA}}}{ElemMatrB}_{{{IndexB}}}";
                           answA = $"{MatrB}T*{MatrA}T";
                           break;
                   case 5: 
                           IndexA = string.Concat(MCc,MCb);
                           IndexB = string.Concat(MCc,MCa);
                           ElemMatrEqA = $"{ElemMatrC}_{{{IndexC}}}=\\sum\\limits_{{{MCc}=1}}^{3}{ElemMatrA}_{{{IndexA}}}{ElemMatrB}_{{{IndexB}}}";
                           answA = $"{MatrB}T*{MatrA}";
                           break;
                   case 6: 
                           IndexA = string.Concat(MCb,MCc);
                           IndexB = string.Concat(MCa,MCc);
                           ElemMatrEqA = $"{ElemMatrC}_{{{IndexC}}}=\\sum\\limits_{{{MCc}=1}}^{3}{ElemMatrA}_{{{IndexA}}}{ElemMatrB}_{{{IndexB}}}";
                           answA = $"{MatrB}*{MatrA}T";
                           break;
                   case 7: 
                           IndexA = string.Concat(MCc,MCb);
                           IndexB = string.Concat(MCa,MCc);
                           ElemMatrEqA = $"{ElemMatrC}_{{{IndexC}}}=\\sum\\limits_{{{MCc}=1}}^{3}{ElemMatrA}_{{{IndexA}}}{ElemMatrB}_{{{IndexB}}}";
                           answA = $"{MatrB}*{MatrA}";
                           break;
                }
            }
        }
        public ITest CreateTest(int randomSeed)
        {
            ITest result = new BigScrTestMatrixLang003();

            Random random = new Random(randomSeed);
            Data data = new Data(random);
                        
            //int num = random.Next(18);

            result.Text = 
                //$"{data.LetterR}" +
                $"Представьте матрицу \\({data.MatrC}\\) с элементами \\({data.ElemMatrEqA}\\) " +
                $" в виде результата применения матричных операций к матрицами " +
                $"\\({data.MatrA}=\\left({data.ElemMatrA}_{{ij}}\\right),\\quad\\) " +
                $"\\( {data.MatrB}=\\left({data.ElemMatrB}_{{ij}}\\right),\\quad \\) " +
                $" (результат транспонирования матрицы \\(Z\\) введите как \\(ZT\\), скобки не используйте, " +
                $"произведение матрицы \\(Z\\), например, на \\(S\\) записывается как \\(Z*S\\)):\r\n " +
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
        public List<MemoryStream> Pictures { get; set; }
        public int TimeLimitSeconds { get; set; } = 120;
        public bool IsHidden { get; set; } = false;
    }
}
