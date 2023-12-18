using CalculatorComponent.Data;
using CalculatorComponent.Views;

namespace CalculatorComponent
{
    public class CalculatorObjectFactory
    {
        private static CalculatorScientificView _calculatorScientificView = null;
        private static CalculatorArithematicView _calculatorArithematicView = null;
        private static MemoryView _memoryView = null;
        private static HelpView _helpView = null;


        public static MemoryView memoryViewObject(CustomStack<double> cs)
        {
            if (_memoryView==null)
            {
                return new MemoryView(cs);
            }
            return _memoryView;
        }

        public static HelpView helpViewObject()
        {
            if (_helpView == null)
            {
                return new HelpView();
            }
            return _helpView;
        }

        public static CalculatorScientificView calculatorScientificViewObject()
        {
            if (_calculatorScientificView == null)
            {
                return new CalculatorScientificView();
            }
            return _calculatorScientificView;
        }

        public static CalculatorArithematicView calculatorArithematicViewObject()
        {
            if (_calculatorArithematicView==null)
            {
                return new CalculatorArithematicView();
            }
            return _calculatorArithematicView;  
        }


    }
}