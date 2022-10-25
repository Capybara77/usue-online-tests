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
    public class Diff : ITestCreator, ITest, ITestGroup
    {
        public int TestID { get; set; }
        public string Name { get; } = "Дифференцирование";
        public string Description { get; }
        public ITest CreateTest(int randomSeed)
        {
            Random random = new Random(randomSeed);
            ITest test = new Diff();

            test.Text = "Вычислите \\((\\sqrt[3]{x})' = <exp>\\)";
            return test;
        }

        public int CheckAnswer(int randomSeed, Dictionary<string, string> answers)
        {
            //    1/(3*(x^(2/3)))
            Expression expression = new Expression("der(x^(1/3), x)");
            expression.addArguments(new Argument("x", 5));

            try
            {
                Expression userExpression = new Expression(answers["exp"]);

                if (userExpression.getExpressionString().Contains("der")) return 0;

                userExpression.addArguments(new Argument("x", 5));

                if (Math.Abs(expression.calculate() - userExpression.calculate()) < 0.001) return 1;
            }
            catch
            {
                
            }

            return 0;
        }

        public string Text { get; set; }
        public string[] CheckBoxes { get; set; }
        public List<Image> Pictures { get; set; }
        public string GroupName { get; set; } = "group name";
    }
}
