using System;
using System.Collections.Generic;
using System.Drawing;
using Test_Wrapper;

namespace UserTest
{
    public class RelatLS02 : ITestCreator, ITest, ITimeLimit, IHidden
    {
        public bool IsHidden { get; set; } = true;
        public int TestID { get; set; }
        public string Name { get; } = "Отношения и предикаты. Линейные пространства. Характеристическое свойство для Л.П. над R из определения линейной зависимости";
        public string Description { get; } = "Характеристическое свойство для Л.П. над R из определения линейной зависимости";

        public class Data
        {
            public string answer { get; set; } = "2431";
            public string[] textstr { get; set; } = { 
                "{𝑢, 𝑣, 𝑤}",
                "{𝑎, 𝑏, 𝑐}",
                "{𝑝, 𝑞, 𝑟}",
                "{𝑟, 𝑠, 𝑡}" };
            public string[] str { get; set; } = {
            "1) 𝛼𝑢+𝛽𝑣+𝛾𝑤=0 ⇒ 𝛼=𝛽=𝛾=0; \r\n 2) 𝛼𝑢+𝛽𝑣+𝛾𝑤=0 и |𝛼|+|𝛽|+|𝛾|>0; \r\n 3) 𝛼𝑢+𝛽𝑣+𝛾𝑤=0 и 𝛼=𝛽=𝛾=0; \r\n 4) 𝛼𝑢+𝛽𝑣+𝛾𝑤=0 ⇒ |𝛼|+|𝛽|+|𝛾|>0." ,
            "1) 𝜆𝑎+𝜇𝑏+𝜈𝑐=0 ⇒ |𝜆|+|𝜇|+|𝜈|>0; \r\n 2) 𝜆𝑎+𝜇𝑏+𝜈𝑐=0 и 𝜆=𝜇=𝜈=0; \r\n 3) 𝜆𝑎+𝜇𝑏+𝜈𝑐=0 ⇒ 𝜆=𝜇=𝜈=0; \r\n 4) 𝜆𝑎+𝜇𝑏+𝜈𝑐=0 и |𝜆|+|𝜇|+|𝜈|>0." ,
            "1) 𝛼𝑝+𝛽𝑞+𝛾𝑟=0 ⇒ |𝛼|+|𝛽|+|𝛾|>0; \r\n 2) 𝛼𝑝+𝛽𝑞+𝛾𝑟=0 и 𝛼=𝛽=𝛾=0; \r\n 3) 𝛼𝑝+𝛽𝑞+𝛾𝑟=0 и |𝛼|+|𝛽|+|𝛾|>0; \r\n 4) 𝛼𝑝+𝛽𝑞+𝛾𝑟=0 ⇒ 𝛼=𝛽=𝛾=0." ,
            "1) 𝜆𝑟+𝜇𝑠+𝜈𝑡=0 и |𝜆|+|𝜇|+|𝜈|>0; \r\n 2) 𝜆𝑟+𝜇𝑠+𝜈𝑡=0 ⇒ 𝜆=𝜇=𝜈=0; \r\n 3) 𝜆𝑟+𝜇𝑠+𝜈𝑡=0 ⇒ |𝜆|+|𝜇|+|𝜈|>0; \r\n 4) 𝜆𝑟+𝜇𝑠+𝜈𝑡=0 и 𝜆=𝜇=𝜈=0." };
        }
        public ITest CreateTest(int randomSeed)
        {
            ITest result = new RelatLS02();

            Random random = new Random(randomSeed);
            Data data = new Data();
            int num = random.Next(4);

            result.Text = $"Для Л.П. над R характеристическое свойство из определения линейной зависимости {data.textstr[num]} отмечено номером \\(<ans>\\): \r\n {data.str[num]}";
            return result;
        }

        public int CheckAnswer(int randomSeed, Dictionary<string, string> answers)
        {
            Random random = new Random(randomSeed);
            Data data = new Data();

            try
            {
                if (answers.ContainsKey("ans")) return (data.answer[random.Next(4)] == Convert.ToChar(answers["ans"])) ? 1 : 0;
            }
            catch
            {
                // ignored
            }
            return 0;
        }

        public string Text { get; set; }
        public string[] CheckBoxes { get; set; }
        public List<Image> Pictures { get; set; }
        public int TimeLimitSeconds { get; set; } = 60;
    }
}
