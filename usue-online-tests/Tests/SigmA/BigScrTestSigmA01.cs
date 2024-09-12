using System;
using System.Collections.Generic;
using System.Drawing;
using Test_Wrapper;

namespace UserTest
{
    public class BigScrSigmA01 : ITestCreator, ITest, ITimeLimit, IHidden, ITestGroup
    {
        public int TestID { get; set; }
        public string GroupName { get; set; }= "Sigma Sign";
        
        public string Name { get; } = "Символ суммирования 01";
        public string Description { get; } = "Работа с символом суммирования";

        public class Data
        {

            public string LetterR = "ABCDFGHKMPRSUVWXYZ";
            //public string answA = "0";
            //public string MatrA = "";
            //public string MatrB = "";
            //public string MatrC = "";
            public string ElemA = "";
            //public string ElemMatrB = "";
            //public string ElemMatrC = "";
            //public string MCa = "";
            //public string MCb = "";
            //public string MCc = "";
            public string IndexA = "";
            public string IndexB = "";
            public string IndexC = "";
            public string FormulaAnsw = "";
            public string FormulaSigma = "";
            public string FormulaElemStartA = "";
            public string FormulaElemStartB = "";
            public string FormulaElemStartC = "";
            public string FormulaElemStartD = "";
            public string FormulaElemStartE = "";
            public string FormulaElemStartF = "";
            public string FormulaElemPrintA = "";
            public string FormulaElemPrintB = "";
            public string FormulaElemPrintC = "";
            public string FormulaElemPrintD = "";
            public string FormulaElemPrintE = "";
            public string FormulaElemPrintF = "";
            public int aA, bA, bB, bC, IndMin, IndMax, Vvv, Vvw;
            public string[] Boxes = new string[6]; //{ get; set; }; 
            public Data(Random random)
            {
                CreateVars(random);
                CreateChoices(random);
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
                aA = random.Next(1, 4);
                bA = random.Next(2, 8);
                bB = random.Next(2, 8);
                if ( bA == bB ) bB++;
                bC = bB + 1;
                FormulaElemStartA = ElemA + "_{" + Convert.ToString(bA, 10) + "}+" +
                                    ElemA + "_{" + Convert.ToString(bA + 2 * aA, 10) + "}+" +
                                    ElemA + "_{" + Convert.ToString(bA + 2 * (aA + 1), 10) + "}";
                FormulaElemStartB = ElemA + "_{" + Convert.ToString(bA, 10) + "}+" +
                                    ElemA + "_{" + Convert.ToString(bA - 2 * aA, 10) + "}+" +
                                    ElemA + "_{" + Convert.ToString(bA - 2 * (aA + 1), 10) + "}";
                FormulaElemStartC = ElemA + "_{" + Convert.ToString(bB, 10) + "}+" +
                                    ElemA + "_{" + Convert.ToString(bB + 3 * aA, 10) + "}+" +
                                    ElemA + "_{" + Convert.ToString(bB + 3 * (aA + 1), 10) + "}";
                FormulaElemStartD = ElemA + "_{" + Convert.ToString(bB, 10) + "}+" +
                                    ElemA + "_{" + Convert.ToString(bB - 3 * aA, 10) + "}+" +
                                    ElemA + "_{" + Convert.ToString(bB - 3 * (aA + 1), 10) + "}";
                FormulaElemStartE = ElemA + "_{" + Convert.ToString(bC, 10) + "}+" +
                                    ElemA + "_{" + Convert.ToString(bC + 3 * aA, 10) + "}+" +
                                    ElemA + "_{" + Convert.ToString(bC + 3 * (aA + 1), 10) + "}";
                FormulaElemStartF = ElemA + "_{" + Convert.ToString(bC, 10) + "}+" +
                                    ElemA + "_{" + Convert.ToString(bC - 3 * aA, 10) + "}+" +
                                    ElemA + "_{" + Convert.ToString(bC - 3 * (aA + 1), 10) + "}";
                Vvv = random.Next(6);
                //Vvv = 0;
                switch (Vvv)
                {
                    case 0:
                        FormulaAnsw = FormulaElemStartA;
                        FormulaSigma = "\\sum\\limits_{" + IndexA + "=" + Convert.ToString(aA, 10) + "}^{" +
                                       Convert.ToString(aA+2,10)+  "} " +
                                       ElemA + "_{" + Convert.ToString(bA - 2 * aA,10) + "+2" + IndexA + "}";
                        break;
                    case 1:
                        FormulaAnsw = FormulaElemStartB;
                        FormulaSigma = "\\sum\\limits_{" + IndexA + "=" + Convert.ToString(aA, 10) + "}^{" +
                                       Convert.ToString(aA + 2, 10) + "}" +
                                       ElemA + "_{" + Convert.ToString(bA + 2 * aA, 10) + "-2" + IndexA + "}";
                        break;
                    case 2:
                        FormulaAnsw = FormulaElemStartC;
                        FormulaSigma = "\\sum\\limits_{" + IndexA + "=" + Convert.ToString(aA, 10) + "}^{" +
                                       Convert.ToString(aA + 2, 10) + "}" +
                                       ElemA + "_{" + Convert.ToString(bB - 3 * aA, 10) + "+3" + IndexA + "}";
                        break;
                    case 3:
                        FormulaAnsw = FormulaElemStartD;
                        FormulaSigma = "\\sum\\limits_{" + IndexA + "=" + Convert.ToString(aA, 10) + "}^{" +
                                       Convert.ToString(aA + 2, 10) + "}" +
                                       ElemA + "_{" + Convert.ToString(bB + 3 * aA, 10) + "-3" + IndexA + "}";
                        break;
                    case 4:
                        FormulaAnsw = FormulaElemStartE;
                        FormulaSigma = "\\sum\\limits_{" + IndexA + "=" + Convert.ToString(aA, 10) + "}^{" +
                                       Convert.ToString(aA + 2, 10) + "}" +
                                       ElemA + "_{" + Convert.ToString(bC - 3 * aA, 10) + "+3" + IndexA + "}";
                        break;
                    case 5:
                        FormulaAnsw = FormulaElemStartF;
                        FormulaSigma = "\\sum\\limits_{" + IndexA + "=" + Convert.ToString(aA, 10) + "}^{" +
                                       Convert.ToString(aA + 2, 10) + "}" +
                                       ElemA + "_{" + Convert.ToString(bC + 3 * aA, 10) + "-3" + IndexA + "}";
                        break;
                    default:
                        break;
                }
            }

            public void CreateChoices(Random random)
            {
                Vvw = random.Next(6);
                //Vvw = 0;
                switch ( Vvw )
                {
                   case 0: 
                        FormulaElemPrintA = FormulaElemStartA;
                        FormulaElemPrintB = FormulaElemStartB;
                        FormulaElemPrintC = FormulaElemStartC;
                        FormulaElemPrintD = FormulaElemStartD;
                        FormulaElemPrintE = FormulaElemStartE;
                        FormulaElemPrintF = FormulaElemStartF;
                        break;
                   case 1:
                       FormulaElemPrintA = FormulaElemStartB;
                       FormulaElemPrintB = FormulaElemStartC;
                       FormulaElemPrintC = FormulaElemStartD;
                       FormulaElemPrintD = FormulaElemStartE;
                       FormulaElemPrintE = FormulaElemStartF;
                       FormulaElemPrintF = FormulaElemStartA;
                       break;
                   case 2:
                       FormulaElemPrintA = FormulaElemStartC;
                       FormulaElemPrintB = FormulaElemStartD;
                       FormulaElemPrintC = FormulaElemStartE;
                       FormulaElemPrintD = FormulaElemStartF;
                       FormulaElemPrintE = FormulaElemStartA;
                       FormulaElemPrintF = FormulaElemStartB;
                       break;
                   case 3:
                       FormulaElemPrintA = FormulaElemStartD;
                       FormulaElemPrintB = FormulaElemStartE;
                       FormulaElemPrintC = FormulaElemStartF;
                       FormulaElemPrintD = FormulaElemStartA;
                       FormulaElemPrintE = FormulaElemStartB;
                       FormulaElemPrintF = FormulaElemStartC;
                       break;
                   case 4:
                       FormulaElemPrintA = FormulaElemStartE;
                       FormulaElemPrintB = FormulaElemStartF;
                       FormulaElemPrintC = FormulaElemStartA;
                       FormulaElemPrintD = FormulaElemStartB;
                       FormulaElemPrintE = FormulaElemStartC;
                       FormulaElemPrintF = FormulaElemStartD;
                       break;
                   case 5:
                       FormulaElemPrintA = FormulaElemStartF;
                       FormulaElemPrintB = FormulaElemStartA;
                       FormulaElemPrintC = FormulaElemStartB;
                       FormulaElemPrintD = FormulaElemStartC;
                       FormulaElemPrintE = FormulaElemStartD;
                       FormulaElemPrintF = FormulaElemStartE;
                       break;
                   default:
                       break;
                }
                //Boxes[0] = FormulaElemPrintA;
                //Boxes[1] = FormulaElemPrintB;
                //Boxes[2] = FormulaElemPrintC;
                //Boxes[3] = FormulaElemPrintD;
                //Boxes[4] = FormulaElemPrintE;
                //Boxes[5] = FormulaElemPrintF;
            }
        }
        public ITest CreateTest(int randomSeed)
        {
            ITest result = new BigScrSigmA01();

            Random random = new Random(randomSeed);
            Data data = new Data(random);
            result.CheckBoxes = data.Boxes;
            data.Boxes[0] = $"\\({data.FormulaElemPrintA}\\)";
            data.Boxes[1] = $"\\({data.FormulaElemPrintB}\\)";
            data.Boxes[2] = $"\\({data.FormulaElemPrintC}\\)";
            data.Boxes[3] = $"\\({data.FormulaElemPrintD}\\)";
            data.Boxes[4] = $"\\({data.FormulaElemPrintE}\\)";
            data.Boxes[5] = $"\\({data.FormulaElemPrintF}\\)";
            result.Text =
                //$"{data.LetterR}: " +
                //$"{data.Vvv}.{data.Vvw}: " +
                $"Отметьте правильные варианты расшифровки формулы \\( {data.FormulaSigma} \\) ";
                //$" в виде результата применения матричных операций к матрицами " +
                //$"\\({data.MatrA}=\\left({data.ElemMatrA}_{{ij}}\\right),\\quad\\) " +
                //$"::: \\({data.FormulaAnsw}\\)";
            result.CheckBoxes = data.Boxes;
            return result;
        }

        public int CheckAnswer(int randomSeed, Dictionary<string, string> answers)
        {
            int total;
            Random random = new Random(randomSeed);
            Data data = new Data(random);

            total = 0;
            //try
            //{
            //    if (answers["ans[0]"] == data.aA.ToString()) total++;
            //    if (answers["ans[1]"] == data.aB.ToString()) total++;
            //    Console.WriteLine(total);
            //}
            //catch
            //{
            //    total = 1000;
            //}
            total = total + 0;
            if (answers.ContainsKey($"\\({data.FormulaElemPrintA}\\)") && answers[$"\\({data.FormulaElemPrintA}\\)"] == "on")
            {
                if ($"\\({data.FormulaElemPrintA}\\)" == $"\\({data.FormulaAnsw}\\)")
                {
                    total += 6;
                }
                else
                {
                    total += -7;
                }
            }
            total = total + 0;
            if (answers.ContainsKey($"\\({data.FormulaElemPrintB}\\)") && answers[$"\\({data.FormulaElemPrintB}\\)"] == "on")
            {
                if ($"\\({data.FormulaElemPrintB}\\)" == $"\\({data.FormulaAnsw}\\)")
                {
                    total += 6;
                }
                else
                {
                    total += -7;
                }
            }
            total = total + 0;
            if (answers.ContainsKey($"\\({data.FormulaElemPrintC}\\)") && answers[$"\\({data.FormulaElemPrintC}\\)"] == "on")
            {
                if ($"\\({data.FormulaElemPrintC}\\)" == $"\\({data.FormulaAnsw}\\)")
                {
                    total += 6;
                }
                else
                {
                    total += -7;
                }
            }
            total = total + 0;
            if (answers.ContainsKey($"\\({data.FormulaElemPrintD}\\)") && answers[$"\\({data.FormulaElemPrintD}\\)"] == "on")
            {
                if ($"\\({data.FormulaElemPrintD}\\)" == $"\\({data.FormulaAnsw}\\)")
                {
                    total += 6;
                }
                else
                {
                    total += -7;
                }
            }
            total = total + 0;
            if (answers.ContainsKey($"\\({data.FormulaElemPrintE}\\)") && answers[$"\\({data.FormulaElemPrintE}\\)"] == "on")
            {
                if ($"\\({data.FormulaElemPrintE}\\)" == $"\\({data.FormulaAnsw}\\)")
                {
                    total += 6;
                }
                else
                {
                    total += -7;
                }
            }
            total = total + 0;
            if (answers.ContainsKey($"\\({data.FormulaElemPrintF}\\)") && answers[$"\\({data.FormulaElemPrintF}\\)"] == "on")
            {
                if ($"\\({data.FormulaElemPrintF}\\)" == $"\\({data.FormulaAnsw}\\)")
                {
                    total += 6;
                }
                else
                {
                    total += -7;
                }
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
