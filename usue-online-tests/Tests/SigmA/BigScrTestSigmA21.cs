using System;
using System.Collections.Generic;
using System.Drawing;
using Test_Wrapper;

namespace UserTest
{
    public class BigScrTestSigmA21 : ITestCreator, ITest, ITimeLimit, IHidden, ITestGroup
    {
        public int TestID { get; set; }
        public string GroupName { get; set; }= "Sigma Sign";
        public string Name { get; } = "Двойной символ суммирования с переменными границами 21";
        public string Description { get; } = "Работа с символом суммирования";

        public class Data
        {

            public string LetterR = "ABCDFGHKMPRSUVWXYZ";
            public int[] answA = new int[10];
            public string IndexA = "";
            public string IndexB = "";
            public string IndexC = "";
            public int nA, nB, nC;
            //public int MembNumbA, MembNumbB, MembNumbC;
            public string FormulaSigma;
            public string MembFormTeXA, MembFormTeXB, MembFormTeXC, MembFormTeXn;
            public int MembAaa, MembAab, MembAac, MembAba, MembAbb, MembAbc, MembAca, MembAcb, MembAcc;
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
                //ElemA = $"{LetterR[random.Next(letterCount)]}";
                //LetterR = LetterR.Replace(ElemA, "");
                //ElemA = ElemA.ToLower();
                //letterCount = LetterR.Length;
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
                aB = random.Next(2, 6);
                bB = random.Next(2, 6);
                if (bB == aB) bB++;
                Vvu = random.Next(3);
                //Vvu = 0;
                switch (Vvu)
                {
                    case 0:
                        MembFormTeXA= "\\left(" + Convert.ToString(aB, 10) + IndexA + "\\cdot " + IndexB +"\\right)";
                        MembAaa=aB*aA*(bA+2); MembAab=aB*(aA+1)*(bA+2); MembAac=aB*(aA+2)*(bA+2); 
                        MembAba=aB*aA*(bA+1); MembAbb=aB*(aA+1)*(bA+1); MembAbc=aB*(aA+2)*(bA+1); 
                        MembAca=aB*aA*(bA+0); MembAcb=aB*(aA+1)*(bA+0); MembAcc=aB*(aA+2)*(bA+0);
                        break;
                    case 1:
                        MembFormTeXA= "\\left(" + Convert.ToString(aB, 10) + IndexA + "+" + Convert.ToString(bB, 10) + IndexB +"\\right)";
                        MembAaa=aB*aA+bB*(bA+2); MembAab=aB*(aA+1)+bB*(bA+2); MembAac=aB*(aA+2)+bB*(bA+2); 
                        MembAba=aB*aA+bB*(bA+1); MembAbb=aB*(aA+1)+bB*(bA+1); MembAbc=aB*(aA+2)+bB*(bA+1); 
                        MembAca=aB*aA+bB*(bA+0); MembAcb=aB*(aA+1)+bB*(bA+0); MembAcc=aB*(aA+2)+bB*(bA+0);
                        break;
                    case 2:
                        MembFormTeXA= "\\left(" + Convert.ToString(aB, 10) + IndexA + "-" + Convert.ToString(bB, 10) + IndexB +"\\right)";
                        MembAaa=aB*aA-bB*(bA+2); MembAab=aB*(aA+1)-bB*(bA+2); MembAac=aB*(aA+2)-bB*(bA+2); 
                        MembAba=aB*aA-bB*(bA+1); MembAbb=aB*(aA+1)-bB*(bA+1); MembAbc=aB*(aA+2)-bB*(bA+1); 
                        MembAca=aB*aA-bB*(bA+0); MembAcb=aB*(aA+1)-bB*(bA+0); MembAcc=aB*(aA+2)-bB*(bA+0);
                        break;
                    default:
                        break;
                }
                Map = IndexB + "=" + Convert.ToString(bA+2, 10);
                Mbp = IndexB + "=" + Convert.ToString(bA+1, 10);
                Mcp = IndexB + "=" + Convert.ToString(bA, 10);
                Mqa = IndexA + "=" + Convert.ToString(aA, 10);
                Mqb = IndexA + "=" + Convert.ToString(aA+1, 10); 
                Mqc = IndexA + "=" + Convert.ToString(aA+2, 10);
                Vvv = random.Next(4);
                //Vvv = 3;
                switch (Vvv)
                {
                    case 0:
                        FormulaSigma =  "\\sum\\limits_{" + IndexA + "=" + Convert.ToString(aA, 10) + "}^{" +
                                        Convert.ToString(aA+2,10)+  "} " +
                                        "\\sum\\limits_{" + IndexB + "=" + Convert.ToString(bA, 10) + "}^{" +
                                        IndexA + "+" + Convert.ToString(bA - aA, 10) + "} " + MembFormTeXA;
                        answA[0] = 0; answA[1] = 0; answA[2] = 1;
                        answA[3] = 0; answA[4] = 1; answA[5] = 1;
                        answA[6] = 1; answA[7] = 1; answA[8] = 1;
                        answA[9]= MembAac + MembAbb + MembAbc + MembAca + MembAcb + MembAcc;
                        break;
                    case 1:
                        FormulaSigma =  "\\sum\\limits_{" + IndexA + "=" + Convert.ToString(aA, 10) + "}^{" +
                                        Convert.ToString(aA+2,10)+  "} " +
                                        "\\sum\\limits_{" + IndexB + "=" + IndexA + "+" + Convert.ToString(bA - aA, 10) + "}^{" +
                                         Convert.ToString(bA+2, 10)+ "} " + MembFormTeXA;
                        answA[0] = 1; answA[1] = 1; answA[2] = 1;
                        answA[3] = 1; answA[4] = 1; answA[5] = 0;
                        answA[6] = 1; answA[7] = 0; answA[8] = 0;
                        answA[9]= MembAaa + MembAab + MembAac + MembAba + MembAbb + MembAca;
                        break;
                    case 2:
                        FormulaSigma =  "\\sum\\limits_{" + IndexA + "=" + Convert.ToString(aA, 10) + "}^{" +
                                        Convert.ToString(aA+2,10)+  "} " +
                                        "\\sum\\limits_{" + IndexB + "=" + Convert.ToString(bA + aA+2, 10) + "-" + IndexA + "}^{" +
                                         Convert.ToString(bA+2, 10)+ "} " + MembFormTeXA;
                        answA[0] = 1; answA[1] = 1; answA[2] = 1;
                        answA[3] = 0; answA[4] = 1; answA[5] = 1;
                        answA[6] = 0; answA[7] = 0; answA[8] = 1;
                        answA[9]= MembAaa + MembAab + MembAac + MembAbb + MembAbc + MembAcc;
                        break;
                    case 3:
                        FormulaSigma =  "\\sum\\limits_{" + IndexA + "=" + Convert.ToString(aA, 10) + "}^{" +
                                        Convert.ToString(aA+2,10)+  "} " +
                                        "\\sum\\limits_{" + IndexB + "=" + Convert.ToString(bA, 10) + "}^{" +
                                        Convert.ToString(bA + aA + 2, 10) + "-" + IndexA + "} " + MembFormTeXA;
                        answA[0] = 1; answA[1] = 0; answA[2] = 0;
                        answA[3] = 1; answA[4] = 1; answA[5] = 0;
                        answA[6] = 1; answA[7] = 1; answA[8] = 1;
                        answA[9]= MembAaa + MembAba + MembAbb + MembAca + MembAcb + MembAcc;
                        break;
                    case 4:
                        //FormulaAnsw = FormulaElemStartE;
                        //FormulaSigma =  "\\sum\\limits_{" + IndexB + "=" + Convert.ToString(bB, 10) + "}^{" +
                        //                Convert.ToString(bB + 1, 10) + "} " +
                        //                "\\sum\\limits_{" + IndexA + "=" + Convert.ToString(aB, 10) + "}^{" +
                        //                Convert.ToString(aB+2,10)+  "} " +
                        //                ElemA + "_{" + Convert.ToString(aA - aB * aC,10) + "+" + Convert.ToString(aC,10) + IndexA + "," + 
                        //                Convert.ToString(bA - bB * bC,10) + "+" + Convert.ToString(bC,10) + IndexB + "}";
                        answA[9]= MembAaa + MembAab + MembAac + MembAba + MembAbb + MembAbc + MembAca + MembAcb + MembAcc;
                        break;
                    case 5:
                        //FormulaAnsw = FormulaElemStartF;
                        //FormulaSigma =  "\\sum\\limits_{" + IndexB + "=" + Convert.ToString(bB, 10) + "}^{" +
                        //                Convert.ToString(bB + 1, 10) + "} " +
                        //                "\\sum\\limits_{" + IndexA + "=" + Convert.ToString(aB, 10) + "}^{" +
                        //                Convert.ToString(aB+2,10)+  "} " +
                        //                ElemA + "_{" + Convert.ToString(aA - aB * aC,10) + "+" + Convert.ToString(aC,10) + IndexA + "," + 
                        //                Convert.ToString(bA + bB * bC,10) + "-" + Convert.ToString(bC,10) + IndexB + "}";
                        answA[9]= MembAaa + MembAab + MembAac + MembAba + MembAbb + MembAbc + MembAca + MembAcb + MembAcc;
                        break;
                    case 6:
                        //FormulaAnsw = FormulaElemStartG;
                        //FormulaSigma =  "\\sum\\limits_{" + IndexB + "=" + Convert.ToString(bB, 10) + "}^{" +
                        //                Convert.ToString(bB + 1, 10) + "} " +
                        //                "\\sum\\limits_{" + IndexA + "=" + Convert.ToString(aB, 10) + "}^{" +
                        //                Convert.ToString(aB+2,10)+  "} " +
                        //                ElemA + "_{" + Convert.ToString(aA + aB * aC,10) + "-" + Convert.ToString(aC,10) + IndexA + "," + 
                        //                Convert.ToString(bA - bB * bC,10) + "+" + Convert.ToString(bC,10) + IndexB + "}";
                        answA[9]= MembAaa + MembAab + MembAac + MembAba + MembAbb + MembAbc + MembAca + MembAcb + MembAcc;
                        break;
                    case 7:
                        //FormulaAnsw = FormulaElemStartH;
                        //FormulaSigma =  "\\sum\\limits_{" + IndexB + "=" + Convert.ToString(bB, 10) + "}^{" +
                        //                Convert.ToString(bB + 1, 10) + "} " +
                        //                "\\sum\\limits_{" + IndexA + "=" + Convert.ToString(aB, 10) + "}^{" +
                        //                Convert.ToString(aB+2,10)+  "} " +
                        //                ElemA + "_{" + Convert.ToString(aA + aB * aC,10) + "-" + Convert.ToString(aC,10) + IndexA + "," + 
                        //                Convert.ToString(bA + bB * bC,10) + "-" + Convert.ToString(bC,10) + IndexB + "}";
                        answA[9]= MembAaa + MembAab + MembAac + MembAba + MembAbb + MembAbc + MembAca + MembAcb + MembAcc;
                        break;
                    default:
                        break;
                }
            }
        }
        public ITest CreateTest(int randomSeed)
        {
            ITest result = new BigScrTestSigmA21();

            Random random = new Random(randomSeed);
            Data data = new Data(random);

            result.Text = //$"{data.answA[0]}:{data.answA[1]}:{data.answA[2]}: \r\n" +
                          //$"{data.answA[3]}:{data.answA[4]}:{data.answA[5]}: \r\n" +
                          //$"{data.answA[6]}:{data.answA[7]}:{data.answA[8]}: \r\n" +
                          //{data.MembFormCalcn}
                          //$"\\(\\sum\\limits_{{{data.IndexA} = {data.nA}}}^{{{data.nB}}} {data.MembFormTeXn}=\\)" +
                          //$"\\(<ans[0]:5> + <ans[1]:5> + <ans[2]:5> = <ans[3]:5>\\)\r\n" +
                          //$"(в поля для ввода вводите только числа, слагаемые переставлять местами нельзя,\r\n" +
                          //$"не игнорируйте нулевые слагаемые). ";
                          //$"{data.Vvu},{data.Vvv}.{data.Vvw}" +
                          $"\\({data.FormulaSigma}=<ans[9]:5>\\) \r\n" +
                          $"В данной таблице в поле для ввода поставьте 1, если слагаемое с данными индексами содержится в сумме " +
                          $"и 0 в противном случае:\r\n" +
                          $"\\(\\begin{{array}}{{c|ccc}}" +
                          $"    {data.Map} & <ans[0]:5>   & <ans[1]:5>   & <ans[2]:5>\\\\" +
                          $"    {data.Mbp} & <ans[3]:5>   & <ans[4]:5>   & <ans[5]:5>\\\\" +
                          $"    {data.Mcp} & <ans[6]:5>   & <ans[7]:5>   & <ans[8]:5>\\\\" +
                          $"    \\hline" +
                          $"               & {data.Mqa} & {data.Mqb} & {data.Mqc}\\\\" +
                          $"\\end{{array}}\\)";

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
                for (int i = 0; i < 10; i++)
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
