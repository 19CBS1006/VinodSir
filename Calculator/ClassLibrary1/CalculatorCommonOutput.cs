using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculatorComponent
{
    public class CalculatorCommonOutput
    {
        public static string GetInstructions()
        {
            return $"Press \t[A] Arithematic Operations(Scientific Calculator Arithematic and power)\n\t [P]Std Calculator (To Be Implemented) ,\n\t[esc] Escape key to Exit!" ;
            
        }
        public static string CreateArithematicHeading()
        {
            string heading = $"Arithematic";
            string underline = GetUnderlineForHeading(heading);
            return $"{heading}\n{underline}{Environment.NewLine}";
        }

        public static string CreateHelpHeading()
        {
            string heading = $"\t\t\t\tImplementation Till now and Key Combinatins";
            string underline = GetUnderlineForHeading(heading);
            return $"{heading}\n\t\t\t\t{underline}{Environment.NewLine}";
        }
        public static string GetUnderlineForHeading(string head)
        {
            return new string('-',head.Length);
        }
        public static string CreateScientificHeading()
        {
            string heading = $"Scientific";
            string underline = GetUnderlineForHeading(heading);
            return $"{heading}\n{underline}{Environment.NewLine}";
        }
        public static string CreateMemoryHeading()
        {
            string heading = $"Memory";
            string underline = GetUnderlineForHeading(heading);
            return $"{heading}\n{underline}{Environment.NewLine}";
        }

        public static string CreateStatus(string state)
        {
            string status = $"{state}";
            return status ;
        }
    }
}
