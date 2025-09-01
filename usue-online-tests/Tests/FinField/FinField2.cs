using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using org.mariuszgromada.math.mxparser;
using Test_Wrapper;

namespace FinField
{
	public class FinField2 : ITest, ITestCreator
	{
		public string Text { get; set; }
		public string[] CheckBoxes { get; set; }
		public List<MemoryStream> Pictures { get; set; }
		public int TestID { get; set; }
		public string Name { get; } = "Конечные поля: тест 2 ";
		public string Description { get; }

		public class Data
		{
			public string Question { get; set; }
			public string[] Answer { get; set; }
			public string AnswerString { get; set; }
		}

		public string Alph { get; set; } = "abcdfghijklmnopqrstuvwxyz";

		public Data[] Equations { get; set; } =
		{
			new()
			{
				Question = "2𝑥^3+𝑥+1",
				AnswerString = "a)\\((𝑓 + 𝑔𝑎 + ℎ𝑎^2)︀+(︀𝑘 + 𝑚𝑎 + 𝑛𝑎^2)︀ = <ans0>\\)" + Environment.NewLine +
				               "б)\\(𝑎^3 = <ans1>\\)" + Environment.NewLine +
				               "в)\\(𝑎^4 = <ans2>\\)" + Environment.NewLine +
				               "г)\\(𝑓 + 𝑔𝑎 + ℎ𝑎^2)︀(︀𝑘 + 𝑚𝑎 + 𝑛𝑎^2)︀ = <ans3>\\)" + Environment.NewLine,
				Answer = new[]
				{
					"f+k+(g+m)*a+(h+n)*a*a", "1+1*a+0*a^2", "0+1*a+1*a^2",
					"f*k+1*(g*n+h*m)+h*n*0+(f*m+g*k+1*(g*n+h*m)+1*h*n)*a+(f*n+g*m+h*k+0*(g*n+h*m)+1*h*n)*a*a"
				}
			},
			new()
			{
				Question = "2𝑥^3+2𝑥^2+1",
				AnswerString = "a)\\((︀𝑢 + 𝑣𝑎 + 𝑤𝑎^2)︀+(︀𝑝 + 𝑞𝑎 + 𝑟𝑎^2)︀ = <ans0>\\)" + Environment.NewLine +
				               "б)\\(𝑎^3 = <ans1>\\)" + Environment.NewLine +
				               "в)\\(𝑎^4 = <ans2>\\)" + Environment.NewLine +
				               "г)\\((︀𝑢 + 𝑣𝑎 + 𝑤𝑎^2)︀(︀𝑝 + 𝑞𝑎 + 𝑟𝑎^2)︀ = <ans3>\\)" + Environment.NewLine,
				Answer = new[]
				{
					"u+p+(v+q)*a+(w+r)*a*a", "1+0*a+2*a^2", "2+1*a+1*a^2",
					"u*p+1*(v*r+w*q)+w*r*2+(u*q+v*p+0*(v*r+w*q)+1*w*r)*a+(u*r+v*q+w*p+2*(v*r+w*q)+1*w*r)*a*a"
				}
			},
			new()
			{
				Question = "2𝑥^3+𝑥+1",
				AnswerString = "a)\\((︀𝑑 + 𝑒𝑎 + 𝑓𝑎^2)︀+(︀𝑟 + 𝑠𝑎 + 𝑡𝑎^2)︀ = <ans0>\\)" + Environment.NewLine +
				               "б)\\(𝑎^3 = <ans1>\\)" + Environment.NewLine +
				               "в)\\(𝑎^4 = <ans2>\\)" + Environment.NewLine +
				               "г)\\((︀𝑑 + 𝑒𝑎 + 𝑓𝑎^2)︀(︀𝑟 + 𝑠𝑎 + 𝑡𝑎^2)︀ = <ans3>\\)" + Environment.NewLine,
				Answer = new[]
				{
					"d+r+(e+s)*a+(f+t)*a*a", "1+1*a+0*a^2", "0+1*a+1*a^2",
					"d*r+1*(e*t+f*s)+f*t*0+(d*s+e*r+1*(e*t+f*s)+1*f*t)*a+(d*t+e*s+f*r+0*(e*t+f*s)+1*f*t)*a*a"
				}
			},
			new()
			{
				Question = "2𝑥^3+𝑥^2+2",
				AnswerString = "a)\\((︀𝑘 + 𝑚𝑎 + 𝑛𝑎^2)︀+(︀𝑓 + 𝑔𝑎 + ℎ𝑎^2)︀ = <ans0>\\)" + Environment.NewLine +
				               "б)\\(𝑎^3 = <ans1>\\)" + Environment.NewLine +
				               "в)\\(𝑎^4 = <ans2>\\)" + Environment.NewLine +
				               "г)\\((︀𝑘 + 𝑚𝑎 + 𝑛𝑎^2)︀(︀𝑓 + 𝑔𝑎 + ℎ𝑎^2)︀ = <ans3>\\)" + Environment.NewLine,
				Answer = new[]
				{
					"k+f+(m+g)*a+(n+h)*a*a", "2+0*a+1*a^2", "2+2*a+1*a^2",
					"k*f+2*(m*h+n*g)+n*h*2+(k*g+m*f+0*(m*h+n*g)+2*n*h)*a+(k*h+m*g+n*f+1*(m*h+n*g)+1*n*h)*a*a"
				}
			},
			new()
			{
				Question = "2𝑥^3+𝑥+2",
				AnswerString = "a)\\((︀𝑓 + 𝑔𝑎 + ℎ𝑎^2)︀+(︀𝑘 + 𝑚𝑎 + 𝑛𝑎^2)︀ = <ans0>\\)" + Environment.NewLine +
				               "б)\\(𝑎^3 = <ans1>\\)" + Environment.NewLine +
				               "в)\\(𝑎^4 = <ans2>\\)" + Environment.NewLine +
				               "г)\\((︀𝑓 + 𝑔𝑎 + ℎ𝑎^2)︀(︀𝑘 + 𝑚𝑎 + 𝑛𝑎^2)︀ = <ans3>\\)" + Environment.NewLine,
				Answer = new[]
				{
					"f+k+(g+m)*a+(h+n)*a*a", "2+1*a+0*a^2", "0+2*a+1*a^2",
					"f*k+2*(g*n+h*m)+h*n*0+(f*m+g*k+1*(g*n+h*m)+2*h*n)*a+(f*n+g*m+h*k+0*(g*n+h*m)+1*h*n)*a*a"
				}
			},
			new()
			{
				Question = "2𝑥^3+𝑥+1",
				AnswerString = "a)\\((︀𝑢 + 𝑣𝑎 + 𝑤𝑎^2)︀+(︀𝑝 + 𝑞𝑎 + 𝑟𝑎^2)︀ = <ans0>\\)" + Environment.NewLine +
				               "б)\\(𝑎^3 = <ans1>\\)" + Environment.NewLine +
				               "в)\\(𝑎^4 = <ans2>\\)" + Environment.NewLine +
				               "г)\\((︀𝑢 + 𝑣𝑎 + 𝑤𝑎^2)︀(︀𝑝 + 𝑞𝑎 + 𝑟𝑎^2)︀ = <ans3>\\)" + Environment.NewLine,
				Answer = new[]
				{
					"u+p+(v+q)*a+(w+r)*a*a", "1+1*a+0*a^2", "0+1*a+1*a^2",
					"u*p+1*(v*r+w*q)+w*r*0+(u*q+v*p+1*(v*r+w*q)+1*w*r)*a+(u*r+v*q+w*p+0*(v*r+w*q)+1*w*r)*a*a"
				}
			},
			new()
			{
				Question = "2𝑥^3+2𝑥^2+𝑥+2",
				AnswerString = "a)\\((︀𝑝 + 𝑞𝑎 + 𝑟𝑎^2)︀+(︀𝑢 + 𝑣𝑎 + 𝑤𝑎^2)︀ = <ans0>\\)" + Environment.NewLine +
				               "б)\\(𝑎^3 = <ans1>\\)" + Environment.NewLine +
				               "в)\\(𝑎^4 = <ans2>\\)" + Environment.NewLine +
				               "г)\\((︀𝑝 + 𝑞𝑎 + 𝑟𝑎^2)︀(︀𝑢 + 𝑣𝑎 + 𝑤𝑎^2)︀ = <ans3>\\)" + Environment.NewLine,
				Answer = new[]
				{
					"p+u+(q+v)*a+(r+w)*a*a", "2+1*a+2*a^2", "1+1*a+2*a^2",
					"p*u+2*(q*w+r*v)+r*w*1+(p*v+q*u+1*(q*w+r*v)+1*r*w)*a+(p*w+q*v+r*u+2*(q*w+r*v)+2*r*w)*a*a"
				}
			},
			new()
			{
				Question = "2𝑥^3+𝑥^2+𝑥+1",
				AnswerString = "a)\\((︀𝑟 + 𝑠𝑎 + 𝑡𝑎^2)︀+(︀𝑑 + 𝑒𝑎 + 𝑓𝑎^2)︀ = <ans0>\\)" + Environment.NewLine +
				               "б)\\(𝑎^3 = <ans1>\\)" + Environment.NewLine +
				               "в)\\(𝑎^4 = <ans2>\\)" + Environment.NewLine +
				               "г)\\((︀𝑟 + 𝑠𝑎 + 𝑡𝑎^2)︀(︀𝑑 + 𝑒𝑎 + 𝑓𝑎^2)︀ = <ans3>\\)" + Environment.NewLine,
				Answer = new[]
				{
					"r+d+(s+e)*a+(t+f)*a*a", "1+1*a+1*a^2", "1+2*a+2*a^2",
					"r*d+1*(s*f+t*e)+t*f*1+(r*e+s*d+1*(s*f+t*e)+2*t*f)*a+(r*f+s*e+t*d+1*(s*f+t*e)+2*t*f)*a*a"
				}
			},
			new()
			{
				Question = "2𝑥^3+2𝑥^2+1",
				AnswerString = "a)\\((︀𝑑 + 𝑒𝑎 + 𝑓𝑎^2)︀+(︀𝑟 + 𝑠𝑎 + 𝑡𝑎^2)︀ = <ans0>\\)" + Environment.NewLine +
				               "б)\\(𝑎^3 = <ans1>\\)" + Environment.NewLine +
				               "в)\\(𝑎^4 = <ans2>\\)" + Environment.NewLine +
				               "г)\\((︀𝑑 + 𝑒𝑎 + 𝑓𝑎^2)︀(︀𝑟 + 𝑠𝑎 + 𝑡𝑎^2)︀ = <ans3>\\)" + Environment.NewLine,
				Answer = new[]
				{
					"d+r+(e+s)*a+(f+t)*a*a", "1+0*a+2*a^2", "2+1*a+1*a^2",
					"d*r+1*(e*t+f*s)+f*t*2+(d*s+e*r+0*(e*t+f*s)+1*f*t)*a+(d*t+e*s+f*r+2*(e*t+f*s)+1*f*t)*a*a"
				}
			},
			new()
			{
				Question = "2𝑥^3+𝑥^2+2",
				AnswerString = "a)\\((︀𝑟 + 𝑠𝑎 + 𝑡𝑎^2)︀+(︀𝑑 + 𝑒𝑎 + 𝑓𝑎^2)︀ = <ans0>\\)" + Environment.NewLine +
				               "б)\\(𝑎^3 = <ans1>\\)" + Environment.NewLine +
				               "в)\\(𝑎^4 = <ans2>\\)" + Environment.NewLine +
				               "г)\\((︀𝑟 + 𝑠𝑎 + 𝑡𝑎^2)︀+(︀𝑑 + 𝑒𝑎 + 𝑓𝑎^2)︀ = <ans3>\\)" + Environment.NewLine,
				Answer = new[]
				{
					"r+d+(s+e)*a+(t+f)*a*a", "2+0*a+1*a^2", "2+2*a+1*a^2",
					"r*d+2*(s*f+t*e)+t*f*2+(r*e+s*d+0*(s*f+t*e)+2*t*f)*a+(r*f+s*e+t*d+1*(s*f+t*e)+1*t*f)*a*a"
				}
			}
		};


		public ITest CreateTest(int randomSeed)
		{
			ITest result = new FinField2();

			var random = new Random(randomSeed);

			var randomNumber = random.Next(Equations.Length);

			var question =
				$"Пусть 𝑎 — корень многочлена \\({Equations[randomNumber].Question}\\) с коэффициентами из поля 𝐺𝐹(3) = {{0, 1, 2}}. Тогда"
				+ Environment.NewLine +
				Equations[randomNumber].AnswerString;

			result.Text = question;

			return result;
		}

		public int CheckAnswer(int randomSeed, Dictionary<string, string> answers)
		{
			var random = new Random(randomSeed);

			var randomNumber = random.Next(Equations.Length);

			Expression expTrueAnswer;
			Expression expUserAnswer;

			var corr = 0;

			for (var i = 0; i < Equations[randomNumber].Answer.Length; i++)
			{
				expTrueAnswer = new Expression(Equations[randomNumber].Answer[i]);
				expUserAnswer = new Expression(answers[$"ans{i}"]);

				var newRandomNumber = random.Next(100);

				foreach (var chr in Alph)
				{
					if (expTrueAnswer.getExpressionString().Contains(chr))
						expTrueAnswer.addArguments(new Argument($"{chr}", newRandomNumber));
					if (expUserAnswer.getExpressionString().Contains(chr))
						expUserAnswer.addArguments(new Argument($"{chr}", newRandomNumber));
				}

				var ans1 = expTrueAnswer.calculate();
				var ans2 = expUserAnswer.calculate();

				if (Math.Abs(ans1 - ans2) < 0.001) corr++;
			}


			return corr;
		}
	}
}