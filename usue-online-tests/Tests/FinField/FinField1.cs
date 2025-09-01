using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using Test_Wrapper;

namespace FinField
{
	public class FinField1 : ITestCreator, ITest
	{
		public int TestID { get; set; }
		public string Name { get; } = "Конечные поля: тест 1 ";
		public string Description { get; }
		public string Text { get; set; }
		public string[] CheckBoxes { get; set; }
		public List<Image> Pictures { get; set; }

		public class Data
		{
			public string Question { get; set; }
			public string[] Answer { get; set; }
			public string AnswerString { get; set; }
		}

		public Data[][] Equations { get; set; } =
		{
			new[]
			{
				new()
				{
					Question = "2𝑎^3+𝑎+1", Answer = new[] { "1", "0", "2" },
					AnswerString = "\\(𝑎^{12}\\) =  \\(<ans0>a^2+<ans1>a+<ans2>\\)"
				},
				new Data
				{
					Question = "2𝑎^3+𝑎^2+𝑎+1", Answer = new[] { "1", "1", "1" },
					AnswerString = "\\(𝑎^{16}\\) =  \\(<ans3>a^2+<ans4>a+<ans5>\\)"
				},
				new Data
				{
					Question = "2𝑎^3+2𝑎^2+1", Answer = new[] { "1", "2", "1" },
					AnswerString = "\\(𝑎^{22}\\) =  \\(<ans6>a^2+<ans7>a+<ans8>\\)"
				}
			},
			new[]
			{
				new Data
				{
					Question = "2𝑎^3+𝑎^2+2", Answer = new[] { "0", "1", "0" },
					AnswerString = "\\(𝑎^{1}\\) =  \\(<ans0>a^2+<ans1>a+<ans2>\\)"
				},
				new Data
				{
					Question = "2𝑎^3+𝑎^2+2𝑎+2", Answer = new[] { "1", "2", "0" },
					AnswerString = "\\(𝑎^{5}\\) =  \\(<ans3>a^2+<ans4>a+<ans5>\\)"
				},
				new Data
				{
					Question = "2𝑎^3+2𝑎^2+𝑎+2", Answer = new[] { "1", "2", "0" },
					AnswerString = "\\(𝑎^{11}\\) =  \\(<ans6>a^2+<ans7>a+<ans8>\\)"
				}
			},
			new[]
			{
				new Data
				{
					Question = "2𝑎^3+𝑎^2+2𝑎+2", Answer = new[] { "1", "0", "0" },
					AnswerString = "\\(𝑎^{2}\\) =  \\(<ans0>a^2+<ans1>a+<ans2>\\)"
				},
				new Data
				{
					Question = "2𝑎^3+𝑎^2+𝑎+2", Answer = new[] { "1", "0", "1" },
					AnswerString = "\\(𝑎^{6}\\) =  \\(<ans3>a^2+<ans4>a+<ans5>\\)"
				},
				new Data
				{
					Question = "2𝑎^3+𝑎+1", Answer = new[] { "1", "0", "2" },
					AnswerString = "\\(𝑎^{12}\\) =  \\(<ans6>a^2+<ans7>a+<ans8>\\)"
				}
			},
			new[]
			{
				new Data
				{
					Question = "2𝑎^3+𝑎+1", Answer = new[] { "1", "1", "0" },
					AnswerString = "\\(𝑎^{4}\\) =  \\(<ans0>a^2+<ans1>a+<ans2>\\)"
				},
				new Data
				{
					Question = "2𝑎^3+𝑎^2+𝑎+1", Answer = new[] { "0", "2", "1" },
					AnswerString = "\\(𝑎^{8}\\) =  \\(<ans3>a^2+<ans4>a+<ans5>\\)"
				},
				new Data
				{
					Question = "2𝑎^3+2𝑎^2+1", Answer = new[] { "0", "1", "0" },
					AnswerString = "\\(𝑎^{14}\\) =  \\(<ans6>a^2+<ans7>a+<ans8>\\)"
				}
			},
			new[]
			{
				new Data
				{
					Question = "2𝑎^3+𝑎^2+𝑎+1", Answer = new[] { "1", "0", "2" },
					AnswerString = "\\(𝑎^{5}\\) =  \\(<ans0>a^2+<ans1>a+<ans2>\\)"
				},
				new Data
				{
					Question = "2𝑎^3+2𝑎^2+1", Answer = new[] { "1", "2", "1" },
					AnswerString = "\\(𝑎^{9}\\) =  \\(<ans3>a^2+<ans4>a+<ans5>\\)"
				},
				new Data
				{
					Question = "2𝑎^3+2𝑎^2+2𝑎+1", Answer = new[] { "1", "0", "0" },
					AnswerString = "\\(𝑎^{15}\\) =  \\(<ans6>a^2+<ans7>a+<ans8>\\)"
				}
			},
			new[]
			{
				new Data
				{
					Question = "2𝑎^3+𝑎^2+2", Answer = new[] { "2", "1", "1" },
					AnswerString = "\\(𝑎^{17}\\) =  \\(<ans0>a^2+<ans1>a+<ans2>\\)"
				},
				new Data
				{
					Question = "2𝑎^3+𝑎^2+2𝑎+2", Answer = new[] { "2", "2", "2" },
					AnswerString = "\\(𝑎^{21}\\) =  \\(<ans3>a^2+<ans4>a+<ans5>\\)"
				},
				new Data
				{
					Question = "2𝑎^3+2𝑎^2+𝑎+2", Answer = new[] { "0", "0", "0" },
					AnswerString = "\\(𝑎^{27}\\) =  \\(<ans6>a^2+<ans7>a+<ans8>\\)"
				}
			},
			new[]
			{
				new Data
				{
					Question = "2𝑎^3+𝑎+1", Answer = new[] { "2", "2", "1" },
					AnswerString = "\\(𝑎^{20}\\) =  \\(<ans0>a^2+<ans1>a+<ans2>\\)"
				},
				new Data
				{
					Question = "2𝑎^3+𝑎^2+𝑎+1", Answer = new[] { "2", "2", "0" },
					AnswerString = "\\(𝑎^{24}\\) =  \\(<ans3>a^2+<ans4>a+<ans5>\\)"
				},
				new Data
				{
					Question = "2𝑎^3+2𝑎^2+1", Answer = new[] { "2", "0", "1" },
					AnswerString = "\\(𝑎^{3}\\) =  \\(<ans6>a^2+<ans7>a+<ans8>\\)"
				}
			},
			new[]
			{
				new Data
				{
					Question = "2𝑎^3+2𝑎^2+1", Answer = new[] { "2", "1", "0" },
					AnswerString = "\\(𝑎^{6}\\) =  \\(<ans0>a^2+<ans1>a+<ans2>\\)"
				},
				new Data
				{
					Question = "2𝑎^3+2𝑎^2+2𝑎+1", Answer = new[] { "2", "0", "1" },
					AnswerString = "\\(𝑎^{10}\\) =  \\(<ans3>a^2+<ans4>a+<ans5>\\)"
				},
				new Data
				{
					Question = "2𝑎^3+𝑎+2", Answer = new[] { "0", "2", "1" },
					AnswerString = "\\(𝑎^{16}\\) =  \\(<ans6>a^2+<ans7>a+<ans8>\\)"
				}
			},
			new[]
			{
				new Data
				{
					Question = "2𝑎^3+𝑎^2+2", Answer = new[] { "2", "2", "2" },
					AnswerString = "\\(𝑎^{9}\\) =  \\(<ans0>a^2+<ans1>a+<ans2>\\)"
				},
				new Data
				{
					Question = "2𝑎^3+𝑎^2+2𝑎+2", Answer = new[] { "0", "0", "2" },
					AnswerString = "\\(𝑎^{13}\\) =  \\(<ans3>a^2+<ans4>a+<ans5>\\)"
				},
				new Data
				{
					Question = "2𝑎^3+2𝑎^2+𝑎+2", Answer = new[] { "2", "0", "2" },
					AnswerString = "\\(𝑎^{19}\\) =  \\(<ans6>a^2+<ans7>a+<ans8>\\)"
				}
			},
			new[]
			{
				new Data
				{
					Question = "2𝑎^3+2𝑎^2+2𝑎+1", Answer = new[] { "1", "2", "0" },
					AnswerString = "\\(𝑎^{7}\\) =  \\(<ans0>a^2+<ans1>a+<ans2>\\)"
				},
				new Data
				{
					Question = "2𝑎^3+𝑎+2", Answer = new[] { "1", "1", "2" },
					AnswerString = "\\(𝑎^{11}\\) =  \\(<ans3>a^2+<ans4>a+<ans5>\\)"
				},
				new Data
				{
					Question = "2𝑎^3+𝑎^2+2", Answer = new[] { "2", "1", "1" },
					AnswerString = "\\(𝑎^{17}\\) =  \\(<ans6>a^2+<ans7>a+<ans8>\\)"
				}
			}
		};

		public ITest CreateTest(int randomSeed)
		{
			var random = new Random(randomSeed);

			var randomNumber = random.Next(Equations.Length);

			ITest result = new FinField1();

			var sb = new StringBuilder();

			for (var i = 0; i < Equations[randomNumber].Length; i++)
				sb.Append("Элемент \\(a\\) является корнем уравнения " +
				          $"\\({Equations[randomNumber][i].Question}\\) с коэффициентами из поля " +
				          "\\(GF(3) = \\{ 0, 1, 2 \\}\\). Тогда " +
				          $"{Equations[randomNumber][i].AnswerString}."
				          + Environment.NewLine);


			result.Text = sb.ToString();
			return result;
		}

		public int CheckAnswer(int randomSeed, Dictionary<string, string> answers)
		{
			var random = new Random(randomSeed);
			var randomNumber = random.Next(Equations.Length);

			var correct = 0;

			try
			{
				for (var i = 0; i < Equations[randomNumber].Length; i++)
				for (var j = 0; j < Equations[randomNumber][i].Answer.Length; j++)
					if (answers[$"ans{i * 3 + j}"] == Equations[randomNumber][i].Answer[j])
						correct++;
			}
			catch
			{
				// ignored
			}

			return correct;
		}
	}
}