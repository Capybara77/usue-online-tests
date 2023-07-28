using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using org.mariuszgromada.math.mxparser;
using org.mariuszgromada.math.mxparser.mathcollection;
using Test_Wrapper;

namespace usue_online_tests.Tests.List
{
    public class IntFunctGa001 : ITestCreator, ITest, ITimeLimit, IHidden, ITestGroup
    {
        public int TestID { get; set; }
        public string GroupName { get; set; } = "Integr of function";
        public string Name { get; } = "Интегрирование заменой переменных 01";
        public string Description { get; } = "Интегрирование функций";
        public class Data
        {
            public string Wwx = "";
            //public string Wwy = "";
            public int aA, aB, aC, aP0, aP1, aP2, aP3, Vvu, Vvv, Vvw, kA, kB;
            public double xMaxInv;
            //public string[] BasicFunctA = new string[200];
            public string FunctTeX;
            public string ArgumOnStart;
            public string FunctOnStart;
            public string FunctCalc;
            public string ConstStart;
            public string ReplaceAnsw;
            public string ReplaceA01 = "\\(t=\\frac{1}{ax-b}\\)"; 
            public string ReplaceA02 = "\\(t=...\\sqrt[n]{a x-b}\\)";
            public string ReplaceA03 = "\\(t=\\sqrt[\\ldots]{\\frac{x-a}{x-b}}\\)"; 
            public string ReplaceA04 = "\\(x=a\\left(\\mbox{tg}\\,t+b\\right)\\)";
            public string ReplaceA05 = "\\(x=\\frac{a}{\\cos{t}}+b\\)";
            public string ReplaceA06 = "\\(x=a\\left(\\sin\\,t+b\\right)\\)";
            //public string ReplaceA07 = "\\(x=...\\left(\\strut\\ldots t+\\ldots\\right)^{\\ldots}\\)";
            public string ReplaceB01, ReplaceB02, ReplaceB03, ReplaceB04, ReplaceB05, ReplaceB06;
            public string FormOfIntA;
            public string[] Boxes = new string[6]; //{ get; set; }; 
            //public string[] Boxes { get; set; } = { "\\(x=...\\cdot\\sin(...\\cdot t)\\)", "\\(x=...\\sqrt{\\strut\\ldots t+\\ldots}\\)", "\\(x=...\\cdot\\mbox{tg}(...\\cdot t)\\)" };//$"\\(t=...\\cdot\\mbox{tg}...\\)", $"t=...\\cdot\\mbox{arcsin}...",};

            public Data(Random random)
            {
                CreateIntA(random);
                CreateReplace(random);
            }
            
            public void CreateIntA(Random random)
            {
                Vvu = random.Next(6);
                aA = random.Next(2,9);
                aB = random.Next(3,10);
                aC = random.Next(4,9);
                if (aA == aB) aB++;
                Vvv = random.Next(6);
                switch (Vvv)
                {
                    case 0:
                        FormOfIntA = string.Concat("\\int F\\left(x,\\frac{1}{", Convert.ToString(aA,10), "x-", Convert.ToString(aB,10), "}, \\ldots, \\left(\\frac{1}{", Convert.ToString(aA,10), "x-", Convert.ToString(aB,10), "}\\right)^{", Convert.ToString(aC,10), "}\\right)dx");
                        ReplaceAnsw = ReplaceA01;
                        break;
                    case 1:
                        FormOfIntA = string.Concat("\\int F\\left(x,\\sqrt{", Convert.ToString(aA,10), "x-", Convert.ToString(aB,10),"}, \\ldots, \\sqrt[n]{\\left(", Convert.ToString(aA,10), "x-", Convert.ToString(aB,10),"\\right)^{", Convert.ToString(aC,10), "}}\\right)dx");
                        ReplaceAnsw = ReplaceA02;
                        break;
                    case 2:
                        FormOfIntA = string.Concat("\\int F\\left(x,\\sqrt[]{\\frac{x-", Convert.ToString(aA,10),"}{x-", Convert.ToString(aB,10), "}}, \\ldots, \\sqrt[n]{\\left(\\frac{x-", Convert.ToString(aA,10),"}{x-", Convert.ToString(aB,10), "}\\right)^{", Convert.ToString(aC,10), "}}\\right)dx");
                        ReplaceAnsw = ReplaceA03;
                        break;
                    case 3:
                        FormOfIntA = string.Concat("\\int F\\left(x,\\sqrt{", Convert.ToString(aA*aA,10), "+(x-", Convert.ToString(aB,10),")^{2}}\\right)dx");
                        ReplaceAnsw = ReplaceA04;
                        break;
                    case 4:
                        FormOfIntA = string.Concat("\\int F\\left(x,\\sqrt{(x-", Convert.ToString(aA,10),")^{2}-", Convert.ToString(aB*aB,10), "}\\right)dx");
                        ReplaceAnsw = ReplaceA05;
                        break;
                    case 5:
                        FormOfIntA = string.Concat("\\int F\\left(x,\\sqrt{", Convert.ToString(aA*aA,10), "-(x-", Convert.ToString(aB,10),")^{2}}\\right)dx");
                        ReplaceAnsw = ReplaceA06;
                        break;
                    default:
                        break;
                }
            }

            public void CreateReplace(Random random)
            {
                ConstStart = "0";
                //aA = random.Next(2,10);
                //aB = random.Next(2,10);
                //aP0 = random.Next(3);
                Vvw = random.Next(24);
                switch (Vvw)
                {
                    case 0:
                        ReplaceB01 = ReplaceA01; ReplaceB02 = ReplaceA02; ReplaceB03 = ReplaceA03; ReplaceB04 = ReplaceA04; ReplaceB05 = ReplaceA05; ReplaceB06 = ReplaceA06;
                        break;
                    case 1:
                        ReplaceB01 = ReplaceA02; ReplaceB02 = ReplaceA03; ReplaceB03 = ReplaceA04; ReplaceB04 = ReplaceA05; ReplaceB05 = ReplaceA06; ReplaceB06 = ReplaceA01;
                        break;
                    case 2:
                        ReplaceB01 = ReplaceA03; ReplaceB02 = ReplaceA04; ReplaceB03 = ReplaceA05; ReplaceB04 = ReplaceA06; ReplaceB05 = ReplaceA01; ReplaceB06 = ReplaceA02;
                        break;
                    case 3:
                        ReplaceB01 = ReplaceA04; ReplaceB02 = ReplaceA05; ReplaceB03 = ReplaceA06; ReplaceB04 = ReplaceA01; ReplaceB05 = ReplaceA02; ReplaceB06 = ReplaceA03;
                        break;
                    case 4:
                        ReplaceB01 = ReplaceA05; ReplaceB02 = ReplaceA06; ReplaceB03 = ReplaceA01; ReplaceB04 = ReplaceA02; ReplaceB05 = ReplaceA03; ReplaceB06 = ReplaceA04;
                        break;
                    case 5:
                        ReplaceB01 = ReplaceA06; ReplaceB02 = ReplaceA01; ReplaceB03 = ReplaceA02; ReplaceB04 = ReplaceA03; ReplaceB05 = ReplaceA04; ReplaceB06 = ReplaceA05;
                        break;
                    case 6:
                        ReplaceB01 = ReplaceA02; ReplaceB02 = ReplaceA03; ReplaceB03 = ReplaceA01; ReplaceB04 = ReplaceA05; ReplaceB05 = ReplaceA06; ReplaceB06 = ReplaceA04;
                        break;
                    case 7:
                        ReplaceB01 = ReplaceA03; ReplaceB02 = ReplaceA01; ReplaceB03 = ReplaceA02; ReplaceB04 = ReplaceA06; ReplaceB05 = ReplaceA04; ReplaceB06 = ReplaceA05;
                        break;
                    case 8:
                        ReplaceB01 = ReplaceA02; ReplaceB02 = ReplaceA01; ReplaceB03 = ReplaceA04; ReplaceB04 = ReplaceA03; ReplaceB05 = ReplaceA06; ReplaceB06 = ReplaceA05;
                        break;
                    case 9:
                        ReplaceB01 = ReplaceA03; ReplaceB02 = ReplaceA01; ReplaceB03 = ReplaceA02; ReplaceB04 = ReplaceA05; ReplaceB05 = ReplaceA06; ReplaceB06 = ReplaceA04;
                        break;
                    case 10:
                        ReplaceB01 = ReplaceA02; ReplaceB02 = ReplaceA03; ReplaceB03 = ReplaceA01; ReplaceB04 = ReplaceA06; ReplaceB05 = ReplaceA04; ReplaceB06 = ReplaceA05;
                        break;
                    case 11:
                        ReplaceB01 = ReplaceA06; ReplaceB02 = ReplaceA05; ReplaceB03 = ReplaceA04; ReplaceB04 = ReplaceA03; ReplaceB05 = ReplaceA02; ReplaceB06 = ReplaceA01;
                        break;
                    case 12:
                        ReplaceB01 = ReplaceA03; ReplaceB02 = ReplaceA02; ReplaceB03 = ReplaceA01; ReplaceB04 = ReplaceA06; ReplaceB05 = ReplaceA05; ReplaceB06 = ReplaceA04;
                        break;
                    case 13:
                        ReplaceB01 = ReplaceA02; ReplaceB02 = ReplaceA03; ReplaceB03 = ReplaceA04; ReplaceB04 = ReplaceA01; ReplaceB05 = ReplaceA06; ReplaceB06 = ReplaceA05;
                        break;
                    case 14:
                        ReplaceB01 = ReplaceA03; ReplaceB02 = ReplaceA04; ReplaceB03 = ReplaceA01; ReplaceB04 = ReplaceA02; ReplaceB05 = ReplaceA06; ReplaceB06 = ReplaceA05;
                        break;
                    case 15:
                        ReplaceB01 = ReplaceA04; ReplaceB02 = ReplaceA01; ReplaceB03 = ReplaceA02; ReplaceB04 = ReplaceA03; ReplaceB05 = ReplaceA06; ReplaceB06 = ReplaceA05;
                        break;
                    case 16:
                        ReplaceB01 = ReplaceA02; ReplaceB02 = ReplaceA01; ReplaceB03 = ReplaceA04; ReplaceB04 = ReplaceA05; ReplaceB05 = ReplaceA06; ReplaceB06 = ReplaceA03;
                        break;
                    case 17:
                        ReplaceB01 = ReplaceA02; ReplaceB02 = ReplaceA01; ReplaceB03 = ReplaceA05; ReplaceB04 = ReplaceA06; ReplaceB05 = ReplaceA03; ReplaceB06 = ReplaceA04;
                        break;
                    case 18:
                        ReplaceB01 = ReplaceA02; ReplaceB02 = ReplaceA01; ReplaceB03 = ReplaceA06; ReplaceB04 = ReplaceA03; ReplaceB05 = ReplaceA04; ReplaceB06 = ReplaceA05;
                        break;
                    case 19:
                        ReplaceB01 = ReplaceA06; ReplaceB02 = ReplaceA03; ReplaceB03 = ReplaceA04; ReplaceB04 = ReplaceA05; ReplaceB05 = ReplaceA02; ReplaceB06 = ReplaceA01;
                        break;
                    case 20:
                        ReplaceB01 = ReplaceA06; ReplaceB02 = ReplaceA04; ReplaceB03 = ReplaceA05; ReplaceB04 = ReplaceA02; ReplaceB05 = ReplaceA03; ReplaceB06 = ReplaceA01;
                        break;
                    case 21:
                        ReplaceB01 = ReplaceA06; ReplaceB02 = ReplaceA05; ReplaceB03 = ReplaceA02; ReplaceB04 = ReplaceA03; ReplaceB05 = ReplaceA04; ReplaceB06 = ReplaceA01;
                        break;
                    case 22:
                        ReplaceB01 = ReplaceA03; ReplaceB02 = ReplaceA04; ReplaceB03 = ReplaceA06; ReplaceB04 = ReplaceA01; ReplaceB05 = ReplaceA02; ReplaceB06 = ReplaceA03;
                        break;
                    case 23:
                        ReplaceB01 = ReplaceA04; ReplaceB02 = ReplaceA05; ReplaceB03 = ReplaceA06; ReplaceB04 = ReplaceA02; ReplaceB05 = ReplaceA03; ReplaceB06 = ReplaceA01;
                        break;
                    //case 4:
                    //    ReplaceB01 = ReplaceA01; ReplaceB02 = ReplaceA02; ReplaceB03 = ReplaceA03; ReplaceB04 = ReplaceA04; ReplaceB05 = ReplaceA05; ReplaceB06 = ReplaceA06;
                    //    break;
                    default:
                    break;
                }
                Boxes[0] = ReplaceB01; 
                Boxes[1] = ReplaceB02; 
                Boxes[2] = ReplaceB03; 
                Boxes[3] = ReplaceB04; 
                Boxes[4] = ReplaceB05; 
                Boxes[5] = ReplaceB06;
                //Boxes = { ReplaceB01, ReplaceB02, ReplaceB03, ReplaceB04, ReplaceB05, ReplaceB06 };
                //FunctTeX = string.Concat("\\int",BasicFunct[7 * Vvv],"x",BasicFunct[7 * Vvv+1]);
                //ArgumOnStart = BasicFunct[7 * Vvv+2];
                //FunctOnStart = BasicFunct[7 * Vvv+3];
                //FunctCalc = string.Concat(BasicFunct[7 * Vvv+4],"+int(",BasicFunct[7 * Vvv + 5],"t",BasicFunct[7 * Vvv+6],", t,", Convert.ToString(BasicFunct[7 * Vvv+2]), ", x)");
            }
        }

        public ITest CreateTest(int randomSeed)
        {
            ITest result = new IntFunctGa001();
            Random random = new Random(randomSeed);
            Data data = new Data(random);

            //Random random = new Random(randomSeed);
            //ITest test = new Diff();

            result.CheckBoxes = data.Boxes;

            result.Text = $"{data.Vvv}.{data.Vvw}: " +
            $"Отметьте замены, являющиеся стандартными для вычисления интеграла вида \\({data.FormOfIntA},\\)\r\n"+//;
            $"причем \\(a=<ans[0]:5>\\),  \\(b=<ans[1]:5>\\).";
            //$"{data.FunctCalc}, data.xMaxInv={data.xMaxInv}";
            //$"Wwx={data.Wwx}\r\n";
            //$"{data.Wwy}\r\n";
            //$"{data.FunctCalc}";
        
            //data.Boxes;// = { data.ReplaceB01, data.ReplaceB02, data.ReplaceB03, data.ReplaceB04, data.ReplaceB05, data.ReplaceB06 };
            result.CheckBoxes = data.Boxes;
            return result;
        }

        public int CheckAnswer(int randomSeed, Dictionary<string, string> answers)
        {
            int total;
            Random random = new Random(randomSeed);
            Data data = new Data(random);
            
            total=0;
            try
            {
                //if (answers["ans["+0+"]"] == data.aA.ToString()) total++;
                //if (answers["ans["+1+"]"] == data.aB.ToString()) total++;
                if (answers["ans[0]"] == data.aA.ToString()) total++;
                if (answers["ans[1]"] == data.aB.ToString()) total++;
                Console.WriteLine(total);
            }
            catch
            {
                total = 1000;
                // ignored
            }
            total = total + 0;
            if (answers.ContainsKey(data.ReplaceB01) && answers[data.ReplaceB01] == "on") 
            {
                if (data.ReplaceB01 == data.ReplaceAnsw) 
                {
                    total+=6;
                }
                else
                {
                    total+=-6;
                }
            }
            total = total + 0;
            if (answers[data.ReplaceB02] == "on") 
            {
                if (data.ReplaceB02 == data.ReplaceAnsw) 
                {
                    total+=6;
                }
                else
                {
                    total+=-6;
                }
            }
            total = total + 0;
            if (answers[data.ReplaceB03] == "on") 
            {
                if (data.ReplaceB03 == data.ReplaceAnsw) 
                {
                    total+=6;
                }
                else
                {
                    total+=-6;
                }
            }
            total = total + 0;
            if (answers[data.ReplaceB04] == "on") 
            {
                if (data.ReplaceB04 == data.ReplaceAnsw) 
                {
                    total+=6;
                }
                else
                {
                    total+=-6;
                }
            }
            total = total + 0;
            if (answers[data.ReplaceB05] == "on") 
            {
                if (data.ReplaceB05 == data.ReplaceAnsw) 
                {
                    total+=6;
                }
                else
                {
                    total+=-6;
                }
            }
            total = total+0;
            if (answers[data.ReplaceB06] == "on") 
            {
                if (data.ReplaceB06 == data.ReplaceAnsw) 
                {
                    total+=6;
                }
                else
                {
                    total+=-6;
                }
            }
            //if (data.ReplaceB01 == data.ReplaceAnsw && answers[data.ReplaceB01] == "on") total+=6;
            //if (data.ReplaceB02 == data.ReplaceAnsw && answers[data.ReplaceB02] == "on") total+=6;
            //if (data.ReplaceB03 == data.ReplaceAnsw && answers[data.ReplaceB03] == "on") total+=6;
            //if (data.ReplaceB04 == data.ReplaceAnsw && answers[data.ReplaceB04] == "on") total+=6;
            //if (data.ReplaceB05 == data.ReplaceAnsw && answers[data.ReplaceB05] == "on") total+=6;
            //if (data.ReplaceB06 == data.ReplaceAnsw && answers[data.ReplaceB06] == "on") total+=6;
            //if (data.ReplaceB01 != data.ReplaceAnsw && answers[data.ReplaceB01] == "on") total+=-10;
            //if (data.ReplaceB02 != data.ReplaceAnsw && answers[data.ReplaceB02] == "on") total+=-20;
            //if (data.ReplaceB03 != data.ReplaceAnsw && answers[data.ReplaceB03] == "on") total+=-30;
            //if (data.ReplaceB04 != data.ReplaceAnsw && answers[data.ReplaceB04] == "on") total+=-40;
            //if (data.ReplaceB05 != data.ReplaceAnsw && answers[data.ReplaceB05] == "on") total+=-50;
            //if (data.ReplaceB06 != data.ReplaceAnsw && answers[data.ReplaceB06] == "on") total+=-60;

            //if (answers.ContainsKey(data.ReplaceAnsw) && answers[data.ReplaceAnsw] == "on") total+=6;
            //if (answers.ContainsKey(data.ReplaceB02) && answers[data.ReplaceAnsw] == "on") total+=6;
            //if (answers.ContainsKey(data.ReplaceB03) && answers[data.ReplaceAnsw] == "on") total+=6;
            //if (answers.ContainsKey(data.ReplaceB04) && answers[data.ReplaceAnsw] == "on") total+=6;
            //if (answers.ContainsKey(data.ReplaceB05) && answers[data.ReplaceAnsw] == "on") total+=6;
            //if (answers.ContainsKey(data.ReplaceB06) && answers[data.ReplaceAnsw] == "on") total+=6;

            //if (answers.ContainsKey(data.ReplaceB01) && answers[data.ReplaceAnsw] == "on") total+=6;
            //if (answers.ContainsKey(data.ReplaceB02) && answers[data.ReplaceAnsw] == "on") total+=6;
            //if (answers.ContainsKey(data.ReplaceB03) && answers[data.ReplaceAnsw] == "on") total+=6;
            //if (answers.ContainsKey(data.ReplaceB04) && answers[data.ReplaceAnsw] == "on") total+=6;
            //if (answers.ContainsKey(data.ReplaceB05) && answers[data.ReplaceAnsw] == "on") total+=6;
            //if (answers.ContainsKey(data.ReplaceB06) && answers[data.ReplaceAnsw] == "on") total+=6;
            return total;

            //Expression expression = new Expression(data.FunctCalc); //new Expression("der(x^(1/3), x)");
            //Expression userExpression = new Expression(answers["exp"]);
            //total = 0;
            ////for (double i = -10; i < 10; i += 0.5)
            //for (double i = 0.1; i < 0.8; i += 0.2)
            ////for (double i = 1; i < 9; i += 1)
            //{
            //    //expression.addArguments(new Argument("x", i));
            //    expression.addArguments(new Argument("x", i/(data.xMaxInv+0.0)));
            //    //double expCorrect;
            //    //double userAns;
//
            //    try
            //    {
            //        if (userExpression.getExpressionString().Contains("int")) return 0;
//
            //        userExpression.addArguments(new Argument("x", i/(data.xMaxInv+0.0)));
//
            //        double expCorrect = expression.calculate();
            //        double userAns = userExpression.calculate();
            //        double results = expCorrect - userAns;
//
            //        if (Math.Abs(results) > 0.0001 || double.IsNaN(expCorrect) != double.IsNaN(userAns))
            //        {
            //            total = 0;
            //            return 0;
            //        }
            //    }
            //    catch
            //    {
            //        total = 0;
            //        return total;
            //    }
            //}
            //if (answers.ContainsKey("Нужна галочка") && answers["Нужна галочка"] == "on") total++;
            //if (!answers.ContainsKey("Не нужна")) total++;
        }

        public string Text { get; set; }
        public string[] CheckBoxes { get; set; }
        public List<Image> Pictures { get; set; }
        public int TimeLimitSeconds { get; set; } = 30;
        public bool IsHidden { get; set; } = false;
    }
}
