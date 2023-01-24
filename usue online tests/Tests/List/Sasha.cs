using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using Microsoft.CodeAnalysis;
using Test_Wrapper;

namespace UserTest
{
    public class RelatLS01del : ITestCreator, ITest, ITimeLimit, IHidden
    {
        public int TestID { get; set; }
        public string Name { get; } = "Отношения и предикаты. (не выбирать)";
        public string Description { get; } = "";

        public class Data
        {
            public string Vectors { get; set; }
            public string Vals { get; set; }
        }

        private Data[] datas = {
            new() { Vectors = "pqr", Vals = "abcde" },
            new() { Vectors = "uvw", Vals = "abcde" },
            new() { Vectors = "ijkm", Vals = "defgh" },
            new() { Vectors = "pqrs", Vals = "defgh" }
        };

        public ITest CreateTest(int randomSeed)
        {
            ITest result = new RelatLS01();
            Random random = new Random(randomSeed);

            int id = random.Next(4);
            string task = id < 2
                ? $"\\(<vec1>*<val1>+<vec2>*<val2>+<vec3>*<val3> = <zero1> \u21d2 \\)\r\n\\( \u21d2 <val21> = <val22> = <val23> = <zero2> \\)"
                : $"\\(<vec1>*<val1>+<vec2>*<val2>+<vec3>*<val3>+<vec4>*<val4> = <zero1> \u21d2 \\)\r\n\\( \u21d2 <val21> = <val22> = <val23> = <val24> = <zero2> \\)";

            result.Text = $"Заполните поля для ввода, записав характеристическое свойство из определения линейной независимости системы векторов {string.Join(", ", datas[id].Vectors.ToArray())} (коэффициенты: {string.Join("; ", datas[id].Vals.ToArray())} ...)\r\n" +
                          task;

            return result;
        }

        public int CheckAnswer(int randomSeed, Dictionary<string, string> answers)
        {
            Random random = new Random(randomSeed);
            int id = random.Next(4);

            string[] inputs = { "vec1", "vec2", "vec3", "vec4", "val1", "val2", "val3", "val4",
                "val21", "val22", "val23", "val24", "zero1", "zero2" };
            Data data = datas[id];

            int total = 0;

            for (int i = 0; i < inputs.Length; i++)
            {
                try
                {
                    total = i switch
                    {
                        < 4 => CheckInput(answers, inputs, i, data.Vectors[i], total),
                        < 8 => CheckInput(answers, inputs, i, data.Vals[i - 4], total),
                        < 12 => CheckInput(answers, inputs, i, data.Vals[i - 8], total),
                        < 14 => CheckInput(answers, inputs, i, '0', total),
                        _ => total
                    };
                }
                catch { }

            }

            return total;
        }

        private static int CheckInput(Dictionary<string, string> answers, string[] inputs, int i, char correctChar, int total)
        {
            if (answers[inputs[i]] == correctChar.ToString())
                total++;

            return total;
        }

        public string Text { get; set; }
        public string[] CheckBoxes { get; set; }
        public List<Image> Pictures { get; set; }
        public int TimeLimitSeconds { get; set; } = 60;
        public bool IsHidden { get; set; } = true;
    }
}