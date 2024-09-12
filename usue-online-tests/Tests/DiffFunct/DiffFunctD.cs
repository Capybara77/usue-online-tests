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
    public class DiffFunctD001 : ITestCreator, ITest, ITimeLimit, IHidden, ITestGroup
    {
        public int TestID { get; set; }
        public string GroupName { get; set; } = "Diff of function";
        public string Name { get; } = "Производная суперпозиции функций 04";
        public string Description { get; } = "Дифференцирование функций";
        public class Data
        {
            public string Wwx = "";
            //public string Wwy = "";
            public int Vvv, Vvw, kA, kB;
            string[] BasicFunct = new string[]
                {
                    //"{\\left(","\\right)}^{5}", "(",")^(5)", 
                    "\\sin{\\left(","\\right)}", "sin(",")", 
                    "\\cos{\\left(","\\right)}", "cos(",")",
                    "4^{","}", "4^(",")", 
                    "\\mbox{tg}\\,{\\left(","\\right)}", "tan(",")", 
                    "\\mbox{arctg}\\,{\\left(","\\right)}",  "atan(",")", 
                    "{\\left(","\\right)}^{2}", "(",")^(2)",
                    //"\\sqrt{\\left(","\\right)}", "sqrt(",")", 
                    //"\\arcsin{\\left(","\\right)}",  "asin(",")", 
                    //"\\arccos{\\left(","\\right)}",  "acos(",")"
                };
            string[] BasicFunctB = new string[]
                {
                    //"\\arcsin{(",")}",  "asin(",")", 
                    //"\\arccos{(",")}",  "acos(",")",
                    "{(",")}^{5}", "(",")^(5)", 
                    //"4^{","}", "4^(",")", 
                    "\\sin{(",")}", "sin(",")", 
                    "\\cos{(",")}", "cos(",")",
                    "\\mbox{tg}\\,{(",")}", "tan(",")", 
                    "\\mbox{arctg}\\,{(",")}",  "atan(",")", 
                    "{(",")}^{2}", "(",")^(2)",
                    //"\\sqrt{(",")}", "sqrt(",")"
                };
            string[] BasicFunctC = new string[]
                {
                    "\\sqrt[3]{x}", "der(x^(1/3), x)", "x^{3}", "der(x^3, x)", "\\sin{{x}}", "der(sin(x), x)", "2^{{x}}", "der(2^x, x)",
                    "\\sqrt{x}", "der(sqrt(x), x)", "\\arcsin{x}",  "der(asin(x), x)", "\\arccos{x}",  "der(acos(x), x)"
                };
            public string FunctTeX;
            public string FunctCalc;

            public Data(Random random)
            {
                CreateFunct(random);
            }

            public void CreateFunct(Random random)
            {
                Vvv = random.Next(6);
                Vvw = random.Next(6);
                kA = random.Next(-5,6);
                kB = random.Next(1,10);
                if (kA>0)
                {
                    kA = kA+1;
                }
                else
                {
                    kA = kA-2;
                }
                //Vvv = 1;
                FunctTeX = string.Concat(BasicFunct[4 * Vvv],BasicFunctB[4 * Vvw],Convert.ToString(kA,10),"x+",Convert.ToString(kB,10),BasicFunctB[4 * Vvw+1],BasicFunct[4 * Vvv+1]);
                FunctCalc = string.Concat("der(",BasicFunct[4 * Vvv + 2],BasicFunctB[4 * Vvw + 2],Convert.ToString(kA,10),"*x+",Convert.ToString(kB,10),BasicFunctB[4 * Vvw+3],BasicFunct[4 * Vvv+3],", x)");
            }
        }

        public ITest CreateTest(int randomSeed)
        {
            ITest result = new DiffFunctD001();
            Random random = new Random(randomSeed);
            Data data = new Data(random);

            //Random random = new Random(randomSeed);
            //ITest test = new Diff();

            result.Text = //$"{data.Vvv}: " +
            $"Вычислите \\(\\left({data.FunctTeX}\\right)' = <exp:30>\\)\r\n" +
            //$"{data.FunctCalc}\r\n" +
            $"\\(\\ln{{x}}\\) запишите как \\(\\texttt{{ln(x)}},\\quad\\) \\(\\log_{{a}}{{x}}\\) запишите как \\(\\texttt{{ln(x)/ln(a)}},\\quad\\)\r\n " +
            $"\\(a^{{x}}\\) запишите как \\(\\texttt{{a^x}},\\quad\\) \\(\\sqrt{{x}}\\) запишите как \\(\\texttt{{sqrt(x)}},\\quad\\)" +
            $"\\(\\sqrt[a]{{x}}\\) запишите как \\(\\texttt{{sqrt[a](x)}}\\) или как \\(\\texttt{{x^(1/a)}},\\quad\\)\r\n" +
            $" \\(\\sin{{x}}\\) запишите как \\(\\texttt{{sin(x)}},\\quad\\) \\(\\cos{{x}}\\) запишите как \\(\\texttt{{cos(x)}},\\quad\\)\r\n" +
            $" \\(\\arcsin{{x}}\\) запишите как \\(\\texttt{{asin(x)}},\\quad\\) \\(\\arccos{{x}}\\) запишите как \\(\\texttt{{acos(x)}},\\quad\\)" +
            $" \\(\\textrm{{arctg}}\\,{{x}}\\) запишите как \\(\\texttt{{atan(x)}}.\\quad\\)\r\n ";
            //$"Wwx={data.Wwx}\r\n";
            //$"{data.Wwy}\r\n";
            //$"{data.FunctCalc}";
            return result;
        }

        public int CheckAnswer(int randomSeed, Dictionary<string, string> answers)
        {
            int total;
            Random random = new Random(randomSeed);
            Data data = new Data(random);
            Expression expression = new Expression(data.FunctCalc); //new Expression("der(x^(1/3), x)");
            Expression userExpression = new Expression(answers["exp"]);
            total = 1;
            //for (double i = -10; i < 10; i += 0.5)
            for (double i = 0.1; i < 1; i += 0.2)
            {
                //expression.addArguments(new Argument("x", i));
                expression.addArguments(new Argument("x", i));
                //double expCorrect;
                //double userAns;

                try
                {
                    if (userExpression.getExpressionString().Contains("der")) return 0;

                    userExpression.addArguments(new Argument("x", i));

                    double expCorrect = expression.calculate();
                    double userAns = userExpression.calculate();
                    double results = expCorrect - userAns;

                    if (Math.Abs(results) > 0.0001 || double.IsNaN(expCorrect) != double.IsNaN(userAns))
                    {
                        total = 0;
                        return 0;
                    }
                }
                catch
                {
                    total = 0;
                    return total;
                }
            }
            return total;
        }

        public string Text { get; set; }
        public string[] CheckBoxes { get; set; }
        public List<Image> Pictures { get; set; }
        public int TimeLimitSeconds { get; set; } = 90;
        public bool IsHidden { get; set; } = false;
    }
}
