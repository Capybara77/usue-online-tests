using System;
using System.Collections.Generic;
using System.IO;
using org.mariuszgromada.math.mxparser;
using Test_Wrapper;

namespace usue_online_tests.Tests.List
{
    public class Diff : ITestCreator, ITest
    {
        public int TestID { get; set; }
        public string Name { get; } = "Дифференцирование";
        public string Description { get; }
        public ITest CreateTest(int randomSeed)
        {
            Random random = new Random(randomSeed);
            ITest test = new Diff();

            test.Text = "Вычислите \\((\\sqrt[3]{x})' = <exp:30>\\)";
            return test;
        }

        public int CheckAnswer(int randomSeed, Dictionary<string, string> answers)
        {
            //    1/(3*(x^(2/3)))
            // Создаем выражение, которое определено в задаче (der - дифференцирование)
            Expression expression = new Expression("der(x^(1/3), x)");
            // Добавляем значение аргументу x = 5 (на этой точке будет проверять правильность)
            expression.addArguments(new Argument("x", 5));


            // Блок try нужен, чтобы даже если пользовать введет неправильные данные для выражения, генератор бы не выдавал ошибок
            try
            {
                // Создаем выражение для пользователя
                Expression userExpression = new Expression(answers["exp"]);

                // Проверяем использовал ли он в нем der, тоесть он мог бы просто передать исходное выражение и обернуть его в der, чтобы не решать
                if (userExpression.getExpressionString().Contains("der")) return 0;

                // Добавляем значение аргумента x = 5
                userExpression.addArguments(new Argument("x", 5));

                // Вычисляем значение обоих выражений и ищем разность (в теории, если все правильно, то она должна быть равна 0)
                double results = expression.calculate() - userExpression.calculate();

                // НО!! Важно проверить значение с учетом погрешности, метод Math.Abs() выдает значение по модулю, а дальше проверяю меньше ли погрешности в 0,001
                // В ЯП есть некоторые нюансы с работой со значениями с плавающей точкой - некоторые операции выдают неточные значения, например может быть так - 0.2 + 0.4 = 0.6000000000000001, поэтому важно делать такую проверку
                if (Math.Abs(results) < 0.001)
                {
                    return 1;
                }
            }
            catch
            {
                
            }

            return 0;
        }

        public string Text { get; set; }
        public string[] CheckBoxes { get; set; }
        public List<MemoryStream> Pictures { get; set; }
    }
}
