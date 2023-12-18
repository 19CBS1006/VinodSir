using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculatorComponent.Views
{
    public class CalculatorArithematicView
    {
        private string _str = null;

        public static int _num = 0;
        public static bool blnHint = true;

        public CalculatorArithematicView()
        {
            _num++;
        }
        //public CalculatorArithematicView(string str1)
        //{
        //    _str = str1;
        //}
        public void CreateArithematicView(string str)
        {
            Console.Clear();
            if (blnHint)
            {
                Console.WriteLine("\t\t\t\t------------Hints----------------");
                Console.WriteLine($"\t\t\t\t--Press [ctrl+L] to view memmory--");
                Console.WriteLine($"\t\t\t\t--When the combination of key is pressed its not shown on console--");
                Console.WriteLine($"\t\t\t\t--Press [tab]  to hide hints --");
            }
          

            Console.WriteLine(CalculatorCommonOutput.CreateArithematicHeading());
            Console.WriteLine($"\t\t\t\t--Press [H] to know key combinations--");
            Console.WriteLine(CalculatorCommonOutput.CreateStatus($"{str}"));
            
        }
    }
}
