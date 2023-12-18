using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arithematic
{
  
    public class ArithematicOperations
    {

        double num1, num2, num;
        double result = 0;
        char opeRator;
        bool flag = true;


        

        public double resultz()
        {
            StringBuilder inputExpression = new StringBuilder();
            int count = 0; 
            while (true)
            {

                Console.WriteLine("Entering String until =");
                ConsoleKeyInfo k1 = Console.ReadKey();  
                if (k1.KeyChar == '=')
                {
                    Console.WriteLine(inputExpression.ToString());
                    break;
                }
                else
                {
                    if (k1.Key == ConsoleKey.Backspace)
                    {
                        if (count>0)
                        {
                            inputExpression.Remove(count - 1, 1);
                            Console.WriteLine(inputExpression.ToString() + " " + count);
                            Console.WriteLine("Backsapce");
                            count--;
                        }
                       
                    }
                    else
                    {
                        inputExpression.Append(k1.KeyChar);
                        count++;
                    }
                    
                }


                Console.Clear();
                Console.WriteLine(inputExpression.ToString());

            }

            return 1.2;
            //while (opeRator != '=')
            //{
            //    Console.WriteLine("Enter the opeRator (+, -, *, /, %,=): ");
            //    opeRator = char.Parse(Console.ReadLine());



            //    //Console.WriteLine("Enter the first number: ");
            //    //num = double.Parse(Console.ReadLine());

            //    //Console.WriteLine("Enter the first number: ");
            //    //num1 = double.Parse(Console.ReadLine());

            //    //Console.WriteLine("Enter the second number: ");
            //    //num2 = double.Parse(Console.ReadLine());

            //    inputExpression += num;

            //}




            //if (opeRator == '+')
            //{
            //    return num1 + num2;
            //}
            //else if (opeRator == '-')
            //{
            //    return num1 - num2;
            //}
            //else if (opeRator == '*')
            //{
            //    return num1 * num2;
            //}
            //else if (opeRator == '/')
            //{
            //    if (num2 == 0)
            //    {
            //        Console.WriteLine("Error: Division by zero.");
            //    }
            //    else
            //    {
            //        return num1 / num2;
            //    }
            //}
            //else if (opeRator == '%')
            //{
            //    if (num2 == 0)
            //    {
            //        Console.WriteLine("Error: Modulus by zero .");
            //    }
            //    else
            //    {
            //        return num1 % num2;
            //    }
            //}
            //return 0;
        } 
      }
    }
