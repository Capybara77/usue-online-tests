using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using Test_Wrapper;

namespace LinOper
{
	public class LinOper2 : ITest, ITestCreator
	{
		public string Text { get; set; }
		public string[] CheckBoxes { get; set; }
		public List<Image> Pictures { get; set; }
		public int TestID { get; set; }
		public string Name { get; } = "Линейные операторы тест 2";
		public string Description { get; }

		public string BigCharAlphabet { get; set; } = "ABCPQR";
		public string SmallCharAlphabet { get; set; } = "efghkmnb";

		public int Option { get; set; }
		public int Alpha { get; set; }
		public int Beta { get; set; }
		public int Gamma { get; set; }
		public int Delta { get; set; }

		public string FormulaP { get; set; }
		public string VectorA { get; set; }
		public string VectorB { get; set; }
		public string VectorC { get; set; }

		public void ResetToDefault()
		{
			Option = default;
			Alpha = default;
			Beta = default;
			Gamma = default;
			Delta = default;

			FormulaP = default;
			VectorA = default;
			VectorB = default;
			VectorC = default;
		}

		public ITest CreateTest(int randomSeed)
		{
			ResetToDefault();

			Random random = new Random(randomSeed);
			Random randomNothing = new Random();

			ITest result = new LinOper2();

			GenerateVariantAndVars(random);

			var bigChar = BigCharAlphabet[randomNothing.Next(BigCharAlphabet.Length)];
			var smallChar = SmallCharAlphabet[randomNothing.Next(SmallCharAlphabet.Length)];

			GetVariantQuestion(bigChar, smallChar, random);

			var question =
				$"Дан оператор {FormulaP} на линейном пространстве с базисом \\(Б = \\left\\{{{VectorA};{VectorB};{VectorC}\\right\\}}\\)." +
				Environment.NewLine +
				$"1. Матрица \\({bigChar}_Б\\) линейного оператора \\(\\hat{{{bigChar}}}\\) в базисе \\(Б\\) равна" +
				Environment.NewLine + $"\\({bigChar}_Б = " +
				@"\begin{pmatrix}				
									<ans00>	&	<ans01>	&	<ans02>		\\						
									<ans10>	&	<ans11>	&	<ans12>		\\						
									<ans20>	&	<ans21>	&	<ans22>		\\						
				\end{pmatrix}\)";

			result.Text = question;
			return result;
		}
		public void GenerateVariantAndVars(Random random)
		{
			Option = random.Next(4);

			if (Option <= 3)
			{
				Alpha = NotZeroRandom(random, -9, 9);
				Beta = NotZeroRandom(random, -9, 9);
				Gamma = NotZeroRandom(random, -9, 9);

				if (Option == 3) Delta = NotZeroRandom(random, -9, 9);
			}
		}

		public static int NotZeroRandom(Random random, int min, int max)
		{
			while (true)
			{
				var result = random.Next(min, max);
				if (result != 0) return result;
			}
		}

		public void GetVariantQuestion(char bigChar,
			char smallChar,
			Random random)
		{
			switch (Option)
			{
				case 0:
					{
						FormulaP =
							$"\\(\\hat{{{bigChar}}}({smallChar}(x,y))\\) = \\({smallChar}({Alpha}x + {Beta}y; {Gamma}x + {Delta}y)\\)";
						VectorA = "x^2";
						VectorB = "xy";
						VectorC = "y^2";
						break;
					}
				case 1:
					{
						VectorA = "x^2";
						VectorB = "y^2";
						VectorC = "z^2";

						switch (random.Next(5))
						{

							case 0:
								FormulaP =
									$"\\(\\hat{{{bigChar}}}({smallChar}(x,y,z))\\) = \\({smallChar}({Alpha}y; {Beta}z; {Gamma}x)\\)";
								break;
							case 1:
								FormulaP =
									$"\\(\\hat{{{bigChar}}}({smallChar}(x,y,z))\\) = \\({smallChar}({Alpha}z; {Beta}x; {Gamma}z)\\)";
								break;
							case 2:
								FormulaP =
									$"\\(\\hat{{{bigChar}}}({smallChar}(x,y,z))\\) = \\({smallChar}({Alpha}x; {Beta}z; {Gamma}y)\\)";
								break;
							case 3:
								FormulaP =
									$"\\(\\hat{{{bigChar}}}({smallChar}(x,y,z))\\) = \\({smallChar}({Alpha}z; {Beta}y; {Gamma}x)\\)";
								break;
							case 4:
								FormulaP =
									$"\\(\\hat{{{bigChar}}}({smallChar}(x,y,z))\\) = \\({smallChar}({Alpha}y; {Beta}x; {Gamma}z)\\)";
								break;

						}

						break;
					}
				case 2:
					{
						VectorA = "xy";
						VectorB = "xz";
						VectorC = "yz";

						switch (random.Next(5))
						{

							case 0:
								FormulaP =
									$"\\(\\hat{{{bigChar}}}({smallChar}(x,y,z))\\) = \\({smallChar}({Alpha}y; {Beta}z; {Gamma}x)\\)";
								break;
							case 1:
								FormulaP =
									$"\\(\\hat{{{bigChar}}}({smallChar}(x,y,z))\\) = \\({smallChar}({Alpha}z; {Beta}x; {Gamma}y)\\)";
								break;
							case 2:
								FormulaP =
									$"\\(\\hat{{{bigChar}}}({smallChar}(x,y,z))\\) = \\({smallChar}({Alpha}x; {Beta}z; {Gamma}y)\\)";
								break;
							case 3:
								FormulaP =
									$"\\(\\hat{{{bigChar}}}({smallChar}(x,y,z))\\) = \\({smallChar}({Alpha}z; {Beta}y; {Gamma}x)\\)";
								break;
							case 4:
								FormulaP =
									$"\\(\\hat{{{bigChar}}}({smallChar}(x,y,z))\\) = \\({smallChar}({Alpha}y; {Beta}x; {Gamma}z)\\)";
								break;

						}
						break;
					}
				case 3:
					{
						VectorA = @"\begin{pmatrix}
										1 & 0 \\
										0 & 0 \\
									\end{pmatrix}";

						VectorB = @"\begin{pmatrix}
										0 & 1 \\
										1 & 0 \\
									\end{pmatrix}";

						VectorC = @"\begin{pmatrix}
										1 & 0 \\
										0 & 1 \\
									\end{pmatrix}";

						switch (random.Next(4))
						{
							case 0:
								FormulaP =
									$"\\(\\hat{{{bigChar}}}(\\mathbf{{X}})\\) = " +
									$@"\(\begin{{pmatrix}}														
										{Alpha} & {Beta} \\
										{Gamma} & {Delta} \\
									\end{{pmatrix}}" +
									"\\mathbf{X}" +
									$@"\begin{{pmatrix}}														
										{Alpha} & {Gamma} \\
										{Beta} & {Delta} \\
									\end{{pmatrix}}\)";
								break;
							case 1:
								FormulaP =
									$"\\(\\hat{{{bigChar}}}(\\mathbf{{X}})\\) = " +
									$@"\(\begin{{pmatrix}}														
										{Alpha} & {Beta} \\
										{Gamma} & {Delta} \\
									\end{{pmatrix}}" +
									"\\mathbf{X}+" +
									"\\mathbf{X}" +
									$@"\begin{{pmatrix}}														
										{Alpha} & {Gamma} \\
										{Beta} & {Delta} \\
									\end{{pmatrix}}\)";
								break;
							case 2:
								FormulaP =
									$"\\(\\hat{{{bigChar}}}(\\mathbf{{X}})\\) = " +
									$@"\(\begin{{pmatrix}}														
										{Alpha} & {Beta} \\
										{Gamma} & {Delta} \\
									\end{{pmatrix}}" +
									"\\mathbf{X}" +
									$@"\begin{{pmatrix}}														
										{Alpha} & {Gamma} \\
										{Beta} & {Delta} \\
									\end{{pmatrix}}\)" +
									$"\\( + ({Alpha} + {Beta})\\mathbf{{X}}\\) ";
								break;
							case 3:
								FormulaP =
									$"\\(\\hat{{{bigChar}}}(\\mathbf{{X}})\\) = " +
									$@"\(\begin{{pmatrix}}														
										{Alpha} & {Beta} \\
										{Gamma} & {Delta} \\
									\end{{pmatrix}}" +
									"\\mathbf{X}+" +
									"\\mathbf{X} " +
									$@"\begin{{pmatrix}}														
										{Alpha} & {Gamma} \\
										{Beta} & {Delta} \\
									\end{{pmatrix}}\)" +
									$"\\( + ({Alpha} + {Beta})\\mathbf{{X}}\\) ";
								break;
						}
						break;
					}
				case 4:
					{
						int u = NotZeroRandom(random, -9, 9),
							v = NotZeroRandom(random, -9, 9),
							w = NotZeroRandom(random, -9, 9),
							m = NotZeroRandom(random, -9, 9),
							n = NotZeroRandom(random, -9, 9),
							k = NotZeroRandom(random, -9, 9);

						FormulaP =
							$"\\(\\hat{{{bigChar}}}" +
							$@"\begin{{pmatrix}}
								0 & a & b \\
							   -a & 0 & c \\
							   -b & -c & 0 \\
                               \end{{pmatrix}} " +
							$@"= \begin{{pmatrix}}
								{u} & {v} & {w} \\
							    {v} & {m} & {n} \\
							    {w} & {n} & {k} \\
                               \end{{pmatrix}}" +
							$@"\begin{{pmatrix}}
								0 & a & b \\
							   -a & 0 & c \\
							   -b & -c & 0 \\
                               \end{{pmatrix}}" +
							$@"\begin{{pmatrix}}
								{u} & {v} & {w} \\
							    {v} & {m} & {n} \\
							    {w} & {n} & {k} \\
                               \end{{pmatrix}}" + "\\)";
						VectorA = $@"\begin{{pmatrix}}
								0 & 1 & 0 \\
							   -1 & 0 & 0 \\
							    0 & 0 & 0 \\
                               \end{{pmatrix}}";
						VectorB = $@"\begin{{pmatrix}}
								0 & 0 & 1 \\
							    0 & 0 & 0 \\
							   -1 & 0 & 0 \\
                               \end{{pmatrix}}";
						VectorC = $@"\begin{{pmatrix}}
								0 & 0 & 0 \\
							    0 & 0 & 1 \\
							    0 & -1 & 0 \\
                               \end{{pmatrix}}";
						break;
					}
			}

			FormulaP = Correcting();
		}

		private string Correcting() => Option switch
		{
			<= 2 => FormulaP.Replace("1", ""),
			3 => FormulaP.Replace("+ -", "-"),
			_ => FormulaP
		};

		public int CheckAnswer(int randomSeed, Dictionary<string, string> answers)
		{
			var random = new Random(randomSeed);

			GenerateVariantAndVars(random);

			int corr = GetVariantAnswer(answers, random);

			return corr;
		}

		public int GetVariantAnswer(Dictionary<string, string> answers, Random random)
		{
			int[,] trueAnswer = new int[3, 3];

			//0 0  0 1  0 2
			//1 0  1 1  1 2
			//2 0  2 1  2 2

			int corr = 0;

			switch (Option)
			{
				case 0:
					{
						trueAnswer[0, 0] = Alpha * Alpha;
						trueAnswer[0, 1] = 2 * Alpha * Beta;
						trueAnswer[0, 2] = Beta * Beta;

						trueAnswer[1, 0] = Alpha * Gamma;
						trueAnswer[1, 1] = Alpha * Delta + Beta * Gamma;
						trueAnswer[1, 2] = Beta * Delta;

						trueAnswer[2, 0] = Gamma * Gamma;
						trueAnswer[2, 1] = 2 * Gamma * Delta;
						trueAnswer[2, 2] = Delta * Delta;

						break;
					}
				case 1:
					{
						switch (random.Next(5))
						{
							//					P(x^2)	 P(y^2)	 P(z^2)
							//					𝛼^2y^2	 𝛽^2z^2  𝛾^2x^2
							// p11 p12 p13	|	0		 0		 y^2 
							// p21 p22 p23	|	a^2		 0		 0
							// p31 p32 p33	|	0		 b^2	 0
							case 0:
								{
									trueAnswer[0, 0] = 0;
									trueAnswer[0, 1] = 0;
									trueAnswer[0, 2] = Gamma * Gamma;

									trueAnswer[1, 0] = Alpha * Alpha;
									trueAnswer[1, 1] = 0;
									trueAnswer[1, 2] = 0;

									trueAnswer[2, 0] = 0;
									trueAnswer[2, 1] = Beta * Beta;
									trueAnswer[2, 2] = 0;
									break;
								}
							//					P(x^2)	 P(y^2)	 P(z^2)
							//					𝛼^2z^2	 𝛽^2x^2  𝛾^2z^2
							// p11 p12 p13	|	0		 b^2	 0 
							// p21 p22 p23	|	0		 0		 0
							// p31 p32 p33	|	a^2		 0		y^2
							case 1:
								{
									trueAnswer[0, 0] = 0;
									trueAnswer[0, 1] = Beta * Beta;
									trueAnswer[0, 2] = 0;

									trueAnswer[1, 0] = 0;
									trueAnswer[1, 1] = 0;
									trueAnswer[1, 2] = 0;

									trueAnswer[2, 0] = Alpha * Alpha;
									trueAnswer[2, 1] = 0;
									trueAnswer[2, 2] = Gamma * Gamma;
									break;
								}
							//					P(x^2)	 P(y^2)	 P(z^2)
							//					𝛼^2x^2	 𝛽^2z^2  𝛾^2y^2
							// p11 p12 p13	|	𝛼^2		 0		 0 
							// p21 p22 p23	|	0		 0		 𝛾^2
							// p31 p32 p33	|	0		 𝛽^2	 0
							case 2:
								{
									trueAnswer[0, 0] = Alpha * Alpha;
									trueAnswer[0, 1] = 0;
									trueAnswer[0, 2] = 0;

									trueAnswer[1, 0] = 0;
									trueAnswer[1, 1] = 0;
									trueAnswer[1, 2] = Gamma * Gamma;

									trueAnswer[2, 0] = 0;
									trueAnswer[2, 1] = Beta * Beta;
									trueAnswer[2, 2] = 0;
									break;
								}
							//					P(x^2)	 P(y^2)	 P(z^2)
							//					𝛼^2z^2	 𝛽^2y^2  𝛾^ 2x^2
							// p11 p12 p13	|	0		 0		 𝛾^2
							// p21 p22 p23	|	0		 𝛽^2	 0
							// p31 p32 p33	|	𝛼^2		 0		 0
							case 3:
								{
									trueAnswer[0, 0] = 0;
									trueAnswer[0, 1] = 0;
									trueAnswer[0, 2] = Gamma * Gamma;

									trueAnswer[1, 0] = 0;
									trueAnswer[1, 1] = Beta * Beta;
									trueAnswer[1, 2] = 0;

									trueAnswer[2, 0] = Alpha * Alpha;
									trueAnswer[2, 1] = 0;
									trueAnswer[2, 2] = 0;
									break;
								}
							//					P(x^2)	 P(y^2)	 P(z^2)
							//					𝛼^2y^2	 𝛽^2x^2  𝛾^2z^2
							// p11 p12 p13	|	0		 𝛽^2	 0 
							// p21 p22 p23	|	𝛼^2		 0		 0
							// p31 p32 p33	|	0		 0		 𝛾^2
							case 4:
								{
									trueAnswer[0, 0] = 0;
									trueAnswer[0, 1] = Beta * Beta;
									trueAnswer[0, 2] = 0;

									trueAnswer[1, 0] = Alpha * Alpha;
									trueAnswer[1, 1] = 0;
									trueAnswer[1, 2] = 0;

									trueAnswer[2, 0] = 0;
									trueAnswer[2, 1] = 0;
									trueAnswer[2, 2] = Gamma * Gamma;
									break;
								}
						}
						break;
					}
				case 2:
					{
						switch (random.Next(5))
						{
							//					P(x^2)	 P(y^2)	 P(z^2)
							//					𝛼𝛽yz	 𝛼𝛾xy	 𝛽𝛾xz
							// p11 p12 p13	|	0		 𝛼𝛾		 0 
							// p21 p22 p23	|	0		 0		 𝛽𝛾
							// p31 p32 p33	|	𝛼𝛽		 0		 0
							case 0:
								{
									trueAnswer[0, 0] = 0;
									trueAnswer[0, 1] = Alpha * Gamma;
									trueAnswer[0, 2] = 0;

									trueAnswer[1, 0] = 0;
									trueAnswer[1, 1] = 0;
									trueAnswer[1, 2] = Beta * Gamma;

									trueAnswer[2, 0] = Alpha * Beta;
									trueAnswer[2, 1] = 0;
									trueAnswer[2, 2] = 0;
									break;
								}
							//					P(x^2)	 P(y^2)	 P(z^2)
							//					𝛼𝛽xz	 𝛼𝛾yz	 𝛽𝛾xy
							// p11 p12 p13	|	0		 0		 𝛽𝛾 
							// p21 p22 p23	|	𝛼𝛽		 0		 0
							// p31 p32 p33	|	0		 𝛼𝛾		 0
							case 1:
								{
									trueAnswer[0, 0] = 0;
									trueAnswer[0, 1] = 0;
									trueAnswer[0, 2] = Beta * Gamma;

									trueAnswer[1, 0] = Alpha * Beta;
									trueAnswer[1, 1] = 0;
									trueAnswer[1, 2] = 0;

									trueAnswer[2, 0] = 0;
									trueAnswer[2, 1] = Alpha * Gamma;
									trueAnswer[2, 2] = 0;
									break;
								}
							//					P(x^2)	 P(y^2)	 P(z^2)
							//					𝛼𝛽xz	 𝛼𝛾xy	 𝛽𝛾yz
							// p11 p12 p13	|	0		 𝛼𝛾		 0 
							// p21 p22 p23	|	𝛼𝛽		 0		 0
							// p31 p32 p33	|	0		 0		 𝛽𝛾
							case 2:
								{
									trueAnswer[0, 0] = 0;
									trueAnswer[0, 1] = Alpha * Gamma;
									trueAnswer[0, 2] = 0;

									trueAnswer[1, 0] = Alpha * Beta;
									trueAnswer[1, 1] = 0;
									trueAnswer[1, 2] = 0;

									trueAnswer[2, 0] = 0;
									trueAnswer[2, 1] = 0;
									trueAnswer[2, 2] = Beta * Gamma;
									break;
								}
							//					P(x^2)	 P(y^2)	 P(z^2)
							//					𝛼𝛽yz	 𝛼𝛾xz	 𝛽𝛾xy
							// p11 p12 p13	|	0		 0		 𝛽𝛾 
							// p21 p22 p23	|	0		 𝛼𝛾		 0
							// p31 p32 p33	|	𝛼𝛽		 0		 0
							case 3:
								{
									trueAnswer[0, 0] = 0;
									trueAnswer[0, 1] = 0;
									trueAnswer[0, 2] = Beta * Gamma;

									trueAnswer[1, 0] = 0;
									trueAnswer[1, 1] = Alpha * Gamma;
									trueAnswer[1, 2] = 0;

									trueAnswer[2, 0] = Alpha * Beta;
									trueAnswer[2, 1] = 0;
									trueAnswer[2, 2] = 0;
									break;
								}
							//					P(x^2)	 P(y^2)	 P(z^2)
							//					𝛼𝛽xy	 𝛼𝛾yz	 𝛽𝛾xz
							// p11 p12 p13	|	𝛼𝛽		 0		 0 
							// p21 p22 p23	|	0		 0		 𝛽𝛾
							// p31 p32 p33	|	0		 𝛼𝛾		 0
							case 4:
								{
									trueAnswer[0, 0] = Alpha * Beta;
									trueAnswer[0, 1] = 0;
									trueAnswer[0, 2] = 0;

									trueAnswer[1, 0] = 0;
									trueAnswer[1, 1] = 0;
									trueAnswer[1, 2] = Beta * Gamma;

									trueAnswer[2, 0] = 0;
									trueAnswer[2, 1] = Alpha * Gamma;
									trueAnswer[2, 2] = 0;
									break;
								}
						}

						break;
					}
				case 3:
					{
						switch (random.Next(4))
						{
							//					P(1 0)	 P(0 1)	 P(0 0)
							//					 (0 0)	  (1 0)	  (0 1)
							// p11 p12 p13	|	𝛼^2		 2𝛼𝛽	 𝛽𝛿 
							// p21 p22 p23	|	𝛼𝛾		 𝛽𝛾 + 𝛼𝛿	 𝛽𝛾
							// p31 p32 p33	|	𝛾^2		 2𝛿𝛾		 𝛿^2
							case 0:
								{
									trueAnswer[0, 0] = Alpha * Alpha;
									trueAnswer[0, 1] = 2 * Alpha * Beta;
									trueAnswer[0, 2] = Beta * Delta;

									trueAnswer[1, 0] = Alpha * Gamma;
									trueAnswer[1, 1] = Beta * Gamma + Alpha * Delta;
									trueAnswer[1, 2] = Beta * Gamma;

									trueAnswer[2, 0] = Gamma * Gamma;
									trueAnswer[2, 1] = 2 * Delta * Gamma;
									trueAnswer[2, 2] = Delta * Delta;
									break;
								}
							//					P(1 0)	 P(0 1)	 P(0 0)
							//					 (0 0)	  (1 0)	  (0 1)
							// p11 p12 p13	|	2𝛼		 2𝛽		  0 
							// p21 p22 p23	|	𝛾		 𝛼+𝛿		  𝛽
							// p31 p32 p33	|	0		 2𝛾		  2𝛿
							case 1:
								{
									trueAnswer[0, 0] = 2 * Alpha;
									trueAnswer[0, 1] = 2 * Beta;
									trueAnswer[0, 2] = 0;

									trueAnswer[1, 0] = Gamma;
									trueAnswer[1, 1] = Alpha + Delta;
									trueAnswer[1, 2] = Beta;

									trueAnswer[2, 0] = 0;
									trueAnswer[2, 1] = 2 * Gamma;
									trueAnswer[2, 2] = 2 * Delta;
									break;
								}
							//					P(1 0)	 P(0 1)	 P(0 0)
							//					 (0 0)	  (1 0)	  (0 1)
							// p11 p12 p13	|  𝛼^2+𝛼+𝛽	 2*𝛼𝛽	 𝛽*𝛿
							// p21 p22 p23	|	𝛼*𝛾   𝛽𝛾+𝛼𝛿+𝛼+𝛽   𝛽𝛾
							// p31 p32 p33	|	𝛾^2	    2*𝛿*𝛾	 𝛿^2+𝛼+𝛽
							case 2:
								{
									trueAnswer[0, 0] = Alpha * Alpha + Alpha + Beta;
									trueAnswer[0, 1] = 2 * Alpha * Beta;
									trueAnswer[0, 2] = Beta * Delta;

									trueAnswer[1, 0] = Alpha * Gamma;
									trueAnswer[1, 1] = Beta * Gamma + Alpha * Delta + Alpha + Beta;
									trueAnswer[1, 2] = Beta * Gamma;

									trueAnswer[2, 0] = Gamma * Gamma;
									trueAnswer[2, 1] = 2 * Delta * Gamma;
									trueAnswer[2, 2] = Delta * Delta + Alpha + Beta;
									break;
								}
							//					P(1 0)	 P(0 1)	 P(0 0)
							//					 (0 0)	  (1 0)	  (0 1)
							// p11 p12 p13	|	 3𝛼+𝛽		2𝛽	   0
							// p21 p22 p23	|	   𝛾     2𝛼+𝛽+𝛿    𝛽
							// p31 p32 p33	|	   0	    2𝛾	 𝛼+𝛽+2𝛿
							case 3:
								{
									trueAnswer[0, 0] = 3 * Alpha + Beta;
									trueAnswer[0, 1] = 2 * Beta;
									trueAnswer[0, 2] = 0;

									trueAnswer[1, 0] = Gamma;
									trueAnswer[1, 1] = 2 * Alpha + Beta + Delta;
									trueAnswer[1, 2] = Beta;

									trueAnswer[2, 0] = 0;
									trueAnswer[2, 1] = 2 * Gamma;
									trueAnswer[2, 2] = Alpha + Beta + 2 * Delta;
									break;
								}
						}
						break;
					}
				case 4:
					{
						//					P(VectorA)	P(VectorB) P(VectorC)
						// p11 p12 p13	|	 m*u-v^2	 n*u-v*w    n*v-m*w
						// p21 p22 p23	|	 n*u-v*w     k*u-w^2    k*v-n*w
						// p31 p32 p33	|    n*v-m*w	 k*v-n*w    k*m-n^2
						int u = NotZeroRandom(random, -9, 9),
							v = NotZeroRandom(random, -9, 9),
							w = NotZeroRandom(random, -9, 9),
							m = NotZeroRandom(random, -9, 9),
							n = NotZeroRandom(random, -9, 9),
							k = NotZeroRandom(random, -9, 9);


						trueAnswer[0, 0] = m * u - v * v;
						trueAnswer[0, 1] = n * u - v * w;
						trueAnswer[0, 2] = n * v - m * w;

						trueAnswer[1, 0] = n * u - v * w;
						trueAnswer[1, 1] = k * u - w * w;
						trueAnswer[1, 2] = k * v - n * w;

						trueAnswer[2, 0] = n * v - m * w;
						trueAnswer[2, 1] = k * v - n * w;
						trueAnswer[2, 2] = k * m - n * n;
						break;
					}
			}

			corr = CompareResponses(answers, trueAnswer);

			return corr;
		}

		private static int CompareResponses(Dictionary<string, string> answers, int[,] trueAnswer)
		{
			int corr = 0;

			for (int i = 0; i < trueAnswer.GetLength(0); i++)
			{
				for (int j = 0; j < trueAnswer.GetLength(1); j++)
				{
					if (trueAnswer[i, j].ToString() == answers[$"ans{i}{j}"]) corr++;
				}
			}

			return corr;
		}
	}
}