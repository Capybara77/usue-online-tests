using System;
using System.Collections.Generic;
using System.Drawing;
using HtmlAgilityPack;
using Test_Wrapper;

namespace UserTest
{
    public class MyTask : ITestCreator, ITest
    {
        public int TestID { get; set; }
        public string Name { get; } = "Пример теста в отдельной dll";
        public string Description { get; }

        public string[] Boxes { get; set; } = new[] { "Нужна галочка", "Не нужна" };

        public ITest CreateTest(int randomSeed)
        {
            ITest result = new MyTask();

            HtmlAgilityPack.HtmlWeb web = new HtmlWeb();
            var doc = web.Load("https://vk.com/");
            

            int a = new Random(randomSeed).Next(10, 20);
            result.Text = $"В ответ запишите число {a}. \\(<number>\\){doc.DocumentNode.InnerHtml}";

            result.CheckBoxes = Boxes;

            return result;
        }

        public int CheckAnswer(int randomSeed, Dictionary<string, string> answers)
        {
            int total = 0;
            try
            {
                if (Convert.ToInt32(answers["number"]) == new Random(randomSeed).Next(10, 20)) total++;
            }
            catch
            {
                // ignored
            }

            if (answers.ContainsKey("Нужна галочка") && answers["Нужна галочка"] == "on") total++;
            if (!answers.ContainsKey("Не нужна")) total++;
            return total;
        }

        public string Text { get; set; }
        public string[] CheckBoxes { get; set; }
        public List<Image> Pictures { get; set; }
    }
}
