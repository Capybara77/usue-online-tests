using System;
using System.Collections.Generic;
using System.Drawing;
using Test_Wrapper;

namespace UserTest
{
    public class BigScrTestSigmA04 : ITestCreator, ITest, ITimeLimit, IHidden, ITestGroup
    {
        public int TestID { get; set; }
        public string GroupName { get; set; }= "Sigma Sign";
        public string Name { get; } = "Двойной символ суммирования 04";
        public string Description { get; } = "Работа с символом суммирования";

        public class Data
        {

            public string LetterR = "ABCDFGHKMPRSUVWXYZ";
            public int[] answA = new int[12];
            public string IndexA = "";
            public string IndexB = "";
            public string IndexC = "";
            public string ElemA = "";
            public int nA, nB, nC;
            //public int MembNumbA, MembNumbB, MembNumbC;
            public string FormulaSigmA, FormulaSigmB;
            //public string MembFormTeXA, MembFormTeXB, MembFormTeXC, MembFormTeXn;
            //public int MembAaa, MembAab, MembAac, MembAba, MembAbb, MembAbc, MembAca, MembAcb, MembAcc;
            //public string MembFormCalcA, MembFormCalcB, MembFormCalcC, MembFormCalcn;
            //public int[] MatrB = new int[4];
            //public int[] MatrC = new int[4];
            public int aA, aB, aC, bA, bB, bC, Maa, Mab, Mac, Mba, Mbb, Mbc, Mca, Mcb, Mcc, Vvu, Vvv, Vvw;
            public string Map, Mbp, Mcp, Mqa, Mqb, Mqc;
            //public string ElemMatrA = "";
            //public string ElemMatrB = "";
            //public string ElemMatrC = "";
            //public string MCa = "";
            //public string MCb = "";
            //public string MCc = "";

            public Data(Random random)
            {
                CreateVars(random);
                //CreateMembers(random);
                //CreateChoices(random);
                //MembFormCalcA = MembFormTeXn.Replace("n", Convert.ToString(nA, 10));
                //MembFormCalcB = MembFormTeXn.Replace("n", Convert.ToString(nA + 1, 10));
                //MembFormCalcC = MembFormTeXn.Replace("n", Convert.ToString(nA + 2, 10));
            }
            public void CreateVars(Random random)
            {
                int letterCount = LetterR.Length;
                ElemA = $"{LetterR[random.Next(letterCount)]}";
                LetterR = LetterR.Replace(ElemA, "");
                ElemA = ElemA.ToLower();
                letterCount = LetterR.Length;
                IndexA = $"{LetterR[random.Next(letterCount)]}";
                LetterR = LetterR.Replace(IndexA, "");
                IndexA = IndexA.ToLower();
                //ElemB = $"{LetterR[random.Next(letterCount)]}";
                //LetterR = LetterR.Replace(ElemB, "");
                //ElemB = ElemB.ToLower();
                letterCount = LetterR.Length;
                IndexB = $"{LetterR[random.Next(letterCount)]}";
                LetterR = LetterR.Replace(IndexB, "");
                IndexB = IndexB.ToLower();
                aA = random.Next(1, 5);
                bA = random.Next(aA+1, 9);
                if (bA == aA) bA++;
                //aB = random.Next(2, 6);
                //bB = random.Next(2, 6);
                //if (bB == aB) bB++;
                FormulaSigmA = "\\sum\\limits_{" + IndexA+ "=" + Convert.ToString(aA,10) +"}^{" + Convert.ToString(aA+2,10) + "}" +
                               "\\sum\\limits_{" + IndexB+ "=" + Convert.ToString(bA,10) +"}^{" + Convert.ToString(bA+1,10) + "}" +
                               ElemA + "_{10" + IndexA + "+" + IndexB +"}=";
                FormulaSigmB = "\\sum\\limits_{" + IndexB+ "=" + Convert.ToString(bA,10) +"}^{" + Convert.ToString(bA+1,10) + "}" +
                               "\\sum\\limits_{" + IndexA+ "=" + Convert.ToString(aA,10) +"}^{" + Convert.ToString(aA+2,10) + "}" +
                               ElemA + "_{10" + IndexA + "+" + IndexB +"}=";
                answA[0] = (aA + 0)*10 + bA + 0; answA[1] = (aA + 0)*10 + bA + 1;
                answA[2] = (aA + 1)*10 + bA + 0; answA[3] = (aA + 1)*10 + bA + 1;
                answA[4] = (aA + 2)*10 + bA + 0; answA[5] = (aA + 2)*10 + bA + 1;
                answA[6] = (aA +0)*10 + bA + 0; answA[07] = (aA +1)*10 + bA + 0; answA[08] = (aA +2)*10 + bA + 0;
                answA[9] = (aA +0)*10 + bA + 1; answA[10] = (aA +1)*10 + bA + 1; answA[11] = (aA +2)*10 + bA + 1;
            }
        }
        public ITest CreateTest(int randomSeed)
        {
            ITest result = new BigScrTestSigmA04();

            Random random = new Random(randomSeed);
            Data data = new Data(random);

            result.Text =
                          //$"\\({data.FormulaSigmA}\\)" +
                          //$"\\({data.ElemA}_{{{data.answA[0]}}}+{data.ElemA}_{{{data.answA[1]}}}+{data.ElemA}_{{{data.answA[2]}}}+\\)" +
                          //$"\\({data.ElemA}_{{{data.answA[3]}}}+{data.ElemA}_{{{data.answA[4]}}}+{data.ElemA}_{{{data.answA[5]}}}.\\)\r\n" +
                          //$"\\({data.FormulaSigmB}\\)" +
                          //$"\\({data.ElemA}_{{{data.answA[6]}}}+{data.ElemA}_{{{data.answA[7]}}}+{data.ElemA}_{{{data.answA[8]}}}+\\)" +
                          //$"\\({data.ElemA}_{{{data.answA[9]}}}+{data.ElemA}_{{{data.answA[10]}}}+{data.ElemA}_{{{data.answA[11]}}}.\\)\r\n" +
                          $"\\({data.FormulaSigmA}\\)" +
                          $"\\({data.ElemA}_{{<ans[0]:5>}}+{data.ElemA}_{{<ans[1]:5>}}+{data.ElemA}_{{<ans[2]:5>}}+\\)" +
                          $"\\({data.ElemA}_{{<ans[3]:5>}}+{data.ElemA}_{{<ans[4]:5>}}+{data.ElemA}_{{<ans[5]:5>}},\\) \r\n" +
                          $"\\({data.FormulaSigmB}\\)" +
                          $"\\({data.ElemA}_{{<ans[6]:5>}}+{data.ElemA}_{{<ans[7]:5>}}+{data.ElemA}_{{<ans[8]:5>}}+\\)" +
                          $"\\({data.ElemA}_{{<ans[9]:5>}}+{data.ElemA}_{{<ans[10]:5>}}+{data.ElemA}_{{<ans[11]:5>}}.\\) \r\n";
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
                for (int i = 0; i < 12; i++)
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
