using System;
using System.Collections.Generic;
using System.Drawing;
using Test_Wrapper;

namespace UserTest
{
    public class BigScrTestHomomorph001 : ITestCreator, ITest, ITimeLimit, IHidden, ITestGroup
    {
        public int TestID { get; set; }
        public string GroupName { get; set; } = "AlgSyst";
        public string Name { get; } = "Гомоморфизм алгебраической системы 001";
        public string Description { get; } = "Алгебраическая система";

        public class Data
        {
            public string LetterR = "ABCDFGHKMPRSUVWXYZ";
            public string LetterXx = "012345";
            public string LetterXy = "012";
            public string LetterY = "";
            public int[] answA = new int[9];
            public int[] MatrA = new int[6];  // Подстановка на множестве {1,2,3,4,5,6}
            public int[] MatrB = new int[6];  // Обратная к MatrA подстановка на множестве {1,2,3,4,5,6}
            public int[] MatrC = new int[36]; // Таблица Кэли для полученной системы
            public int[] MatrDa = new int[6];  // таблица, задающая гомоморфизм
            public int[] MatrDb = new int[3];  // таблица, задающая прототип относительно гомоморфизма
            public int[] MatrF = new int[9];  // Таблица Кэли гомоморфного образа
            public int[] MatrG = new int[9];  // Таблица Кэли фактор-системы (индексы классов)
            public int[] MatrH = new int[3];  // Второй элемент в классе
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
                CreateSubstTab(random);
                CreateHomomorph(random);
                CreateCayleyTabA(random);
                CreateCayleyTabB(random);
            }
            public void CreateSubstTab(Random random)
            {
                Vvv = random.Next(6);
                LetterY = string.Concat(LetterY, LetterXx[Vvv].ToString());
                LetterXx = LetterXx.Replace(Convert.ToString(LetterY[0]), "");
                MatrA[0] = Int32.Parse(Convert.ToString(LetterY[0]));
                Vvv = random.Next(5);
                LetterY = string.Concat(LetterY, LetterXx[Vvv].ToString());
                LetterXx = LetterXx.Replace(Convert.ToString(LetterY[1]), "");
                MatrA[1] = Int32.Parse(Convert.ToString(LetterY[1]));
                Vvv = random.Next(4);
                LetterY = string.Concat(LetterY, LetterXx[Vvv].ToString());
                LetterXx = LetterXx.Replace(Convert.ToString(LetterY[2]), "");
                MatrA[2] = Int32.Parse(Convert.ToString(LetterY[2]));
                Vvv = random.Next(3);
                LetterY = string.Concat(LetterY, LetterXx[Vvv].ToString());
                LetterXx = LetterXx.Replace(Convert.ToString(LetterY[3]), "");
                MatrA[3] = Int32.Parse(Convert.ToString(LetterY[3]));
                Vvv = random.Next(2);
                LetterY = string.Concat(LetterY, LetterXx[Vvv].ToString());
                LetterXx = LetterXx.Replace(Convert.ToString(LetterY[4]), "");
                MatrA[4] = Int32.Parse(Convert.ToString(LetterY[4]));
                Vvv = random.Next(1);
                LetterY = string.Concat(LetterY, LetterXx[Vvv].ToString());
                LetterXx = LetterXx.Replace(Convert.ToString(LetterY[5]), "");
                MatrA[5] = Int32.Parse(Convert.ToString(LetterY[5]));
                MatrB[MatrA[0]] = 0;
                MatrB[MatrA[1]] = 1;
                MatrB[MatrA[2]] = 2;
                MatrB[MatrA[3]] = 3;
                MatrB[MatrA[4]] = 4;
                MatrB[MatrA[5]] = 5;
            }
            public void CreateHomomorph(Random random)
            {
                //MatrDa[MatrA[0]] = 0;
                //MatrDa[MatrA[3]] = 0;
                //MatrDb[0] = MatrA[0];
                //MatrDa[MatrA[1]] = 1;
                //MatrDa[MatrA[4]] = 1;
                //MatrDb[1] = MatrA[1];
                //MatrDa[MatrA[2]] = 2;
                //MatrDa[MatrA[5]] = 2;
                //MatrDb[2] = MatrA[2];
                LetterY = "";
                Vvv = random.Next(3);
                LetterY = string.Concat(LetterY, LetterXy[Vvv].ToString());
                LetterXy = LetterXy.Replace(Convert.ToString(LetterY[0]), "");
                MatrDa[MatrA[0]] = Int32.Parse(Convert.ToString(LetterY[0]));
                MatrDa[MatrA[3]] = Int32.Parse(Convert.ToString(LetterY[0]));
                MatrDb[Int32.Parse(Convert.ToString(LetterY[0]))] = MatrA[0];
                Vvv = random.Next(2);
                LetterY = string.Concat(LetterY, LetterXy[Vvv].ToString());
                LetterXy = LetterXy.Replace(Convert.ToString(LetterY[1]), "");
                MatrDa[MatrA[1]] = Int32.Parse(Convert.ToString(LetterY[1]));
                MatrDa[MatrA[4]] = Int32.Parse(Convert.ToString(LetterY[1]));
                MatrDb[Int32.Parse(Convert.ToString(LetterY[1]))] = MatrA[1];
                Vvv = random.Next(1);
                LetterY = string.Concat(LetterY, LetterXy[Vvv].ToString());
                LetterXy = LetterXy.Replace(Convert.ToString(LetterY[2]), "");
                MatrDa[MatrA[2]] = Int32.Parse(Convert.ToString(LetterY[2]));
                MatrDa[MatrA[5]] = Int32.Parse(Convert.ToString(LetterY[2]));
                MatrDb[Int32.Parse(Convert.ToString(LetterY[2]))] = MatrA[2];
            }
            public void CreateCayleyTabA(Random random)
            {
                for (iA = 0; iA < 6; iA++)
                {
                    for (jA = 0; jA < 6; jA++)
                    {
                        Vvv = MatrA[MatrB[iA]] + MatrA[MatrB[jA]];
                        if (Vvv > 5)
                        {
                            Vvv = Vvv - 6;
                        }
                        MatrC[MatrA[iA] * 6 + MatrA[jA]] = MatrA[Vvv];
                    };
                };
            }
            public void CreateCayleyTabB(Random random)
            {
                for (iA = 0; iA < 3; iA++)
                {
                    for (jA = 0; jA < 3; jA++)
                    {
                        Vvv = MatrB[MatrDb[iA]] + MatrB[MatrDb[jA]];
                        if (Vvv > 6)
                        {
                            Vvv = Vvv - 6;
                        };
                        MatrF[iA + jA * 3] = MatrDa[MatrA[Vvv]];
                        answA[iA + jA * 3] = MatrF[iA + jA * 3];
                    };
                };
                //answA[0]=1; answA[1]=2; answA[2]=3; answA[3]=4; answA[4]=5; answA[5]=6; answA[6]=7; answA[7]=8; answA[8]=9; 
            }
        }
        public ITest CreateTest(int randomSeed)
        {
            ITest result = new BigScrTestHomomorph001();

            Random random = new Random(randomSeed);
            Data data = new Data(random);

            //int num = random.Next(18);

            result.Text =
                //$"\\(\\mathtt{{MatrA}}=" +
                //$"\\begin{{array}}{{|cccccc|}}" +
                //$"\\hline 0 & 1 & 2 & 3 & 4 & 5\\\\" +
                //$"\\hline {data.MatrA[0]} & {data.MatrA[1]} & {data.MatrA[2]} & {data.MatrA[3]} & {data.MatrA[4]} & {data.MatrA[5]} \\\\" +
                //$"\\hline \\end{{array}}\\)\r\n" +
                $" В алгебраической системе операция \\( * \\) задана таблицей значений " +
                $"\\(\\begin{{array}}{{|c|cccccc|}} " +
                //$"\\multicolumn {{7}} {{c}} {{x*y}} \\\\ " +
                $"\\hline x\\backslash y & 0 & 1 & 2 & 3 & 4 & 5\\\\ \\hline  " +
                $" 0 & {data.MatrC[00]} & {data.MatrC[01]} & {data.MatrC[02]} & {data.MatrC[03]} & {data.MatrC[04]} & {data.MatrC[05]}\\\\ " +
                $" 1 & {data.MatrC[06]} & {data.MatrC[07]} & {data.MatrC[08]} & {data.MatrC[09]} & {data.MatrC[10]} & {data.MatrC[11]}\\\\ " +
                $" 2 & {data.MatrC[12]} & {data.MatrC[13]} & {data.MatrC[14]} & {data.MatrC[15]} & {data.MatrC[16]} & {data.MatrC[17]}\\\\ " +
                $" 3 & {data.MatrC[18]} & {data.MatrC[19]} & {data.MatrC[20]} & {data.MatrC[21]} & {data.MatrC[22]} & {data.MatrC[23]}\\\\ " +
                $" 4 & {data.MatrC[24]} & {data.MatrC[25]} & {data.MatrC[26]} & {data.MatrC[27]} & {data.MatrC[28]} & {data.MatrC[29]}\\\\ " +
                $" 5 & {data.MatrC[30]} & {data.MatrC[31]} & {data.MatrC[32]} & {data.MatrC[33]} & {data.MatrC[34]} & {data.MatrC[35]}\\\\ \\hline " +
                $"\\end{{array}}\\) и гомоморфизм \\(\\varphi\\) на систему с носителем \\( 0, 1, 2 \\) и операцией \\(\\circ\\) задан таблицей значений " +
                $"\\(\\begin{{array}}{{|c|cccccc|c|}}  \\hline x & 0 & 1 & 2 & 3 & 4 & 5 & *\\\\ \\hline " +
                $"x^{{\\varphi}} & {data.MatrDa[0]} & {data.MatrDa[1]} & {data.MatrDa[2]} & {data.MatrDa[3]} & {data.MatrDa[4]} & {data.MatrDa[5]} & \\circ\\\\ " +
                $"\\hline \\end{{array}}\\). Тогда таблица значений операции \\(\\circ\\) может быть задана таблицей значений " +
                //
                //$"\r\n \\(\\begin{{array}}{{|c|ccc|}} " +
                //$" \\hline x\\backslash y & 0 & 1 & 2 \\\\ \\hline " +
                //$" 0 & {data.answA[0]} & {data.answA[3]} & {data.answA[6]} \\\\ " +
                //$" 1 & {data.answA[1]} & {data.answA[4]} & {data.answA[7]} \\\\"+ 
                //$" 2 & {data.answA[2]} & {data.answA[5]} & {data.answA[8]} \\\\ " +
                ////$" 0 & <ans[0]:5> & <ans[3]:5> & <ans[6]:5> \\\\ " +
                ////$" 1 & <ans[1]:5> & <ans[4]:5> & <ans[7]:5> \\\\ "+ 
                ////$" 2 & <ans[2]:5> & <ans[5]:5> & <ans[8]:5> \\\\ " +
                //$"\\hline \\end{{array}}\\). ==>>" +
                //
                $"\\(\\begin{{array}}{{|c|ccc|}} " +
                //$"\\multicolumn{{4}}{{c}}{{x \\circ y}} " +
                $" \\hline x\\backslash y & 0 & 1 & 2 \\\\ \\hline " +
                //$" 0 & {data.answA[0]} & {data.answA[1]} & {data.answA[2]} \\\\ " +
                //$" 1 & {data.answA[3]} & {data.answA[4]} & {data.answA[5]} \\\\"+ 
                //$" 2 & {data.answA[6]} & {data.answA[7]} & {data.answA[8]} \\\\ " +
                $" 0 & <ans[0]:5> & <ans[3]:5> & <ans[6]:5> \\\\ " +
                $" 1 & <ans[1]:5> & <ans[4]:5> & <ans[7]:5> \\\\ " +
                $" 2 & <ans[2]:5> & <ans[5]:5> & <ans[8]:5> \\\\ " +
                $" \\end{{array}}\\)."; // +
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
                for (int i = 0; i < 9; i++)
                {
                    // if (answers["ans" + i]  == ans[i].ToString()) total++;
                    //if(answers[ans] == data.ans[num][0]) total++;
                    if (answers["ans[" + i + "]"] == data.answA[i].ToString()) total++;
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
