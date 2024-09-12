using System;
using System.Collections.Generic;
using System.Drawing;
using Test_Wrapper;

namespace UserTest
{
    public class BigScrTestMatrixSum003 : ITestCreator, ITest, ITimeLimit, IHidden, ITestGroup
    {
        public int TestID { get; set; }
        public string GroupName { get; set; }= "Matrix Algebra";
        public string Name { get; } = "Вычисление суммы матриц 003";
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
                Vvv = random.Next(1,10);
                alpha = Numbers[Vvv];
                Numbers[Vvv] = Numbers[31];
                for (int i = 0; i < 4; i++)
                {
                   MatrC[i] = MatrA[i] + alpha * MatrB[i];
                }
                answA[3] = alpha;
                Vvv = random.Next(24);
                switch (Vvv)
                {
                    case  0: //(00)(01)(10)
                        answA[0]=MatrA[0];
                        answA[1]=MatrB[1];
                        answA[2]=MatrC[2];
                        break;
                    case  1: //(00)(01)(11)
                        answA[0]=MatrA[0];
                        answA[1]=MatrB[1];
                        answA[2]=MatrC[3];
                        break;
                    case  2: //(00)(10)(01)
                        answA[0]=MatrA[0];
                        answA[1]=MatrB[2];
                        answA[2]=MatrC[1];
                        break;
                    case  3: //(00)(10)(11)
                        answA[0]=MatrA[0];
                        answA[1]=MatrB[2];
                        answA[2]=MatrC[3];
                        break;
                    case  4: //(00)(11)(01)
                        answA[0]=MatrA[0];
                        answA[1]=MatrB[3];
                        answA[2]=MatrC[1];
                        break;
                    case  5: //(00)(11)(10)
                        answA[0]=MatrA[0];
                        answA[1]=MatrB[3];
                        answA[2]=MatrC[2];
                        break;
                    case  6: //(01)(00)(10)
                        answA[0]=MatrA[1];
                        answA[1]=MatrB[0];
                        answA[2]=MatrC[2];
                        break;
                    case  7: //(01)(00)(11)
                        answA[0]=MatrA[1];
                        answA[1]=MatrB[0];
                        answA[2]=MatrC[3];
                        break;
                    case  8: //(01)(10)(00)
                        answA[0]=MatrA[1];
                        answA[1]=MatrB[2];
                        answA[2]=MatrC[0];
                        break;
                    case  9: //(01)(10)(11)
                        answA[0]=MatrA[1];
                        answA[1]=MatrB[2];
                        answA[2]=MatrC[3];
                        break;
                    case 10: //(01)(11)(00)
                        answA[0]=MatrA[1];
                        answA[1]=MatrB[3];
                        answA[2]=MatrC[0];
                        break;
                    case 11: //(01)(11)(10)
                        answA[0]=MatrA[1];
                        answA[1]=MatrB[3];
                        answA[2]=MatrC[2];
                        break;
                    case 12: //(10)(00)(01)
                        answA[0]=MatrA[2];
                        answA[1]=MatrB[0];
                        answA[2]=MatrC[1];
                        break;
                    case 13: //(10)(00)(11)
                        answA[0]=MatrA[2];
                        answA[1]=MatrB[0];
                        answA[2]=MatrC[3];
                        break;
                    case 14: //(10)(01)(00)
                        answA[0]=MatrA[2];
                        answA[1]=MatrB[1];
                        answA[2]=MatrC[0];
                        break;
                    case 15: //(10)(01)(11)
                        answA[0]=MatrA[2];
                        answA[1]=MatrB[1];
                        answA[2]=MatrC[3];
                        break;
                    case 16: //(10)(11)(00)
                        answA[0]=MatrA[2];
                        answA[1]=MatrB[3];
                        answA[2]=MatrC[0];
                        break;
                    case 17: //(10)(11)(01)
                        answA[0]=MatrA[2];
                        answA[1]=MatrB[3];
                        answA[2]=MatrC[1];
                        break;
                    case 18: //(11)(00)(01)
                        answA[0]=MatrA[3];
                        answA[1]=MatrB[0];
                        answA[2]=MatrC[1];
                        break;
                    case 19: //(11)(00)(10)
                        answA[0]=MatrA[3];
                        answA[1]=MatrB[0];
                        answA[2]=MatrC[2];
                        break;
                    case 20: //(11)(01)(00)
                        answA[0]=MatrA[3];
                        answA[1]=MatrB[1];
                        answA[2]=MatrC[0];
                        break;
                    case 21: //(11)(01)(10)
                        answA[0]=MatrA[3];
                        answA[1]=MatrB[1];
                        answA[2]=MatrC[2];
                        break;
                    case 22: //(11)(10)(00)
                        answA[0]=MatrA[3];
                        answA[1]=MatrB[2];
                        answA[2]=MatrC[0];
                        break;
                    case 23: //(11)(10)(01)
                        answA[0]=MatrA[3];
                        answA[1]=MatrB[2];
                        answA[2]=MatrC[1];
                        break;
                }
            }
        }
        public ITest CreateTest(int randomSeed)
        {
            ITest result = new BigScrTestMatrixSum003();

            Random random = new Random(randomSeed);
            Data data = new Data(random);
                        
            //int num = random.Next(18);

            switch (data.Vvv)
            {
                case  0: //(00)(01)(10)
                    //answA[0]=MatrA[0]; answA[1]=MatrB[1]; answA[2]=MatrC[2];
                    result.Text =
                    $"\\(\\left(\\begin{{array}}{{cc}} <ans[0]:5>      & {data.MatrA[1]}\\\\ {data.MatrA[2]} & {data.MatrA[3]}\\\\ \\end{{array}}\\right)\\) + " + 
                    $"\\(<ans[3]:5>\\)" +
                    $"\\(\\left(\\begin{{array}}{{cc}} {data.MatrB[0]} & <ans[1]:5>     \\\\ {data.MatrB[2]} & {data.MatrB[3]}\\\\ \\end{{array}}\\right)\\) = " + 
                    $"\\(\\left(\\begin{{array}}{{cc}} {data.MatrC[0]} & {data.MatrC[1]}\\\\ <ans[2]:5>      & {data.MatrC[3]}\\\\ \\end{{array}}\\right)\\)\r\n";// +
                    //$"\\({data.answA[0]},{data.answA[1]},{data.answA[2]},{data.answA[3]}\\)"; 
                    break;
                case  1: //(00)(01)(11)
                    //answA[0]=MatrA[0]; answA[1]=MatrB[1]; answA[2]=MatrC[3];
                    result.Text =
                    $"\\(\\left(\\begin{{array}}{{cc}} <ans[0]:5>      & {data.MatrA[1]}\\\\ {data.MatrA[2]} & {data.MatrA[3]}\\\\ \\end{{array}}\\right)\\) + " + 
                    $"\\(<ans[3]:5>\\)" +
                    $"\\(\\left(\\begin{{array}}{{cc}} {data.MatrB[0]} & <ans[1]:5>     \\\\ {data.MatrB[2]} & {data.MatrB[3]}\\\\ \\end{{array}}\\right)\\) = " + 
                    $"\\(\\left(\\begin{{array}}{{cc}} {data.MatrC[0]} & {data.MatrC[1]}\\\\ {data.MatrC[2]} & <ans[2]:5>     \\\\ \\end{{array}}\\right)\\)\r\n";// +
                    //$"\\({data.answA[0]},{data.answA[1]},{data.answA[2]},{data.answA[3]}\\)"; 
                    break;
                case  2: //(00)(10)(01)
                    //answA[0]=MatrA[0]; answA[1]=MatrB[2]; answA[2]=MatrC[1];
                    result.Text =
                    $"\\(\\left(\\begin{{array}}{{cc}} <ans[0]:5>      & {data.MatrA[1]}\\\\ {data.MatrA[2]} & {data.MatrA[3]}\\\\ \\end{{array}}\\right)\\) + " + 
                    $"\\(<ans[3]:5>\\)" +
                    $"\\(\\left(\\begin{{array}}{{cc}} {data.MatrB[0]} & {data.MatrB[1]}\\\\ <ans[1]:5>      & {data.MatrB[3]}\\\\ \\end{{array}}\\right)\\) = " + 
                    $"\\(\\left(\\begin{{array}}{{cc}} {data.MatrC[0]} & <ans[2]:5>     \\\\ {data.MatrC[2]} & {data.MatrC[3]}\\\\ \\end{{array}}\\right)\\)\r\n";// +
                    //$"\\({data.answA[0]},{data.answA[1]},{data.answA[2]},{data.answA[3]}\\)"; 
                    break;
                case  3: //(00)(10)(11)
                    //answA[0]=MatrA[0]; answA[1]=MatrB[2]; answA[2]=MatrC[3];
                    result.Text =
                    $"\\(\\left(\\begin{{array}}{{cc}} <ans[0]:5>      & {data.MatrA[1]}\\\\ {data.MatrA[2]} & {data.MatrA[3]}\\\\ \\end{{array}}\\right)\\) + " + 
                    $"\\(<ans[3]:5>\\)" +
                    $"\\(\\left(\\begin{{array}}{{cc}} {data.MatrB[0]} & {data.MatrB[1]}\\\\ <ans[1]:5>      & {data.MatrB[3]}\\\\ \\end{{array}}\\right)\\) = " + 
                    $"\\(\\left(\\begin{{array}}{{cc}} {data.MatrC[0]} & {data.MatrC[1]}\\\\ {data.MatrC[2]} & <ans[2]:5>     \\\\ \\end{{array}}\\right)\\)\r\n";// +
                    //$"\\({data.answA[0]},{data.answA[1]},{data.answA[2]},{data.answA[3]}\\)"; 
                    break;
                case  4: //(00)(11)(01)
                    //answA[0]=MatrA[0]; answA[1]=MatrB[3]; answA[2]=MatrC[1];
                    result.Text =
                    $"\\(\\left(\\begin{{array}}{{cc}} <ans[0]:5>      & {data.MatrA[1]}\\\\ {data.MatrA[2]} & {data.MatrA[3]}\\\\ \\end{{array}}\\right)\\) + " + 
                    $"\\(<ans[3]:5>\\)" +
                    $"\\(\\left(\\begin{{array}}{{cc}} {data.MatrB[0]} & {data.MatrB[1]}\\\\ {data.MatrB[2]} & <ans[1]:5>     \\\\ \\end{{array}}\\right)\\) = " + 
                    $"\\(\\left(\\begin{{array}}{{cc}} {data.MatrC[0]} & <ans[2]:5>     \\\\ {data.MatrC[2]} & {data.MatrC[3]}\\\\ \\end{{array}}\\right)\\)\r\n";// +
                    //$"\\({data.answA[0]},{data.answA[1]},{data.answA[2]},{data.answA[3]}\\)"; 
                    break;
                case  5: //(00)(11)(10)
                    //answA[0]=MatrA[0]; answA[1]=MatrB[3]; answA[2]=MatrC[2];
                    result.Text =
                    $"\\(\\left(\\begin{{array}}{{cc}} <ans[0]:5>      & {data.MatrA[1]}\\\\ {data.MatrA[2]} & {data.MatrA[3]}\\\\ \\end{{array}}\\right)\\) + " + 
                    $"\\(<ans[3]:5>\\)" +
                    $"\\(\\left(\\begin{{array}}{{cc}} {data.MatrB[0]} & {data.MatrB[1]}\\\\ {data.MatrB[2]} & <ans[1]:5>     \\\\ \\end{{array}}\\right)\\) = " + 
                    $"\\(\\left(\\begin{{array}}{{cc}} {data.MatrC[0]} & {data.MatrC[1]}\\\\ <ans[2]:5>      & {data.MatrC[3]}\\\\ \\end{{array}}\\right)\\)\r\n";// +
                    //$"\\({data.answA[0]},{data.answA[1]},{data.answA[2]},{data.answA[3]}\\)"; 
                    break;
                case  6: //(01)(00)(10)
                    //answA[0]=MatrA[1]; answA[1]=MatrB[0]; answA[2]=MatrC[2];
                    result.Text =
                    $"\\(\\left(\\begin{{array}}{{cc}} {data.MatrA[0]} & <ans[0]:5>     \\\\ {data.MatrA[2]} & {data.MatrA[3]}\\\\ \\end{{array}}\\right)\\) + " + 
                    $"\\(<ans[3]:5>\\)" +
                    $"\\(\\left(\\begin{{array}}{{cc}} <ans[1]:5>      & {data.MatrB[1]}\\\\ {data.MatrB[2]} & {data.MatrB[3]}\\\\ \\end{{array}}\\right)\\) = " + 
                    $"\\(\\left(\\begin{{array}}{{cc}} {data.MatrC[0]} & {data.MatrC[1]}\\\\ <ans[2]:5>      & {data.MatrC[3]}\\\\ \\end{{array}}\\right)\\)\r\n";// +
                    //$"\\({data.answA[0]},{data.answA[1]},{data.answA[2]},{data.answA[3]}\\)"; 
                    break;
                case  7: //(01)(00)(11)
                    //answA[0]=MatrA[1]; answA[1]=MatrB[0]; answA[2]=MatrC[3];
                    result.Text =
                    $"\\(\\left(\\begin{{array}}{{cc}} {data.MatrA[0]} & <ans[0]:5>     \\\\ {data.MatrA[2]} & {data.MatrA[3]}\\\\ \\end{{array}}\\right)\\) + " + 
                    $"\\(<ans[3]:5>\\)" +
                    $"\\(\\left(\\begin{{array}}{{cc}} <ans[1]:5>      & {data.MatrB[1]}\\\\ {data.MatrB[2]} & {data.MatrB[3]}\\\\ \\end{{array}}\\right)\\) = " + 
                    $"\\(\\left(\\begin{{array}}{{cc}} {data.MatrC[0]} & {data.MatrC[1]}\\\\ {data.MatrC[2]} & <ans[2]:5>     \\\\ \\end{{array}}\\right)\\)\r\n";// +
                    //$"\\({data.answA[0]},{data.answA[1]},{data.answA[2]},{data.answA[3]}\\)"; 
                    break;
                case  8: //(01)(10)(00)
                    //answA[0]=MatrA[1]; answA[1]=MatrB[2]; answA[2]=MatrC[0];
                    result.Text =
                    $"\\(\\left(\\begin{{array}}{{cc}} {data.MatrA[0]} & <ans[0]:5>     \\\\ {data.MatrA[2]} & {data.MatrA[3]}\\\\ \\end{{array}}\\right)\\) + " + 
                    $"\\(<ans[3]:5>\\)" +
                    $"\\(\\left(\\begin{{array}}{{cc}} {data.MatrB[0]} & {data.MatrB[1]}\\\\ <ans[1]:5>      & {data.MatrB[3]}\\\\ \\end{{array}}\\right)\\) = " + 
                    $"\\(\\left(\\begin{{array}}{{cc}} <ans[2]:5>      & {data.MatrC[1]}\\\\ {data.MatrC[2]} & {data.MatrC[3]}\\\\ \\end{{array}}\\right)\\)\r\n";// +
                    //$"\\({data.answA[0]},{data.answA[1]},{data.answA[2]},{data.answA[3]}\\)"; 
                    break;
                case  9: //(01)(10)(11)
                    //answA[0]=MatrA[1]; answA[1]=MatrB[2]; answA[2]=MatrC[3];
                    result.Text =
                    $"\\(\\left(\\begin{{array}}{{cc}} {data.MatrA[0]} & <ans[0]:5>     \\\\ {data.MatrA[2]} & {data.MatrA[3]}\\\\ \\end{{array}}\\right)\\) + " + 
                    $"\\(<ans[3]:5>\\)" +
                    $"\\(\\left(\\begin{{array}}{{cc}} {data.MatrB[0]} & {data.MatrB[1]}\\\\ <ans[1]:5>      & {data.MatrB[3]}\\\\ \\end{{array}}\\right)\\) = " + 
                    $"\\(\\left(\\begin{{array}}{{cc}} {data.MatrC[0]} & {data.MatrC[1]}\\\\ {data.MatrC[2]} & <ans[2]:5>     \\\\ \\end{{array}}\\right)\\)\r\n";// +
                    //$"\\({data.answA[0]},{data.answA[1]},{data.answA[2]},{data.answA[3]}\\)"; 
                    break;
                case 10: //(01)(11)(00)
                    //answA[0]=MatrA[1]; answA[1]=MatrB[3]; answA[2]=MatrC[0];
                    result.Text =
                    $"\\(\\left(\\begin{{array}}{{cc}} {data.MatrA[0]} & <ans[0]:5>     \\\\ {data.MatrA[2]} & {data.MatrA[3]}\\\\ \\end{{array}}\\right)\\) + " + 
                    $"\\(<ans[3]:5>\\)" +
                    $"\\(\\left(\\begin{{array}}{{cc}} {data.MatrB[0]} & {data.MatrB[1]}\\\\ {data.MatrB[2]} & <ans[1]:5>     \\\\ \\end{{array}}\\right)\\) = " + 
                    $"\\(\\left(\\begin{{array}}{{cc}} <ans[2]:5>      & {data.MatrC[1]}\\\\ {data.MatrC[2]} & {data.MatrC[3]}\\\\ \\end{{array}}\\right)\\)\r\n";// +
                    //$"\\({data.answA[0]},{data.answA[1]},{data.answA[2]},{data.answA[3]}\\)"; 
                    break;
                case 11: //(01)(11)(10)
                    //answA[0]=MatrA[1]; answA[1]=MatrB[3]; answA[2]=MatrC[2];
                    result.Text =
                    $"\\(\\left(\\begin{{array}}{{cc}} {data.MatrA[0]} & <ans[0]:5>     \\\\ {data.MatrA[2]} & {data.MatrA[3]}\\\\ \\end{{array}}\\right)\\) + " + 
                    $"\\(<ans[3]:5>\\)" +
                    $"\\(\\left(\\begin{{array}}{{cc}} {data.MatrB[0]} & {data.MatrB[1]}\\\\ {data.MatrB[2]} & <ans[1]:5>     \\\\ \\end{{array}}\\right)\\) = " + 
                    $"\\(\\left(\\begin{{array}}{{cc}} {data.MatrC[0]} & {data.MatrC[1]}\\\\ <ans[2]:5>      & {data.MatrC[3]}\\\\ \\end{{array}}\\right)\\)\r\n";// +
                    //$"\\({data.answA[0]},{data.answA[1]},{data.answA[2]},{data.answA[3]}\\)"; 
                    break;
                case 12: //(10)(00)(01)
                    //answA[0]=MatrA[2]; answA[1]=MatrB[0]; answA[2]=MatrC[1];
                    result.Text =
                    $"\\(\\left(\\begin{{array}}{{cc}} {data.MatrA[0]} & {data.MatrA[1]}\\\\ <ans[0]:5>      & {data.MatrA[3]}\\\\ \\end{{array}}\\right)\\) + " + 
                    $"\\(<ans[3]:5>\\)" +
                    $"\\(\\left(\\begin{{array}}{{cc}} <ans[1]:5>      & {data.MatrB[1]}\\\\ {data.MatrB[2]} & {data.MatrB[3]}\\\\ \\end{{array}}\\right)\\) = " + 
                    $"\\(\\left(\\begin{{array}}{{cc}} {data.MatrC[0]} & <ans[2]:5>     \\\\ {data.MatrC[2]} & {data.MatrC[3]}\\\\ \\end{{array}}\\right)\\)\r\n";// +
                    //$"\\({data.answA[0]},{data.answA[1]},{data.answA[2]},{data.answA[3]}\\)"; 
                    break;
                case 13: //(10)(00)(11)
                    //answA[0]=MatrA[2]; answA[1]=MatrB[0]; answA[2]=MatrC[3];
                    result.Text =
                    $"\\(\\left(\\begin{{array}}{{cc}} {data.MatrA[0]} & {data.MatrA[1]}\\\\ <ans[0]:5>      & {data.MatrA[3]}\\\\ \\end{{array}}\\right)\\) + " + 
                    $"\\(<ans[3]:5>\\)" +
                    $"\\(\\left(\\begin{{array}}{{cc}} <ans[1]:5>      & {data.MatrB[1]}\\\\ {data.MatrB[2]} & {data.MatrB[3]}\\\\ \\end{{array}}\\right)\\) = " + 
                    $"\\(\\left(\\begin{{array}}{{cc}} {data.MatrC[0]} & {data.MatrC[1]}\\\\ {data.MatrC[2]} & <ans[2]:5>     \\\\ \\end{{array}}\\right)\\)\r\n";// +
                    //$"\\({data.answA[0]},{data.answA[1]},{data.answA[2]},{data.answA[3]}\\)"; 
                    break;
                case 14: //(10)(01)(00)
                    //answA[0]=MatrA[2]; answA[1]=MatrB[1]; answA[2]=MatrC[0];
                    result.Text =
                    $"\\(\\left(\\begin{{array}}{{cc}} {data.MatrA[0]} & {data.MatrA[1]}\\\\ <ans[0]:5>      & {data.MatrA[3]}\\\\ \\end{{array}}\\right)\\) + " + 
                    $"\\(<ans[3]:5>\\)" +
                    $"\\(\\left(\\begin{{array}}{{cc}} {data.MatrB[0]} & <ans[1]:5>     \\\\ {data.MatrB[2]} & {data.MatrB[3]}\\\\ \\end{{array}}\\right)\\) = " + 
                    $"\\(\\left(\\begin{{array}}{{cc}} <ans[2]:5>      & {data.MatrC[1]}\\\\ {data.MatrC[2]} & {data.MatrC[3]}\\\\ \\end{{array}}\\right)\\)\r\n";// +
                    //$"\\({data.answA[0]},{data.answA[1]},{data.answA[2]},{data.answA[3]}\\)"; 
                    break;
                case 15: //(10)(01)(11)
                    //answA[0]=MatrA[2]; answA[1]=MatrB[1]; answA[2]=MatrC[3];
                    result.Text =
                    $"\\(\\left(\\begin{{array}}{{cc}} {data.MatrA[0]} & {data.MatrA[1]}\\\\ <ans[0]:5>      & {data.MatrA[3]}\\\\ \\end{{array}}\\right)\\) + " + 
                    $"\\(<ans[3]:5>\\)" +
                    $"\\(\\left(\\begin{{array}}{{cc}} {data.MatrB[0]} & <ans[1]:5>     \\\\ {data.MatrB[2]} & {data.MatrB[3]}\\\\ \\end{{array}}\\right)\\) = " + 
                    $"\\(\\left(\\begin{{array}}{{cc}} {data.MatrC[0]} & {data.MatrC[1]}\\\\ {data.MatrC[2]} & <ans[2]:5>     \\\\ \\end{{array}}\\right)\\)\r\n";// +
                    //$"\\({data.answA[0]},{data.answA[1]},{data.answA[2]},{data.answA[3]}\\)"; 
                    break;
                case 16: //(10)(11)(00)
                    //answA[0]=MatrA[2]; answA[1]=MatrB[3]; answA[2]=MatrC[0];
                    result.Text =
                    $"\\(\\left(\\begin{{array}}{{cc}} {data.MatrA[0]} & {data.MatrA[1]}\\\\ <ans[0]:5>      & {data.MatrA[3]}\\\\ \\end{{array}}\\right)\\) + " + 
                    $"\\(<ans[3]:5>\\)" +
                    $"\\(\\left(\\begin{{array}}{{cc}} {data.MatrB[0]} & {data.MatrB[1]}\\\\ {data.MatrB[2]} & <ans[1]:5>     \\\\ \\end{{array}}\\right)\\) = " + 
                    $"\\(\\left(\\begin{{array}}{{cc}} <ans[2]:5>      & {data.MatrC[1]}\\\\ {data.MatrC[2]} & {data.MatrC[3]}\\\\ \\end{{array}}\\right)\\)\r\n";// +
                    //$"\\({data.answA[0]},{data.answA[1]},{data.answA[2]},{data.answA[3]}\\)"; 
                    break;
                case 17: //(10)(11)(01)
                    //answA[0]=MatrA[2]; answA[1]=MatrB[3]; answA[2]=MatrC[1];
                    result.Text =
                    $"\\(\\left(\\begin{{array}}{{cc}} {data.MatrA[0]} & {data.MatrA[1]}\\\\ <ans[0]:5>      & {data.MatrA[3]}\\\\ \\end{{array}}\\right)\\) + " + 
                    $"\\(<ans[3]:5>\\)" +
                    $"\\(\\left(\\begin{{array}}{{cc}} {data.MatrB[0]} & {data.MatrB[1]}\\\\ {data.MatrB[2]} & <ans[1]:5>     \\\\ \\end{{array}}\\right)\\) = " + 
                    $"\\(\\left(\\begin{{array}}{{cc}} {data.MatrC[0]} & <ans[2]:5>     \\\\ {data.MatrC[2]} & {data.MatrC[3]}\\\\ \\end{{array}}\\right)\\)\r\n";// +
                    //$"\\({data.answA[0]},{data.answA[1]},{data.answA[2]},{data.answA[3]}\\)"; 
                    break;
                case 18: //(11)(00)(01)
                    //answA[0]=MatrA[3]; answA[1]=MatrB[0]; answA[2]=MatrC[1];
                    result.Text =
                    $"\\(\\left(\\begin{{array}}{{cc}} {data.MatrA[0]} & {data.MatrA[1]}\\\\ {data.MatrA[2]} & <ans[0]:5>     \\\\ \\end{{array}}\\right)\\) + " + 
                    $"\\(<ans[3]:5>\\)" +
                    $"\\(\\left(\\begin{{array}}{{cc}} <ans[1]:5>      & {data.MatrB[1]}\\\\ {data.MatrB[2]} & {data.MatrB[3]}\\\\ \\end{{array}}\\right)\\) = " + 
                    $"\\(\\left(\\begin{{array}}{{cc}} {data.MatrC[0]} & <ans[2]:5>     \\\\ {data.MatrC[2]} & {data.MatrC[3]}\\\\ \\end{{array}}\\right)\\)\r\n";// +
                    //$"\\({data.answA[0]},{data.answA[1]},{data.answA[2]},{data.answA[3]}\\)"; 
                    break;
                case 19: //(11)(00)(10)
                    //answA[0]=MatrA[3]; answA[1]=MatrB[0]; answA[2]=MatrC[2];
                    result.Text =
                    $"\\(\\left(\\begin{{array}}{{cc}} {data.MatrA[0]} & {data.MatrA[1]}\\\\ {data.MatrA[2]} & <ans[0]:5>     \\\\ \\end{{array}}\\right)\\) + " + 
                    $"\\(<ans[3]:5>\\)" +
                    $"\\(\\left(\\begin{{array}}{{cc}} <ans[1]:5>      & {data.MatrB[1]}\\\\ {data.MatrB[2]} & {data.MatrB[3]}\\\\ \\end{{array}}\\right)\\) = " + 
                    $"\\(\\left(\\begin{{array}}{{cc}} {data.MatrC[0]} & {data.MatrC[1]}\\\\ <ans[2]:5>      & {data.MatrC[3]}\\\\ \\end{{array}}\\right)\\)\r\n";// +
                    //$"\\({data.answA[0]},{data.answA[1]},{data.answA[2]},{data.answA[3]}\\)"; 
                    break;
                case 20: //(11)(01)(00)
                    //answA[0]=MatrA[3]; answA[1]=MatrB[1]; answA[2]=MatrC[0];
                    result.Text =
                    $"\\(\\left(\\begin{{array}}{{cc}} {data.MatrA[0]} & {data.MatrA[1]}\\\\ {data.MatrA[2]} & <ans[0]:5>     \\\\ \\end{{array}}\\right)\\) + " + 
                    $"\\(<ans[3]:5>\\)" +
                    $"\\(\\left(\\begin{{array}}{{cc}} {data.MatrB[0]} & <ans[1]:5>     \\\\ {data.MatrB[2]} & {data.MatrB[3]}\\\\ \\end{{array}}\\right)\\) = " + 
                    $"\\(\\left(\\begin{{array}}{{cc}} <ans[2]:5>      & {data.MatrC[1]}\\\\ {data.MatrC[2]} & {data.MatrC[3]}\\\\ \\end{{array}}\\right)\\)\r\n";// +
                    //$"\\({data.answA[0]},{data.answA[1]},{data.answA[2]},{data.answA[3]}\\)"; 
                    break;
                case 21: //(11)(01)(10)
                    //answA[0]=MatrA[3]; answA[1]=MatrB[1]; answA[2]=MatrC[2];
                    result.Text =
                    $"\\(\\left(\\begin{{array}}{{cc}} {data.MatrA[0]} & {data.MatrB[1]}\\\\ {data.MatrA[2]} & <ans[0]:5>     \\\\ \\end{{array}}\\right)\\) + " + 
                    $"\\(<ans[3]:5>\\)" +
                    $"\\(\\left(\\begin{{array}}{{cc}} {data.MatrB[0]} & <ans[1]:5>     \\\\ {data.MatrB[2]} & {data.MatrB[3]}\\\\ \\end{{array}}\\right)\\) = " + 
                    $"\\(\\left(\\begin{{array}}{{cc}} {data.MatrC[0]} & {data.MatrC[1]}\\\\ <ans[2]:5>      & {data.MatrC[3]}\\\\ \\end{{array}}\\right)\\)\r\n";// +
                    //$"\\({data.answA[0]},{data.answA[1]},{data.answA[2]},{data.answA[3]}\\)"; 
                    break;
                case 22: //(11)(10)(00)
                    //answA[0]=MatrA[3]; answA[1]=MatrB[2]; answA[2]=MatrC[0];
                    result.Text =
                    $"\\(\\left(\\begin{{array}}{{cc}} {data.MatrA[0]} & {data.MatrA[1]}\\\\ {data.MatrA[2]} & <ans[0]:5>     \\\\ \\end{{array}}\\right)\\) + " + 
                    $"\\(<ans[3]:5>\\)" +
                    $"\\(\\left(\\begin{{array}}{{cc}} {data.MatrB[0]} & {data.MatrB[1]}\\\\ <ans[1]:5>      & {data.MatrB[3]}\\\\ \\end{{array}}\\right)\\) = " + 
                    $"\\(\\left(\\begin{{array}}{{cc}} <ans[2]:5>      & {data.MatrC[1]}\\\\ {data.MatrC[2]} & {data.MatrC[3]}\\\\ \\end{{array}}\\right)\\)\r\n";// +
                    //$"\\({data.answA[0]},{data.answA[1]},{data.answA[2]},{data.answA[3]}\\)"; 
                    break;
                case 23: //(11)(10)(01)
                    //answA[0]=MatrA[3]; answA[1]=MatrB[2]; answA[2]=MatrC[1];
                    result.Text =
                    $"\\(\\left(\\begin{{array}}{{cc}} {data.MatrA[0]} & {data.MatrA[1]}\\\\ {data.MatrA[2]} & <ans[0]:5>     \\\\ \\end{{array}}\\right)\\) + " + 
                    $"\\(<ans[3]:5>\\)" +
                    $"\\(\\left(\\begin{{array}}{{cc}} {data.MatrB[0]} & {data.MatrB[1]}\\\\ <ans[1]:5>      & {data.MatrB[3]}\\\\ \\end{{array}}\\right)\\) = " + 
                    $"\\(\\left(\\begin{{array}}{{cc}} {data.MatrC[0]} & <ans[2]:5>     \\\\ {data.MatrC[2]} & {data.MatrC[3]}\\\\ \\end{{array}}\\right)\\)\r\n";// +
                    //$"\\({data.answA[0]},{data.answA[1]},{data.answA[2]},{data.answA[3]}\\)"; 
                    break;
            }
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
