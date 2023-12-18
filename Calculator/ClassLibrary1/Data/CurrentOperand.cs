using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculatorComponent.Data
{



    //this class for for knowing the recent operand
    //# temp result pending..
    public class CurrentOperand
    {


        public  double Final_Res_Current(string str)
        {
            bool flagFinished = false;
            char opPrev = ' ';
            double result = 0;
            char[] ch = str.ToCharArray();
            int count = 0;
            Boolean flagFirstNum = false;
            double a = 0;
            String str1 = "";
            String str2 = "";
            char op = ' ';
            double b = 0;
            Boolean flagSecondNum = false;
            double temp = 0;
            char temPop = ' ';
            int opCount = 0;
            bool flagPrecedence = false;
            bool flagBracketEnd = false;
            bool flagBracket = false;
            double temp1 = 0;
            int z = 0;
            char temPop1 = ' ';
            bool flagRunAgain = false;
            double operandMulBeforBrac = 1;
            bool flagCarry = false;
            int bracCount = 0;
            double[] arr = new double[100];
            bool flageIgnoreCarry = false;
            bool flagCheckIfResult = false;
            bool flagMulCarryToA = false;
            bool flagCorrectCarry = false;
            bool flagPrecedenceSolved = false;






            for (int i = 0; i < str.Length; i++)
            {

                //if bracket opening is encountered
                if (ch[i] == '(')
                {
                    flagCarry = true;
                    bracCount++;
                    temPop = op;

                    //if bracket count greater than 1
                    if (bracCount > 1)
                    {
                        //if there is precedence
                        if (flagPrecedence)
                        {
                            if (flagCheckIfResult)
                            {
                                Console.WriteLine("k");

                                operandMulBeforBrac = result * arr[bracCount - 1];
                            }
                            else
                            {
                                operandMulBeforBrac = b * arr[bracCount - 1];
                            }
                            op = temPop1;
                            result = 0;
                            count = 0;
                            a = temp1;
                            flagFirstNum = true;
                            opCount = 0;
                        }
                        else
                        {
                            if (op == '-')
                            {
                                if (flagCheckIfResult)
                                {
                                    flageIgnoreCarry = true;
                                    a = result;

                                }

                                operandMulBeforBrac *= -1;

                                op = '+';
                                flagFirstNum = true;
                                str2 = "";
                            }
                            if (op == '+')
                            {
                                if (flagCheckIfResult)
                                {
                                    flageIgnoreCarry = true;
                                    a = result;

                                }

                                operandMulBeforBrac *= 1;

                                op = '+';
                                flagFirstNum = true;
                                str2 = "";

                            }
                            else if (op == '*' || op == '/')
                            {

                                if (flagCheckIfResult)
                                {
                                    flageIgnoreCarry = true;
                                    operandMulBeforBrac = result * arr[bracCount - 1];
                                }
                                else
                                {
                                    operandMulBeforBrac = a * arr[bracCount - 1];
                                }
                                result = 0;
                                count = 0;
                                str1 = "";
                                str2 = "";
                                flagFirstNum = false;
                                flagSecondNum = false;
                                opCount = 0;
                                flagMulCarryToA = true;
                            }
                        }
                    }
                    else
                    {
                        if (flagPrecedence)
                        {
                            a = temp1;
                            flagFirstNum = true;
                            if (flagCheckIfResult)
                            {
                                operandMulBeforBrac = result;
                            }
                            else
                            {
                                operandMulBeforBrac = b;
                            }

                            op = temPop1;
                        }
                        else
                        {
                            if (flagCheckIfResult)
                            {
                                flageIgnoreCarry = true;
                                flagFirstNum = false;
                                if (op == '-')
                                {

                                    a = result;
                                    operandMulBeforBrac = -1;
                                    op = '+';
                                    str2 = "";

                                }
                                else if (op == '+')
                                {

                                    a = result;
                                    operandMulBeforBrac = 1;
                                    op = '+';

                                }
                                else if (op == '*' || op == '/')
                                {

                                    Console.WriteLine("Working");
                                    operandMulBeforBrac = result;
                                    result = 0;
                                    count = 0;
                                    str1 = "";
                                    str2 = "";
                                    flagSecondNum = false;
                                    opCount = 0;
                                    flagMulCarryToA = true;
                                }
                            }
                            else
                            {

                                flagFirstNum = true;
                                if (op == '-')
                                {
                                    operandMulBeforBrac = -1;
                                    op = '+';
                                    str2 = "";
                                }
                                else if (op == '+')
                                {
                                    operandMulBeforBrac = 1;
                                    op = '+';
                                }

                                else if (op == '*' || op == '/')
                                {
                                    Console.WriteLine("Working");
                                    operandMulBeforBrac = a;
                                    result = 0;
                                    count = 0;
                                    str1 = "";
                                    str2 = "";

                                    flagSecondNum = false;
                                    opCount = 0;
                                    flagMulCarryToA = true;
                                }
                            }
                        }
                    }

                    arr[bracCount] = operandMulBeforBrac;
                    flagPrecedence = false;

                }


                //for computaition with result
                if (ch[i] != '+' && ch[i] != '*' && ch[i] != '/' && ch[i] != '%' && ch[i] != '(' && ch[i] != ')' && !flagSecondNum && flagFirstNum)
                {
                    str2 += ch[i];
                    if (i + 1 < ch.Length && (ch[i + 1] == '-' || ch[i + 1] == '+' || ch[i + 1] == ')' || ch[i + 1] == '*' || ch[i + 1] == '/' || ch[i + 1] == '%'))
                    {

                        flagSecondNum = true;
                      
                        b = double.Parse(str2);
                        if (op == '-')
                        {
                            op = '+';
                        }
                    }
                    else if (i + 1 == ch.Length)
                    {
                        if (op == '-')
                        {
                            op = '+';
                        }
                        b = double.Parse(str2);
                        flagSecondNum = true;
                    }
                }

             



                //for a first time or temp start
                if (count == 0 && ch[i] != '+' && !flagFirstNum && ch[i] != '(' && ch[i] != '*' && ch[i] != '/' && ch[i] != '%')
                {
                    str1 += ch[i];
             
                   

                    if (i + 1 < ch.Length - 1)
                    {
                        if (ch[i + 1] == '-' || ch[i + 1] == '+' || ch[i + 1] == '*' || ch[i + 1] == '/' || ch[i + 1] == '%')
                        {
                            flagFirstNum = true;
                            a = double.Parse(str1);
                       
                            if (ch[i + 1] == '*' && !flagPrecedence)
                            {
                                Console.WriteLine("Err");
                                flagMulCarryToA = false;
                            }
                        }

                    }
                   
                }

                //for  single function a calculation

                if (i == ch.Length - 1 && !flagSecondNum && !flagFirstNum )
                {
                   
                        if (ch[i] != '-' && ch[i] != '+' && ch[i] != '*' && ch[i] != '/')
                        {
                            a = double.Parse(str1);
                            return a;
                        }

                  
                }



                //If next char is * or /
                if (flagSecondNum && op != '*' && op != '/' && i + 1 < ch.Length && (ch[i + 1] == '*' || ch[i + 1] == '/'))
                {

                    flagPrecedence = true;

                    //If current operator and upcoming operator are * or /
                    if (op != (char)((int)ch[i + 1] - 5) && op != (char)((int)ch[i + 1] + 5) && op != ch[i + 1])
                    {

                        if (count == 0 || flagCheckIfResult)
                        {
                            temp1 = a;
                        }
                        else
                        {
                            temp1 = result;
                        }
                        temPop1 = op;
                        a = b;
                        count = 0;
                        str1 = "";
                        str2 = "";
                        result = 0;
                        flagSecondNum = false;
                    }

                }



                //if a and b or result and b
                if (flagFirstNum && flagSecondNum)
                {
                    if (i == str.Length - 1)
                    {
                        return b;
                    }
                    if ((i + 1 < ch.Length && !flagPrecedence && flagCarry && ch[i + 1] != '*') || (!flagCarry && flagCorrectCarry))
                    {
                        
                        
                        Console.WriteLine("this is called:-->");
                        if (flagMulCarryToA)
                        {
                            Console.WriteLine("flagcarrytoa");
                            if (flagCorrectCarry)
                            {
                                a *= arr[bracCount + 1];
                            }
                            else
                            {
                                a *= operandMulBeforBrac;
                            }
                            flagMulCarryToA = false;
                        }

                        if (flagCorrectCarry)
                        {
                            b *= arr[bracCount + 1];
                            flagCorrectCarry = false;
                        }
                        else
                        {
                            b *= operandMulBeforBrac;
                        }
                    }

                    flagRunAgain = true;

                    if (op == '+')
                    {

                        if (count == 0)
                        {
                            result += (a + b);
                            count++;
                            //Console.WriteLine("Result : " + " " + a + " " + b + " " + result);
                        }
                        else
                        {
                            result += b;
                            //Console.WriteLine("Result : " + " " + b + " " + result);
                        }
                    }
                    //if (op == '-')
                    //{

                    //    if (count == 0)
                    //    {
                    //        result += (a - b);
                    //        count++;

                    //        //Console.WriteLine("Result : " + a + " " + b + " " + result);
                    //    }
                    //    else
                    //    {
                    //        result -= b;
                    //        //Console.WriteLine("Result : " + " " + b + " " + result);
                    //    }

                    //}

                    if (op == '*')
                    {
                        if (count == 0)
                        {
                            result += (a * b);
                            count++;

                            //Console.WriteLine("Result : " + a + " " + b + " " + result);
                        }
                        else
                        {
                            result *= b;
                            //Console.WriteLine("Result : " + " " + b + " " + result);
                        }

                    }
                    if (op == '/')
                    {
                        if (count == 0)
                        {
                            result += (a / b);
                            count++;

                            //Console.WriteLine("Result : " + a + " " + b + " " + result);
                        }
                        else
                        {
                            result /= b;
                            //Console.WriteLine("Result : " + " " + b + " " + result);
                        }

                    }
                    if (op == '%')
                    {

                        if (count == 0)
                        {
                            result += (a % b);
                            count++;

                            //Console.WriteLine("Result : " + a + " " + b + " " + result);
                        }
                        else
                        {
                            result %= b;
                            //Console.WriteLine("Result : " + " " + b + " " + result);
                        }

                    }
                    flagSecondNum = false;
                    str2 = "";
                    if (flageIgnoreCarry)
                    {
                        flageIgnoreCarry = false;
                    }
                    if (flagCheckIfResult)
                    {
                        flagCheckIfResult = false;
                    }

                    if (i + 2 < ch.Length && ch[i + 2] == '(')
                    {
                        flagCheckIfResult = true;
                    }
                }

                //operator assigned
                if (ch[i] == '+' || ch[i] == '-' || ch[i] == '*' || ch[i] == '/' || ch[i] == '%')
                {
                    opPrev = op;
                    op = (char)ch[i];
                    opCount++;
                }

                //if closing bracket encountered
                if (!flagFinished && i + 1 < ch.Length && ch[i + 1] == ')')
                {

                    if (flagPrecedence)
                    {
                        flagCorrectCarry = true;
                    }

                    if (bracCount == 1)
                    {
                        flagCarry = false;
                        flagFinished = true;
                    }

                    operandMulBeforBrac = arr[bracCount - 1];
                    bracCount--;


                    a = result;
                    flagFirstNum = true;
                    flagBracket = false;

                }

                //Solve Precedence
                if ((flagPrecedence && i + 1 == ch.Length) || (flagPrecedence && i + 1 < ch.Length && (ch[i + 1] == '+' || ch[i + 1] == '-' || ch[i + 1] == ')')))
                {
                    flagPrecedence = false;
                    flagPrecedenceSolved = true;
                    count = 0;
                    op = temPop1;
                    a = temp1;
                    b = result;
                    result = 0;
                    flagFirstNum = true;
                    flagSecondNum = true;
                    i--;
                }
            }
            return 0;

           
        }

    }
}
