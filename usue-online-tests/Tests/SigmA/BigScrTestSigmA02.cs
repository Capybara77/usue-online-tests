using System;
using System.Collections.Generic;
using System.Drawing;
using Test_Wrapper;

namespace UserTest
{
    public class BigScrTestSigmA02 : ITestCreator, ITest, ITimeLimit, IHidden, ITestGroup
    {
        public int TestID { get; set; }
        public string GroupName { get; set; }= "Sigma Sign";
        public string Name { get; } = "Символ суммирования 02";
        public string Description { get; } = "Работа с символом суммирования";

        public class Data
        {

            public string LetterR = "ABCDFGHKMPRSUVWXYZ";
            public int[] answA = new int[4];
            public int nA, nB, nC;
            //public int MembNumbA, MembNumbB, MembNumbC;
            public string MembFormTeXA, MembFormTeXB, MembFormTeXC, MembFormTeXn, IndexA;
            public string MembFormCalcA, MembFormCalcB, MembFormCalcC, MembFormCalcn;
            //public int[] MatrB = new int[4];
            //public int[] MatrC = new int[4];
            public int Vvv, Vvw;
            //public string ElemMatrA = "";
            //public string ElemMatrB = "";
            //public string ElemMatrC = "";
            //public string MCa = "";
            //public string MCb = "";
            //public string MCc = "";

            public Data(Random random)
            {
                CreateMembers(random);
                //CreateChoices(random);
                //MembFormCalcA = MembFormTeXn.Replace("n", Convert.ToString(nA, 10));
                //MembFormCalcB = MembFormTeXn.Replace("n", Convert.ToString(nA + 1, 10));
                //MembFormCalcC = MembFormTeXn.Replace("n", Convert.ToString(nA + 2, 10));
            }
            public void CreateMembers(Random random)
            {
                //int[] Numbers = new int[40];
                //for (int i = 0; i < 20; i++)
                //{
                //    Numbers[2*i] = -i-1;
                //    Numbers[2*i+1] = i+1;
                //}
                nA = random.Next(6);
                nB = nA + 2;
                Vvv = random.Next(8);
                //Vvv = 0;
                switch ( Vvv )
                {
                    case 0:
                        MembFormTeXn = "n(n-2)";
                        MembFormCalcn = "n*(n-2)";
                        answA[0] = (nA + 0) * ((nA + 0) - 2);
                        answA[1] = (nA + 1) * ((nA + 1) - 2);
                        answA[2] = (nA + 2) * ((nA + 2) - 2);
                        answA[3] = answA[0]+ answA[1]+ answA[2];
                        break;
                    case 1:
                        MembFormTeXn = "n(n+1)";
                        MembFormCalcn = "n*(n+1)";
                        answA[0] = (nA + 0) * ((nA + 0) + 1);
                        answA[1] = (nA + 1) * ((nA + 1) + 1);
                        answA[2] = (nA + 2) * ((nA + 2) + 1);
                        answA[3] = answA[0] + answA[1] + answA[2];
                        break;
                    case 2:
                        MembFormTeXn = "(2n+1)";
                        MembFormCalcn = "(2*n+1)";
                        answA[0] = (2*(nA + 0) + 1);
                        answA[1] = (2*(nA + 1) + 1);
                        answA[2] = (2*(nA + 2) + 1);
                        answA[3] = answA[0] + answA[1] + answA[2];
                        break;
                    case 3:
                        MembFormTeXn = "(2n-1)";
                        MembFormCalcn = "(2*n-1)";
                        answA[0] = (2 * (nA + 0) - 1);
                        answA[1] = (2 * (nA + 1) - 1);
                        answA[2] = (2 * (nA + 2) - 1);
                        answA[3] = answA[0] + answA[1] + answA[2];
                        break;
                    case 4:
                        MembFormTeXn = "(n-1)(n+1)";
                        MembFormCalcn = "(n-1)*(n+1)";
                        answA[0] = ((nA + 0) - 1) * ((nA + 0) + 1);
                        answA[1] = ((nA + 1) - 1) * ((nA + 1) + 1);
                        answA[2] = ((nA + 2) - 1) * ((nA + 2) + 1);
                        answA[3] = answA[0] + answA[1] + answA[2];
                        break;
                    case 5:
                        MembFormTeXn = "n(2n+1)";
                        MembFormCalcn = "n*(2*n+1)";
                        answA[0] = (nA + 0) * (2 * (nA + 0) + 1);
                        answA[1] = (nA + 1) * (2 * (nA + 1) + 1);
                        answA[2] = (nA + 2) * (2 * (nA + 2) + 1);
                        answA[3] = answA[0] + answA[1] + answA[2];
                        break;
                    case 6:
                        MembFormTeXn = "n(2n-1)";
                        MembFormCalcn = "n*(2*n-1)";
                        answA[0] = (nA + 0) * (2 * (nA + 0) - 1);
                        answA[1] = (nA + 1) * (2 * (nA + 1) - 1);
                        answA[2] = (nA + 2) * (2 * (nA + 2) - 1);
                        answA[3] = answA[0] + answA[1] + answA[2];
                        break;
                    case 7:
                        MembFormTeXn = "(n^2-n)";
                        MembFormCalcn = "(n^2-n)";
                        answA[0] = (nA + 0)^2 - (nA + 0);
                        answA[1] = (nA + 1)^2 - (nA + 1);
                        answA[2] = (nA + 2)^2 - (nA + 2);
                        answA[3] = answA[0] + answA[1] + answA[2];
                        break;
                }
                int letterCount = LetterR.Length;
                IndexA = $"{LetterR[random.Next(letterCount)]}";
                LetterR = LetterR.Replace(IndexA, "");
                IndexA = IndexA.ToLower();
                letterCount = LetterR.Length;
                Vvw = random.Next(8);
            }
        }
        public ITest CreateTest(int randomSeed)
        {
            ITest result = new BigScrTestSigmA02();

            Random random = new Random(randomSeed);
            Data data = new Data(random);

            result.Text = //$"{data.answA[0]}:{data.answA[1]}:{data.answA[2]}:{data.answA[3]}:::" +
                          //{data.MembFormCalcn}
                          $"\\(\\sum\\limits_{{{data.IndexA} = {data.nA}}}^{{{data.nB}}} {data.MembFormTeXn}=\\)" +
                          $"\\(<ans[0]:5> + <ans[1]:5> + <ans[2]:5> = <ans[3]:5>\\)\r\n" +
                          $"(в поля для ввода вводите только числа, слагаемые переставлять местами нельзя,\r\n" +
                          $"не игнорируйте нулевые слагаемые). ";

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
                for (int i = 0; i < 4; i++)
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
