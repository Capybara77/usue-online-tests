using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using Test_Wrapper;

namespace LinOper
{
	public class LinOper1 : ITest, ITestCreator
	{
		public string Text { get; set; }
		public string[] CheckBoxes { get; set; }
		public List<MemoryStream> Pictures { get; set; }
		public int TestID { get; set; }
		public string Name { get; } = "Линейные операторы тест 1";
		public string Description { get; }
		public string Alph1 { get; set; } = "ABCPQR";
		public string Alph2 { get; set; } = "efghkmnb";
		public ITest CreateTest(int randomSeed)
		{
			Random random = new Random(randomSeed);

			int keyAnswer = random.Next(1, 4);

			int rndNum = random.Next(Alph1.Length);

			char bigChar = Alph1[rndNum];
			char smallChar = Alph2[random.Next(Alph2.Length)];

			ITest result = new LinOper1();

			string question =
				$"Если \\({bigChar}_Б = (q_{{ij}})_{{3*3}} \\) — матрица линейного оператора \\(\\hat{{{bigChar}}}\\) в базисе \\(Б = \\left\\{{{smallChar}_1, {smallChar}_2, {smallChar}_3\\right\\}}\\), то"
			+ Environment.NewLine + $"\\(\\hat{{{bigChar}}}({smallChar}_{keyAnswer}) \\)= \\(q_{{<ans0><ans1>}}{smallChar}_1\\) + \\(q_{{<ans2><ans3>}}{smallChar}_2\\) + \\(q_{{<ans4><ans5>}}{smallChar}_3\\)";

			result.Text = question;
			return result;
		}

		public int CheckAnswer(int randomSeed, Dictionary<string, string> answers)
		{
			Random random = new Random(randomSeed);
			string keyAnswer = random.Next(1, 4).ToString();

			string[] answerBuild = { "1", keyAnswer, "2", keyAnswer, "3", keyAnswer};

			int corr = 0;

			try
			{
				for (int i = 0; i < answerBuild.Length; i++)
				{
					if (answers[$"ans{i}"] == answerBuild[i])
					{
						corr++;
					}
				}

			}
			catch
			{
			}

			return corr;
		}
	}
}
