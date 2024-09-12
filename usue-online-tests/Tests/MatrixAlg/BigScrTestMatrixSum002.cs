using System;
using System.Collections.Generic;
using System.Drawing;
using Test_Wrapper;

namespace UserTest
{
    public class BigScrTestMatrixSum002 : ITestCreator, ITest, ITimeLimit, IHidden, ITestGroup
    {
        public int TestID { get; set; }
        public string GroupName { get; set; }= "Matrix Algebra";
        public string Name { get; } = "Вычисление суммы матриц 002";
        public string Description { get; } = "Матричные операции";

        public class Data
        {

            public string LetterR = "ABCDFGHKMPRSUVWXYZ";
            public int[] answA = new int[4];
            public int[] MatrA = new int[4];
            public int[] MatrB = new int[4];
            public int[] MatrC = new int[4];
            public int iA, jA, iB, jB, iCx, jCx, iCy, jCy, Vvv;
            public string ElemMatrA = "";
            public string ElemMatrB = "";
            public string ElemMatrC = "";
            public string MCa = "";
            public string MCb = "";
            public string MCc = "";
            public string IndAa = "";
            public string IndAb = "";
            public string IndAc = "";
            public string IndBa = "";
            public string IndBb = "";
            public string IndBc = "";
            public string IndexC = "";
            public string MatrEqA = "";
            public int alpha;
            public int beta;

            public Data(Random random)
            {
                CreateMatrixes(random);
            }
            public void CreateMatrixes(Random random)
            {
                int[] Numbers = new int[40];
                for (int i = 0; i < 20; i++)
                {
                    Numbers[2*i] = -i-1;
                    Numbers[2*i+1] = i+1;
                }
                Vvv = random.Next(38);
                MatrA[0] = Numbers[Vvv];
                Numbers[Vvv] = Numbers[39];
                Vvv = random.Next(37);
                MatrA[1] = Numbers[Vvv];
                Numbers[Vvv] = Numbers[38];
                Vvv = random.Next(36);
                MatrA[2] = Numbers[Vvv];
                Numbers[Vvv] = Numbers[37];
                Vvv = random.Next(35);
                MatrA[3] = Numbers[Vvv];
                Numbers[Vvv] = Numbers[36];
                Vvv = random.Next(34);
                MatrB[0] = Numbers[Vvv];
                Numbers[Vvv] = Numbers[35];
                Vvv = random.Next(33);
                MatrB[1] = Numbers[Vvv];
                Numbers[Vvv] = Numbers[34];
                Vvv = random.Next(32);
                MatrB[2] = Numbers[Vvv];
                Numbers[Vvv] = Numbers[33];
                Vvv = random.Next(31);
                MatrB[3] = Numbers[Vvv];
                Numbers[Vvv] = Numbers[32];
                for (int i = 0; i < 4; i++)
                {
                   MatrC[i] = MatrA[i] + MatrB[i];
                }
                iA = random.Next(2);
                jA = random.Next(2);
                iB = random.Next(2);
                jB = random.Next(2);
                
                //iA = 1; jA=1; iB=1; jB=1;
                
                if ( iA == iB ) 
                {
                    if ( jA == jB )
                    {
                        jB = 1-jB;
                    };
                }
                if (iA == iB)
                {
                    iCx = 1 - iA;
                    jCx = 0;
                    iCy = 1 - iA;
                    jCy = 1;
                }
                else
                {
                    iCx = iA;
                    jCx = 1 - jA; 
                    iCy = iB;
                    jCy = 1 - jB; 
                }
                answA[0] = MatrA[2*iA+jA];
                answA[1] = MatrB[2*iB+jB];
                answA[2] = MatrC[2*iCx+jCx];
                answA[3] = MatrC[2*iCy+jCy];
            }
        }
        public ITest CreateTest(int randomSeed)
        {
            ITest result = new BigScrTestMatrixSum002();

            Random random = new Random(randomSeed);
            Data data = new Data(random);
                        
            //int num = random.Next(18);

            //result.Text =
                //$"\\(iA={data.iA}, jA={data.jA}, iB={data.iB}, jB={data.jB}, iCx={data.iCx}, jCx={data.jCx}, iCy={data.iCy}, jCy={data.jCy}\\)\r\n" +
                //$"\\(\\left(\\begin{{array}}{{cc}} {data.MatrA[0]} & {data.MatrA[1]}\\\\ {data.MatrA[2]} & {data.MatrA[3]}\\\\ \\end{{array}}\\right)\\) + " + 
                //$"\\(\\left(\\begin{{array}}{{cc}} {data.MatrB[0]} & {data.MatrB[1]}\\\\ {data.MatrB[2]} & {data.MatrB[3]}\\\\ \\end{{array}}\\right)\\) = " + 
                //$"\\(\\left(\\begin{{array}}{{cc}} {data.MatrC[0]} & {data.MatrC[1]}\\\\ {data.MatrC[2]} & {data.MatrC[3]}\\\\ \\end{{array}}\\right)\\)"; 
                switch (8*data.iA+4*data.jA+2*data.iB+data.jB)
                {
                    case 0://0000
                    case 1://0001
                        result.Text =                        
                        //$"\\(iA={data.iA}, jA={data.jA}, iB={data.iB}, jB={data.jB}, iCx={data.iCx}, jCx={data.jCx}, iCy={data.iCy}, jCy={data.jCy}\\)\r\n" +
                        $"\\(\\left(\\begin{{array}}{{cc}} <ans[0]:5>      & {data.MatrA[1]}\\\\ {data.MatrA[2]} & {data.MatrA[3]}\\\\ \\end{{array}}\\right)\\) + " + 
                        $"\\(\\left(\\begin{{array}}{{cc}} {data.MatrB[0]} & <ans[1]:5>     \\\\ {data.MatrB[2]} & {data.MatrB[3]}\\\\ \\end{{array}}\\right)\\) = " + 
                        $"\\(\\left(\\begin{{array}}{{cc}} {data.MatrC[0]} & {data.MatrC[1]}\\\\ <ans[2]:5>      & <ans[3]:5>     \\\\ \\end{{array}}\\right)\\)\r\n";// +
                        //$"\\({data.answA[0]},{data.answA[1]},{data.answA[2]},{data.answA[3]}\\)"; 
                        break;
                    case 2://0010
                        result.Text =                        
                        //$"\\(iA={data.iA}, jA={data.jA}, iB={data.iB}, jB={data.jB}, iCx={data.iCx}, jCx={data.jCx}, iCy={data.iCy}, jCy={data.jCy}\\)\r\n" +
                        $"\\(\\left(\\begin{{array}}{{cc}} <ans[0]:5>      & {data.MatrA[1]}\\\\ {data.MatrA[2]} & {data.MatrA[3]}\\\\ \\end{{array}}\\right)\\) + " + 
                        $"\\(\\left(\\begin{{array}}{{cc}} {data.MatrB[0]} & {data.MatrB[1]}\\\\ <ans[1]:5>      & {data.MatrB[3]}\\\\ \\end{{array}}\\right)\\) = " + 
                        $"\\(\\left(\\begin{{array}}{{cc}} {data.MatrC[0]} & <ans[2]:5>     \\\\ {data.MatrC[2]} & <ans[3]:5>     \\\\ \\end{{array}}\\right)\\)\r\n";// +
                        //$"\\({data.answA[0]},{data.answA[1]},{data.answA[2]},{data.answA[3]}\\)"; 
                        break;
                    case 3://0011
                        result.Text =                        
                        //$"\\(iA={data.iA}, jA={data.jA}, iB={data.iB}, jB={data.jB}, iCx={data.iCx}, jCx={data.jCx}, iCy={data.iCy}, jCy={data.jCy}\\)\r\n" +
                        $"\\(\\left(\\begin{{array}}{{cc}} <ans[0]:5>      & {data.MatrA[1]}\\\\ {data.MatrA[2]} & {data.MatrA[3]}\\\\ \\end{{array}}\\right)\\) + " + 
                        $"\\(\\left(\\begin{{array}}{{cc}} {data.MatrB[0]} & {data.MatrB[1]}\\\\ {data.MatrB[2]} & <ans[1]:5>     \\\\ \\end{{array}}\\right)\\) = " + 
                        $"\\(\\left(\\begin{{array}}{{cc}} {data.MatrC[0]} & <ans[2]:5>     \\\\ <ans[3]:5>      & {data.MatrC[3]}\\\\ \\end{{array}}\\right)\\)\r\n";// +
                        //$"\\({data.answA[0]},{data.answA[1]},{data.answA[2]},{data.answA[3]}\\)"; 
                        break;
                    case 4://0100
                    case 5://0101
                        result.Text =                        
                        //$"\\(iA={data.iA}, jA={data.jA}, iB={data.iB}, jB={data.jB}, iCx={data.iCx}, jCx={data.jCx}, iCy={data.iCy}, jCy={data.jCy}\\)\r\n" +
                        $"\\(\\left(\\begin{{array}}{{cc}} {data.MatrA[0]} & <ans[0]:5>     \\\\ {data.MatrA[2]} & {data.MatrA[3]}\\\\ \\end{{array}}\\right)\\) + " + 
                        $"\\(\\left(\\begin{{array}}{{cc}} <ans[1]:5>      & {data.MatrB[1]}\\\\ {data.MatrB[2]} & {data.MatrB[3]}\\\\ \\end{{array}}\\right)\\) = " + 
                        $"\\(\\left(\\begin{{array}}{{cc}} {data.MatrC[0]} & {data.MatrC[1]}\\\\ <ans[2]:5>      & <ans[3]:5>     \\\\ \\end{{array}}\\right)\\)\r\n";// +
                        //$"\\({data.answA[0]},{data.answA[1]},{data.answA[2]},{data.answA[3]}\\)"; 
                        break;
                    case 6://0110
                        result.Text =                        
                        //$"\\(iA={data.iA}, jA={data.jA}, iB={data.iB}, jB={data.jB}, iCx={data.iCx}, jCx={data.jCx}, iCy={data.iCy}, jCy={data.jCy}\\)\r\n" +
                        $"\\(\\left(\\begin{{array}}{{cc}} {data.MatrA[0]} & <ans[0]:5>     \\\\ {data.MatrA[2]} & {data.MatrA[3]}\\\\ \\end{{array}}\\right)\\) + " + 
                        $"\\(\\left(\\begin{{array}}{{cc}} {data.MatrB[0]} & {data.MatrB[1]}\\\\ <ans[1]:5>      & {data.MatrB[3]}\\\\ \\end{{array}}\\right)\\) = " + 
                        $"\\(\\left(\\begin{{array}}{{cc}} <ans[2]:5>      & {data.MatrC[1]}\\\\ {data.MatrC[2]} & <ans[3]:5>     \\\\ \\end{{array}}\\right)\\)\r\n";// +
                        //$"\\({data.answA[0]},{data.answA[1]},{data.answA[2]},{data.answA[3]}\\)"; 
                        break;
                    case 7://0111
                        result.Text =                        
                        //$"\\(iA={data.iA}, jA={data.jA}, iB={data.iB}, jB={data.jB}, iCx={data.iCx}, jCx={data.jCx}, iCy={data.iCy}, jCy={data.jCy}\\)\r\n" +
                        $"\\(\\left(\\begin{{array}}{{cc}} {data.MatrA[0]} & <ans[0]:5>     \\\\ {data.MatrA[2]} & {data.MatrA[3]}\\\\ \\end{{array}}\\right)\\) + " + 
                        $"\\(\\left(\\begin{{array}}{{cc}} {data.MatrB[0]} & {data.MatrB[1]}\\\\ {data.MatrB[2]} & <ans[1]:5>     \\\\ \\end{{array}}\\right)\\) = " + 
                        $"\\(\\left(\\begin{{array}}{{cc}} <ans[2]:5>      & {data.MatrC[1]}\\\\ <ans[3]:5>      & {data.MatrC[3]}\\\\ \\end{{array}}\\right)\\)\r\n";// +
                        //$"\\({data.answA[0]},{data.answA[1]},{data.answA[2]},{data.answA[3]}\\)"; 
                        break;
                    case 8://1000
                        result.Text =                        
                        //$"\\(iA={data.iA}, jA={data.jA}, iB={data.iB}, jB={data.jB}, iCx={data.iCx}, jCx={data.jCx}, iCy={data.iCy}, jCy={data.jCy}\\)\r\n" +
                        $"\\(\\left(\\begin{{array}}{{cc}} {data.MatrA[0]} & {data.MatrA[1]}\\\\ <ans[0]:5>      & {data.MatrA[3]}\\\\ \\end{{array}}\\right)\\) + " + 
                        $"\\(\\left(\\begin{{array}}{{cc}} <ans[1]:5>      & {data.MatrB[1]}\\\\ {data.MatrB[2]} & {data.MatrB[3]}\\\\ \\end{{array}}\\right)\\) = " + 
                        $"\\(\\left(\\begin{{array}}{{cc}} {data.MatrC[0]} & <ans[3]:5>     \\\\ {data.MatrC[2]} & <ans[2]:5>     \\\\ \\end{{array}}\\right)\\)\r\n";// +
                        //$"\\({data.answA[0]},{data.answA[1]},{data.answA[2]},{data.answA[3]}\\)"; 
                        break;
                    case 9://1001
                        result.Text =                        
                        //$"\\(iA={data.iA}, jA={data.jA}, iB={data.iB}, jB={data.jB}, iCx={data.iCx}, jCx={data.jCx}, iCy={data.iCy}, jCy={data.jCy}\\)\r\n" +
                        $"\\(\\left(\\begin{{array}}{{cc}} {data.MatrA[0]} & {data.MatrA[1]}\\\\ <ans[0]:5>      & {data.MatrA[3]}\\\\ \\end{{array}}\\right)\\) + " + 
                        $"\\(\\left(\\begin{{array}}{{cc}} {data.MatrB[0]} & <ans[1]:5>     \\\\ {data.MatrB[2]} & {data.MatrB[3]}\\\\ \\end{{array}}\\right)\\) = " + 
                        $"\\(\\left(\\begin{{array}}{{cc}} <ans[3]:5>      & {data.MatrC[1]}\\\\ {data.MatrC[2]} & <ans[2]:5>     \\\\ \\end{{array}}\\right)\\)\r\n";// +
                        //$"\\({data.answA[0]},{data.answA[1]},{data.answA[2]},{data.answA[3]}\\)"; 
                        break;
                    case 10://1010
                    case 11://1011
                        result.Text =                        
                        //$"\\(iA={data.iA}, jA={data.jA}, iB={data.iB}, jB={data.jB}, iCx={data.iCx}, jCx={data.jCx}, iCy={data.iCy}, jCy={data.jCy}\\)\r\n" +
                        $"\\(\\left(\\begin{{array}}{{cc}} {data.MatrA[0]} & {data.MatrA[1]}\\\\ <ans[0]:5>      & {data.MatrA[3]}\\\\ \\end{{array}}\\right)\\) + " + 
                        $"\\(\\left(\\begin{{array}}{{cc}} {data.MatrB[0]} & {data.MatrB[1]}\\\\ {data.MatrB[2]} & <ans[1]:5>     \\\\ \\end{{array}}\\right)\\) = " + 
                        $"\\(\\left(\\begin{{array}}{{cc}} <ans[2]:5>      & <ans[3]:5>     \\\\ {data.MatrC[2]} & {data.MatrC[3]}\\\\ \\end{{array}}\\right)\\)\r\n";// +
                        //$"\\({data.answA[0]},{data.answA[1]},{data.answA[2]},{data.answA[3]}\\)"; 
                        break;
                    case 12://1100
                        result.Text =                        
                        //$"\\(iA={data.iA}, jA={data.jA}, iB={data.iB}, jB={data.jB}, iCx={data.iCx}, jCx={data.jCx}, iCy={data.iCy}, jCy={data.jCy}\\)\r\n" +
                        $"\\(\\left(\\begin{{array}}{{cc}} {data.MatrA[0]} & {data.MatrA[1]}\\\\ {data.MatrA[2]} & <ans[0]:5>     \\\\ \\end{{array}}\\right)\\) + " + 
                        $"\\(\\left(\\begin{{array}}{{cc}} <ans[1]:5>      & {data.MatrB[1]}\\\\ {data.MatrB[2]} & {data.MatrB[3]}\\\\ \\end{{array}}\\right)\\) = " + 
                        $"\\(\\left(\\begin{{array}}{{cc}} {data.MatrC[0]} & <ans[3]:5>     \\\\ <ans[2]:5>      & {data.MatrC[3]}\\\\ \\end{{array}}\\right)\\)\r\n";// +
                        //$"\\({data.answA[0]},{data.answA[1]},{data.answA[2]},{data.answA[3]}\\)"; 
                        break;
                    case 13://1101
                        result.Text =                        
                        //$"\\(iA={data.iA}, jA={data.jA}, iB={data.iB}, jB={data.jB}, iCx={data.iCx}, jCx={data.jCx}, iCy={data.iCy}, jCy={data.jCy}\\)\r\n" +
                        $"\\(\\left(\\begin{{array}}{{cc}} {data.MatrA[0]} & {data.MatrA[1]}\\\\ {data.MatrA[2]} & <ans[0]:5>     \\\\ \\end{{array}}\\right)\\) + " + 
                        $"\\(\\left(\\begin{{array}}{{cc}} {data.MatrB[0]} & <ans[1]:5>     \\\\ {data.MatrB[2]} & {data.MatrB[3]}\\\\ \\end{{array}}\\right)\\) = " + 
                        $"\\(\\left(\\begin{{array}}{{cc}} <ans[3]:5>      & {data.MatrC[1]}\\\\ <ans[2]:5>      & {data.MatrC[3]}\\\\ \\end{{array}}\\right)\\)\r\n";// +
                        //$"\\({data.answA[0]},{data.answA[1]},{data.answA[2]},{data.answA[3]}\\)"; 
                        break;
                    case 14://1110
                    case 15://1111
                        result.Text =                        
                        //$"\\(iA={data.iA}, jA={data.jA}, iB={data.iB}, jB={data.jB}, iCx={data.iCx}, jCx={data.jCx}, iCy={data.iCy}, jCy={data.jCy}\\)\r\n" +
                        $"\\(\\left(\\begin{{array}}{{cc}} {data.MatrA[0]} & {data.MatrA[1]}\\\\ {data.MatrA[2]} & <ans[0]:5>     \\\\ \\end{{array}}\\right)\\) + " + 
                        $"\\(\\left(\\begin{{array}}{{cc}} {data.MatrB[0]} & {data.MatrB[1]}\\\\ <ans[1]:5>      & {data.MatrB[3]}\\\\ \\end{{array}}\\right)\\) = " + 
                        $"\\(\\left(\\begin{{array}}{{cc}} <ans[2]:5>      & <ans[3]:5>     \\\\ {data.MatrC[2]} & {data.MatrC[3]}\\\\ \\end{{array}}\\right)\\)\r\n";// +
                        //$"\\({data.answA[0]},{data.answA[1]},{data.answA[2]},{data.answA[3]}\\)"; 
                        break;
                    default:
                        result.Text =                        
                        $"\\(iA={data.iA}, jA={data.jA}, iB={data.iB}, jB={data.jB}, iCx={data.iCx}, jCx={data.jCx}, iCy={data.iCy}, jCy={data.jCy}\\)\r\n" +
                        $"\\(\\left(\\begin{{array}}{{cc}} {data.MatrA[0]} & {data.MatrA[1]}\\\\ {data.MatrA[2]} & {data.MatrA[3]}\\\\ \\end{{array}}\\right)\\) + " + 
                        $"\\(\\left(\\begin{{array}}{{cc}} {data.MatrB[0]} & {data.MatrB[1]}\\\\ {data.MatrB[2]} & {data.MatrB[3]}\\\\ \\end{{array}}\\right)\\) = " + 
                        $"\\(\\left(\\begin{{array}}{{cc}} {data.MatrC[0]} & {data.MatrC[1]}\\\\ {data.MatrC[2]} & {data.MatrC[3]}\\\\ \\end{{array}}\\right)\\)"; 
                        break;
                }
//
//
                ////$"{data.LetterR}" +
                //$"Представьте формулу для элемента матрицы \\({data.MatrEqA}\\) в виде результата" +
                //$" применения матричных операций к матрицами " +
                //$"\\({data.MatrA}=\\left({data.MatrA}_{{ij}}\\right),\\quad\\) " +
                //$"\\({data.MatrB}=\\left({data.MatrB}_{{ij}}\\right),\\quad \\) " +
                //$" (элемент \\({data.ElemMatrC}_{{ij}}\\) запишите как \\({data.ElemMatrC}ij\\), где все буквы строчные, и т.п., "+
                ////$" результат транспонирования матрицы \\(Z\\) введите как \\(ZT\\), " +
                //$"не пропускайте знак * операции умножения, скобки не используйте, не переставляйте слагаемые, " +
                //$" первый множитель относится к первому индексу, второй - ко второму), \r\n" +
                //$" \\({data.ElemMatrC}_{{{data.IndexC}}}=\\)\\(<ans:20>\\)\r\n"; //+
                ////$" {data.answA}";

            return result;
        }

        public int CheckAnswer(int randomSeed, Dictionary<string, string> answers)
        {
            int total = 0;

            Random random = new Random(randomSeed);
            Data data = new Data(random);
                        
            //int num = random.Next(8);


            //string[] ans = ans[num];
            try
            {
                for (int i = 0; i < 4; i++)
                {
                    // if (answers["ans" + i]  == ans[i].ToString()) total++;
                    //if(answers[ans] == data.ans[num][0]) total++;
                    if (answers["ans["+i+"]"] == data.answA[i].ToString()) total++;
                }
            }
            catch
            {
                // ignored
            }

            return total;
        }

        public string Text { get; set; }
        public string[] CheckBoxes { get; set; }
        public List<Image> Pictures { get; set; }
        public int TimeLimitSeconds { get; set; } = 120;
        public bool IsHidden { get; set; } = false;
    }
}
