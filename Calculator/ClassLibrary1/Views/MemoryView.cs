using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;
using CalculatorComponent.Data;

namespace CalculatorComponent.Views
{
    public class MemoryView
    {
        CustomStack<double> cs = null;
        public MemoryView(CustomStack<double> cs1)
        {
            cs = cs1;
        }
        public void CreateMemoryView()
        {



            Console.WriteLine(CalculatorCommonOutput.CreateMemoryHeading());
            Console.WriteLine("# Press [=] to hide");
            cs.Display();


        }
    }
}
