using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Power
{
    public class PowerOperations
    {
        double result = 0;
        double x = 0;
        double y = 0;


        public double resultz()
        {
            Console.WriteLine("Select an operation:");
            Console.WriteLine("1. x^y (Exponent)");
            Console.WriteLine("2. x^(1/y) (Root)");
            Console.WriteLine("3. sin");
            Console.WriteLine("4. cos");
            Console.WriteLine("5. tan");
            //    int opeRator = int.Parse(Console.ReadLine());

            //    Console.WriteLine("Enter x :");
            //    x = double.Parse(Console.ReadLine());
            //    Console.WriteLine("Enter y :");
            //    y = double.Parse(Console.ReadLine());
            //    if (opeRator == 1)
            //    {
            //        return Math.Pow(x, y);

            //    }
            //    else if (opeRator == 2)
            //    {
            //        return Math.Pow(x, 1.0 / y);
            //    }

            //    return 0;

            //}

            ConsoleKey ck = Console.ReadKey().Key;
            switch (ck)
            {

                case ConsoleKey.S:

                    break;
                case ConsoleKey.C:
                    break;
                default:
                    break;

            }
            return 0;
        }
    }
}
