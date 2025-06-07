using System;
using System.Collections.Generic;
using System.IO;
using Test_Wrapper;

namespace UserTest
{
    public class BigScrTestFactorAlg001 : ITestCreator, ITest, ITimeLimit, IHidden, ITestGroup
    {
        public int TestID { get; set; }
        public string GroupName { get; set; }= "AlgSyst";
        public string Name { get; } = "Фактор-система 001";
        public string Description { get; } = "Алгебраическая система";
        
        public class Data
        {
            public string LetterR = "ABCDFGHKMPRSUVWXYZ";
            public string LetterXx = "012345";
            public string LetterXy = "012";
            public string LetterY = "";
            public int[] answA = new int[24];
            public int[] MatrA = new int[6];  // Подстановка на множестве {0, 1,2,3,4,5}
            public int[] MatrB = new int[6];  // Обратная к MatrA подстановка на множестве {0, 1,2,3,4,5}
            public int[] MatrC = new int[36]; // Таблица Кэли для полученной системы
            public int[] MatrDa = new int[6];  // таблица, задающая гомоморфизм
            public int[] MatrDb = new int[6];  // таблица, задающая прототип относительно гомоморфизма
            public int[] MatrDax = new int[6];  // таблица, задающая гомоморфизм
            public int[] MatrDbx = new int[6];  // таблица, задающая прототип относительно гомоморфизма
            public int[] MatrF = new int[9];  // Таблица Кэли гомоморфного образа
            public int[] MatrG = new int[18];  // Таблица Кэли фактор-системы
            public int[] MatrH = new int[3];  // Второй элемент в классе
            public int iA, jA, iB, jB, iCx, jCx, iCy, jCy, Vvv, Vvw, Vvy;
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
                CreateAnsw(random);
            }            
            public void CreateSubstTab(Random random)
            {
                Vvv = random.Next(6);
                LetterY = string.Concat(LetterY,LetterXx[Vvv].ToString());
                LetterXx=LetterXx.Replace(Convert.ToString(LetterY[0]),"");
                MatrA[0] = Int32.Parse(Convert.ToString(LetterY[0]));
                Vvv = random.Next(5);
                LetterY = string.Concat(LetterY,LetterXx[Vvv].ToString());
                LetterXx=LetterXx.Replace(Convert.ToString(LetterY[1]),"");
                MatrA[1] = Int32.Parse(Convert.ToString(LetterY[1]));
                Vvv = random.Next(4);
                LetterY = string.Concat(LetterY,LetterXx[Vvv].ToString());
                LetterXx=LetterXx.Replace(Convert.ToString(LetterY[2]),"");
                MatrA[2] = Int32.Parse(Convert.ToString(LetterY[2]));
                Vvv = random.Next(3);
                LetterY = string.Concat(LetterY,LetterXx[Vvv].ToString());
                LetterXx=LetterXx.Replace(Convert.ToString(LetterY[3]),"");
                MatrA[3] = Int32.Parse(Convert.ToString(LetterY[3]));
                Vvv = random.Next(2);
                LetterY = string.Concat(LetterY,LetterXx[Vvv].ToString());
                LetterXx=LetterXx.Replace(Convert.ToString(LetterY[4]),"");
                MatrA[4] = Int32.Parse(Convert.ToString(LetterY[4]));
                Vvv = random.Next(1);
                LetterY = string.Concat(LetterY,LetterXx[Vvv].ToString());
                LetterXx=LetterXx.Replace(Convert.ToString(LetterY[5]),"");
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
                if (MatrA[0]<MatrA[3])
                {
                    MatrDax[0] = MatrA[0]+100;
                    MatrDbx[MatrA[0]] = 0+110;
                    MatrDax[3] = MatrA[3]+120;
                    MatrDbx[MatrA[3]] = 3+130;
                    MatrDa[0] = MatrA[0];
                    MatrDb[MatrA[0]] = 0;
                    MatrDa[3] = MatrA[3];
                    MatrDb[MatrA[3]] = 3;
                }
                else
                {
                    MatrDax[0] = MatrA[3]+200;
                    MatrDbx[MatrA[3]] = 0+210;
                    MatrDax[3] = MatrA[0]+220;
                    MatrDbx[MatrA[0]] = 3+230;
                    MatrDa[0] = MatrA[3];
                    MatrDb[MatrA[3]] = 0;
                    MatrDa[3] = MatrA[0];
                    MatrDb[MatrA[0]] = 3;
                };
                if (MatrA[1]<MatrA[4])
                {
                    MatrDax[1] = MatrA[1]+300;
                    MatrDbx[MatrA[1]] = 1+310;
                    MatrDax[4] = MatrA[4]+320;
                    MatrDbx[MatrA[4]] = 4+330;
                    MatrDa[1] = MatrA[1];
                    MatrDb[MatrA[1]] = 1;
                    MatrDa[4] = MatrA[4];
                    MatrDb[MatrA[4]] = 4;
                }
                else
                {
                    MatrDax[1] = MatrA[4]+400;
                    MatrDbx[MatrA[4]] = 1+410;
                    MatrDax[4] = MatrA[1]+420;
                    MatrDbx[MatrA[1]] = 4+430;
                    MatrDa[1] = MatrA[4];
                    MatrDb[MatrA[4]] = 1;
                    MatrDa[4] = MatrA[1];
                    MatrDb[MatrA[1]] = 4;
                };
                if (MatrA[2]<MatrA[5])
                {
                    MatrDax[2] = MatrA[2]+500;
                    MatrDbx[MatrA[2]] = 2+510;
                    MatrDax[5] = MatrA[5]+520;
                    MatrDbx[MatrA[5]] = 5+530;
                    MatrDa[2] = MatrA[2];
                    MatrDb[MatrA[2]] = 2;
                    MatrDa[5] = MatrA[5];
                    MatrDb[MatrA[5]] = 5;
                }
                else
                {
                    MatrDax[2] = MatrA[5]+600;
                    MatrDbx[MatrA[5]] = 2+610;
                    MatrDax[5] = MatrA[2]+620;
                    MatrDbx[MatrA[2]] = 5+630;
                    MatrDa[2] = MatrA[5];
                    MatrDb[MatrA[5]] = 2;
                    MatrDa[5] = MatrA[2];
                    MatrDb[MatrA[2]] = 5;
                };
            }
            public void CreateCayleyTabA(Random random)
            {
                for ( iA = 0; iA < 6; iA++)
                {
                    for (jA = 0; jA < 6; jA++)
                    {                        
                        Vvv = MatrB[iA] + MatrB[jA];
                        if (Vvv>5)
                        {
                            Vvv = Vvv - 6;
                        }
                        //MatrC[MatrA[iA]*6+MatrA[jA]] = MatrA[Vvv];
                        MatrC[iA*6+jA] = MatrA[Vvv];
                    };
                };
           }
            public void CreateCayleyTabB(Random random)
            {
                for ( iA = 0; iA < 3; iA++)
                {
                    for (jA = 0; jA < 3; jA++)
                    {
                        Vvv = iA+jA;
                        if (Vvv > 5)
                        {
                            Vvv = Vvv-6;
                        }
                        MatrG[2*(iA+3*jA)] = MatrDa[Vvv];//+1000*iA+100*jA+10*Vvv;
                        answA[2*(iA+3*jA)] = MatrDa[Vvv];//+1000*iA+100*jA+10*Vvv;
                        //MatrG[2*(iA+3*jA)] = Vvv;
                        //answA[2*(iA+3*jA)] = Vvv;
                        Vvv = iA+3+jA;
                        if (Vvv > 5)
                        {
                            Vvv = Vvv-6;
                        }
                        MatrG[2*(iA+3*jA)+1] = MatrDa[Vvv];//+1000*iA+100*jA+10*Vvv;
                        answA[2*(iA+3*jA)+1] = MatrDa[Vvv];//+1000*iA+100*jA+10*Vvv;
                    }
                }
            }
            public void CreateAnsw(Random random)
            {
                answA[0] = MatrDa[3];
                answA[1] = MatrDa[4];
                answA[2] = MatrDa[5];
                answA[3] = MatrDa[3];
                for (iA=0; iA<3; iA++)
                {
                    if (MatrG[2*iA]<MatrG[2*iA+1])
                    {
                        answA[2*(iA+2)] = MatrG[2*iA];
                        answA[2*(iA+2)+1] = MatrG[2*iA+1];
                    }
                    else
                    {
                        answA[2*(iA+2)] = MatrG[2*iA+1];
                        answA[2*(iA+2)+1] = MatrG[2*iA];
                    }
                }
                answA[10] = MatrDa[4];
                for (iA=0; iA<3; iA++)
                {
                    if (MatrG[2*(3+iA)]<MatrG[2*(3+iA)+1])
                    {
                        answA[2*(iA+5)+1] = MatrG[2*(3+iA)];
                        answA[2*(iA+5)+2] = MatrG[2*(3+iA)+1];
                    }
                    else
                    {
                        answA[2*(iA+5)+1] = MatrG[2*(3+iA)+1];
                        answA[2*(iA+5)+2] = MatrG[2*(3+iA)];
                    }
                }
                answA[17] = MatrDa[5];
                for (iA=0; iA<3; iA++)
                {
                    if (MatrG[2*(6+iA)]<MatrG[2*(6+iA)+1])
                    {
                        answA[2*(iA+9)] = MatrG[2*(6+iA)];
                        answA[2*(iA+9)+1] = MatrG[2*(6+iA)+1];
                    }
                    else
                    {
                        answA[2*(iA+9)] = MatrG[2*(6+iA)+1];
                        answA[2*(iA+9)+1] = MatrG[2*(6+iA)];
                    }
                }
            }
        }
        public ITest CreateTest(int randomSeed)
        {
            ITest result = new BigScrTestFactorAlg001();

            Random random = new Random(randomSeed);
            Data data = new Data(random);
                        
            //int num = random.Next(18);

            result.Text =
                //$"\\(\\mathtt{{MatrA}}=" +
                //$"\\begin{{array}}{{|cccccc|}}" +
                //$"\\hline 0 & 1 & 2 & 3 & 4 & 5\\\\" +
                //$"\\hline {data.MatrA[0]} & {data.MatrA[1]} & {data.MatrA[2]} & {data.MatrA[3]} & {data.MatrA[4]} & {data.MatrA[5]} \\\\" +
                //$"\\hline \\end{{array}}\\)," +
                //$"\\(\\mathtt{{MatrDa}}=" +
                //$"\\begin{{array}}{{|cccccc|}}" +
                //$"\\hline 0 & 1 & 2 & 3 & 4 & 5\\\\" +
                //$"\\hline {data.MatrDax[0]} & {data.MatrDax[1]} & {data.MatrDax[2]} & {data.MatrDax[3]} & {data.MatrDax[4]} & {data.MatrDax[5]} \\\\" +
                //$"\\hline \\end{{array}}\\)," +
                ////$"\\(\\mathtt{{MatrDb}}=" +
                ////$"\\begin{{array}}{{|cccccc|}}" +
                ////$"\\hline 0 & 1 & 2 & 3 & 4 & 5\\\\" +
                ////$"\\hline {data.MatrDbx[0]} & {data.MatrDbx[1]} & {data.MatrDbx[2]} & {data.MatrDbx[3]} & {data.MatrDbx[4]} & {data.MatrDbx[5]} \\\\" +
                ////$"\\hline \\end{{array}}\\)\r\n" +
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
                $"\\end{{array}}\\) и задана конгруэнция \\(T=\\{{" +
                $" ({data.MatrA[0]},{data.MatrA[0]}), ({data.MatrA[0]},{data.MatrA[3]}), " +
                $" ({data.MatrA[3]},{data.MatrA[0]}), ({data.MatrA[3]},{data.MatrA[3]}), " +
                $" ({data.MatrA[1]},{data.MatrA[1]}), ({data.MatrA[1]},{data.MatrA[4]}), " +
                $" ({data.MatrA[4]},{data.MatrA[1]}), ({data.MatrA[4]},{data.MatrA[4]}), " +
                $" ({data.MatrA[2]},{data.MatrA[2]}), ({data.MatrA[2]},{data.MatrA[5]}), " +
                $" ({data.MatrA[5]},{data.MatrA[2]}), ({data.MatrA[5]},{data.MatrA[5]})\\}}\\). " +
                $" Тогда таблица значений операции, индуцированной \\(*\\) на фактор-системе по конгруенции \\(T\\), может быть задана таблицей значений " +
                $" (в каждом классе меньший элемент должен предшествовать большему)\r\n" +
                //$" (порядок элементов в классе не меняйте!)" +
                //$"\\(\\begin{{array}}{{|c|ccc|}} " +
                //$" \\hline C_{{i}}\\backslash C_{{j}} & \\{{{data.MatrDa[0]},{data.MatrDa[3]}\\}} &" +
                //$" \\{{{data.MatrDa[1]},{data.MatrDa[4]}\\}} & \\{{{data.MatrDa[2]},{data.MatrDa[5]}\\}} \\\\ \\hline " +
                //$" \\{{{data.MatrDa[0]},{data.MatrDa[3]}\\}} &" +
                //$" \\{{{data.answA[0]},{data.answA[1]}\\}} & \\{{{data.answA[6]},{data.answA[7]}\\}} & \\{{{data.answA[12]},{data.answA[13]}\\}} \\\\ " +
                //$" \\{{{data.MatrDa[1]},{data.MatrDa[4]}\\}} &" +
                //$" \\{{{data.answA[2]},{data.answA[3]}\\}} & \\{{{data.answA[8]},{data.answA[9]}\\}} & \\{{{data.answA[14]},{data.answA[15]}\\}} \\\\ "+ 
                //$" \\{{{data.MatrDa[2]},{data.MatrDa[5]}\\}} &" +
                //$" \\{{{data.answA[4]},{data.answA[5]}\\}} & \\{{{data.answA[10]},{data.answA[11]}\\}} & \\{{{data.answA[16]},{data.answA[17]}\\}} \\\\ " +                
                //$"\\hline \\end{{array}}\\). =>>" +
                //$"\\(\\begin{{array}}{{|c|ccc|}} " +
                //$" \\hline C_{{i}}\\backslash C_{{j}} & \\{{{data.MatrDa[0]},{data.answA[3]}\\}} &" +
                //$" \\{{{data.MatrDa[1]},{data.answA[10]}\\}} & \\{{{data.MatrDa[2]},{data.answA[17]}\\}} \\\\ \\hline " +
                //$" \\{{{data.MatrDa[0]},{data.answA[0]}\\}} &" +
                //$" \\{{{data.answA[4]},{data.answA[5]}\\}} & \\{{{data.answA[11]},{data.answA[12]}\\}} & \\{{{data.answA[18]},{data.answA[19]}\\}} \\\\ " +
                //$" \\{{{data.MatrDa[1]},{data.answA[1]}\\}} &" +
                //$" \\{{{data.answA[6]},{data.answA[7]}\\}} & \\{{{data.answA[13]},{data.answA[14]}\\}} & \\{{{data.answA[20]},{data.answA[21]}\\}} \\\\ "+ 
                //$" \\{{{data.MatrDa[2]},{data.answA[2]}\\}} &" +
                //$" \\{{{data.answA[8]},{data.answA[9]}\\}} & \\{{{data.answA[15]},{data.answA[16]}\\}} & \\{{{data.answA[22]},{data.answA[23]}\\}} \\\\ " +                
                //$"\\hline \\end{{array}}\\). ==>>" +
                $"\\(\\begin{{array}}{{|c|ccc|}} " +
                $" \\hline C_{{i}}\\backslash C_{{j}} & \\{{{data.MatrDa[0]},<ans[3]:2>\\}} &" +
                $" \\{{{data.MatrDa[1]},<ans[10]:2>\\}} & \\{{{data.MatrDa[2]},<ans[17]:2>\\}} \\\\ \\hline " +
                $" \\{{{data.MatrDa[0]},<ans[0]:2>\\}} &" +
                $" \\{{<ans[4]:2>,<ans[5]:2>\\}} & \\{{<ans[11]:2>,<ans[12]:2>\\}} & \\{{<ans[18]:2>,<ans[19]:2>\\}} \\\\ " +
                $" \\{{{data.MatrDa[1]},<ans[1]:2>\\}} &" +
                $" \\{{<ans[6]:2>,<ans[7]:2>\\}} & \\{{<ans[13]:2>,<ans[14]:2>\\}} & \\{{<ans[20]:2>,<ans[21]:2>\\}} \\\\ "+ 
                $" \\{{{data.MatrDa[2]},<ans[2]:2>\\}} &" +
                $" \\{{<ans[8]:2>,<ans[9]:2>\\}} & \\{{<ans[15]:2>,<ans[16]:2>\\}} & \\{{<ans[22]:2>,<ans[23]:2>\\}} \\\\ " +                
                $" \\end{{array}}\\).";
                //
                //$"\\(\\begin{{array}}{{|c|ccc|}} " +
                ////$"\\multicolumn{{4}}{{c}}{{x \\circ y}} " +
                //$" \\hline C_{{i}}\\backslash C_{{j}} & \\{{{data.MatrA[0]},{data.MatrA[3]}\\}} &" +
                //$" \\{{{data.MatrA[1]},{data.MatrA[4]}\\}} & \\{{{data.MatrA[2]},{data.MatrA[5]}\\}} \\\\ \\hline " +
                ////$" 0 & {data.answA[0]} & {data.answA[1]} & {data.answA[2]} \\\\ " +
                ////$" 1 & {data.answA[3]} & {data.answA[4]} & {data.answA[5]} \\\\"+ 
                ////$" 2 & {data.answA[6]} & {data.answA[7]} & {data.answA[8]} \\\\ " +
                //$" \\{{{data.MatrA[0]},{data.MatrA[3]}\\}} &" +
                //$" \\{{<ans[0]:2>,<ans[1]:2>\\}} & \\{{<ans[6]:2>,<ans[7]:2>\\}} & \\{{<ans[12]:2>,<ans[13]:2>\\}} \\\\ " +
                //$" \\{{{data.MatrA[1]},{data.MatrA[4]}\\}} &" +
                //$" \\{{<ans[2]:2>,<ans[3]:2>\\}} & \\{{<ans[8]:2>,<ans[9]:2>\\}} & \\{{<ans[14]:2>,<ans[15]:2>\\}} \\\\ "+ 
                //$" \\{{{data.MatrA[2]},{data.MatrA[5]}\\}} &" +
                //$" \\{{<ans[4]:2>,<ans[5]:2>\\}} & \\{{<ans[10]:2>,<ans[11]:2>\\}} & \\{{<ans[16]:2>,<ans[17]:2>\\}} \\\\ " +
                //$"\\hline \\end{{array}}\\)."; // +
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
                for (int i = 0; i < 24; i++)
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
        public List<MemoryStream> Pictures { get; set; }
        public int TimeLimitSeconds { get; set; } = 120;
        public bool IsHidden { get; set; } = false;
    }
}
