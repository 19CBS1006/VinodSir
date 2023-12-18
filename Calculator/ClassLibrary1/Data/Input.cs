using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using CalculatorComponent.Views;

namespace CalculatorComponent.Data
{
    public class Input
    {
        int threashold;
        int incr;
        public Input(int thPass)
        {
            threashold = thPass;

        }

        public void Add(int x)
        {
            incr += x;

            if (incr > threashold)
            {
                //args to store event details
                ThreasholdReachedArgs args = new ThreasholdReachedArgs();
                args.threashold = threashold;
                args.TimeDone = DateTime.Now;
                onThresholdReached(args);
            }
        }
        public void FetchRes(StringBuilder inputExpr,CalculatorArithematicView view)
        {
        
            EqualPressArgs args=new EqualPressArgs();
            Evaluate ev = new Evaluate();
            
            args.xyz= ev.Final_Res(inputExpr.ToString());
            inputExpr.Clear();
            inputExpr.Append(args.xyz);
            args.obj=(CalculatorArithematicView)view;
           

            //args.ConsoleKeyInform = Console.ReadKey(true);
            onequalPressed(args);
        }

        public void MemReadPressed(StringBuilder inputExpr, CustomStack<double> cs)
        {
            MemReadPressArgs args = new MemReadPressArgs();
            CurrentOperand cd = new CurrentOperand();
            double dRes = cd.Final_Res_Current(inputExpr.ToString());
            

            if (inputExpr.Length>1 && ( inputExpr[inputExpr.Length-1]=='+' || inputExpr[inputExpr.Length - 1] == '*' || inputExpr[inputExpr.Length - 1] == '-' || inputExpr[inputExpr.Length - 1] == '/' ))
            {
                //Console.WriteLine("this");
                inputExpr.Append(cs.peek());
            }
            else
            {
                if (inputExpr.Length>0)
                {
                    int startIndex = inputExpr.Length - dRes.ToString().Length;
                    inputExpr.Remove(startIndex, dRes.ToString().Length);
                    if (dRes < 0)
                    {
                        inputExpr.Append('-');
                    }
                }
             
                inputExpr.Append(cs.peek());
            }
            args.xyz = inputExpr.ToString();
            Console.WriteLine(args.xyz);
            onMemReadPressed(args);
        }

        public void MemViewPressed(CustomStack<double> cs)
        {
            MemPressArgs args= new MemPressArgs();
            MemoryView memCreateView = CalculatorObjectFactory.memoryViewObject(cs);
            memCreateView.CreateMemoryView();
            onMemPressed(args);
        }
        public void MemAddPressed(MemoryOperations mO, StringBuilder inputExpr)
        {
            MemAddPressArgs args = new MemAddPressArgs();
            CurrentOperand cd = new CurrentOperand();
            double dRes = cd.Final_Res_Current(inputExpr.ToString());
            if (inputExpr.Length>0)
            {
                mO.MPlus(dRes);
            }
            else
            {
                mO.MPlus(0);
            }
            
            onMemAddPressed(args);
        }

        public void MemMinusPressed( StringBuilder inputExpr, MemoryOperations mO)
        {
            MemMinusPressArgs args = new MemMinusPressArgs();
            CurrentOperand cd = new CurrentOperand();
            double dRes = cd.Final_Res_Current(inputExpr.ToString());
           
            mO.MMinus(dRes);
           

            onMemMinusPressed(args);
        }
        public void MemSavePressed(StringBuilder inputExpr, MemoryOperations mO)
        {
            MemSavePressArgs args = new MemSavePressArgs();
            CurrentOperand cd = new CurrentOperand();
            double dRes = cd.Final_Res_Current(inputExpr.ToString());
            Console.WriteLine(dRes);

            mO.MSave(dRes);


            onMemSavePressed(args);
        }

        public void LogPressed(StringBuilder inputExpr)
        {
            LogPressArgs args = new LogPressArgs();
            CurrentOperand cd = new CurrentOperand();
            double dRes = cd.Final_Res_Current(inputExpr.ToString());
            
            int startIndex = inputExpr.Length - dRes.ToString().Length;
            inputExpr.Remove(startIndex, dRes.ToString().Length);

            double naturalLog = Math.Log(dRes);
            args.xyz=inputExpr.Append(naturalLog).ToString();
            onLogPressed(args);
        }


        public void AbsPressed(StringBuilder inputExpr)
        {
            AbsPressArgs args = new AbsPressArgs();
            CurrentOperand cd = new CurrentOperand();
            double dRes = cd.Final_Res_Current(inputExpr.ToString());

            int startIndex = inputExpr.Length - dRes.ToString().Length;
            inputExpr.Remove(startIndex, dRes.ToString().Length);

            if (dRes<0)
            {
                dRes *= -1;
            }

            //double naturalLog = Math.Log(dRes);
            args.str = inputExpr.Append(dRes).ToString();
            onAbsPressed(args);
        }

        public void CeilPressed(StringBuilder inputExpr)
        {
            CeilPressArgs args = new CeilPressArgs();
            CurrentOperand cd = new CurrentOperand();
            double dRes = cd.Final_Res_Current(inputExpr.ToString());

            int startIndex = inputExpr.Length - dRes.ToString().Length;
            inputExpr.Remove(startIndex, dRes.ToString().Length);

            double ceil = Math.Ceiling(dRes);
            //Console.WriteLine(ceil);
            args.str = inputExpr.Append(ceil).ToString();
            onCeilPressed(args);
        }

        public void FloorPressed(StringBuilder inputExpr)
        {
            FloorPressArgs args = new FloorPressArgs();
            CurrentOperand cd = new CurrentOperand();
            double dRes = cd.Final_Res_Current(inputExpr.ToString());

            int startIndex = inputExpr.Length - dRes.ToString().Length;
            inputExpr.Remove(startIndex, dRes.ToString().Length);

            double floor = Math.Floor(dRes);
            //Console.WriteLine(ceil);
            args.str = inputExpr.Append(floor).ToString();
            onFloorPressed(args);
        }


        public void DegTogPressed(StringBuilder inputExpr)
        {
            DegTogPressArgs args = new DegTogPressArgs();
            CurrentOperand cd = new CurrentOperand();
            //double dRes = cd.Final_Res_Current(inputExpr.ToString());

            //int startIndex = inputExpr.Length - dRes.ToString().Length;
            //inputExpr.Remove(startIndex, dRes.ToString().Length);

            //double floor = Math.Ceiling(dRes);
            //Console.WriteLine(ceil);
            //args.str = inputExpr.Append(floor).ToString();
            //CalculatorArithematicView calculatorArithematicView= CalculatorObjectFactory.calculatorArithematicViewObject();
            //Console.Clear();
            if (CalculatorArithematicView._num % 2 == 0)
            {
                //Console.WriteLine("a");
                //calculatorArithematicView.CreateArithematicView("Deg");
                args.str = "Deg";
            }
            else
            {
                //calculatorArithematicView.CreateArithematicView("Rad");
                args.str = "Rad";
            }
            

            onDegTogPressed(args);
        }



        public void RadPressed(StringBuilder inputExpr)
        {
            RadPressArgs args = new RadPressArgs();
            CurrentOperand cd = new CurrentOperand();
            double dRes = cd.Final_Res_Current(inputExpr.ToString());

        
            double sineValue = Math.Sin(dRes);

            int startIndex = inputExpr.Length - dRes.ToString().Length;
            inputExpr.Remove(startIndex, dRes.ToString().Length);
            if (dRes > 0 && inputExpr.Length - 1 > 0)
            {
                inputExpr.Remove(inputExpr.Length - 1, 1);
            }
            if (sineValue > 0)
            {
                inputExpr.Append("+");
            }


            args.ModifiedString = inputExpr.ToString();
            args.CurrentOperand = sineValue;
            //Console.WriteLine(sineValue);
            onRadPressed(args);
        }

        public void PowPressed(StringBuilder inputExpr)
        {
            PowPressArgs args = new PowPressArgs();
            
            CurrentOperand cd = new CurrentOperand();
            double dRes = cd.Final_Res_Current(inputExpr.ToString());

            int startIndex = inputExpr.Length - dRes.ToString().Length;
            inputExpr.Remove(startIndex, dRes.ToString().Length);
            string str = "";
            ConsoleKeyInfo keyInfo;
            while (true)
            {

                keyInfo = Console.ReadKey();
                if (keyInfo.KeyChar == '+' || keyInfo.KeyChar == '-' || keyInfo.KeyChar == '*' || keyInfo.KeyChar == '-' || keyInfo.KeyChar == '=')
                {
                   
                    break; 
                }

                str += keyInfo.KeyChar;
            }
            double pow = Math.Pow(dRes,double.Parse(str));
            if (keyInfo.KeyChar != '=')
            {
                args.str = inputExpr.Append(pow).Append(keyInfo.KeyChar).ToString();
            }
            else
            {
                args.str = inputExpr.Append(pow).ToString();
            }
           
          
            onPowPressed(args);

        }

        public void ShiftPressed(ConsoleKeyInfo shiftKey,StringBuilder inputExpr)
        {
            ShiftPressArgs args = new ShiftPressArgs();
            args.ShiftPress = shiftKey;
            CurrentOperand cd = new CurrentOperand();
            double dRes = cd.Final_Res_Current(inputExpr.ToString());
           
            double angleInRadians = Math.PI * dRes / 180.0;


          
            double sineValue = Math.Sin(angleInRadians);

            int startIndex = inputExpr.Length - dRes.ToString().Length;
            inputExpr.Remove(startIndex, dRes.ToString().Length);
            if (dRes > 0 && inputExpr.Length - 1 > 0)
            {
                inputExpr.Remove(inputExpr.Length - 1, 1);
            }
            if (sineValue > 0)
            {
                inputExpr.Append("+");
            }


            args.ModifiedString = inputExpr.ToString();
            args.CurrentOperand = sineValue;
            onShiftPressed(args);
        }


        public void CosPressed(ConsoleKeyInfo shiftKey, StringBuilder inputExpr)
        {
            ShiftPressArgs args = new ShiftPressArgs();
            args.ShiftPress = shiftKey;
            CurrentOperand cd = new CurrentOperand();
            double dRes = cd.Final_Res_Current(inputExpr.ToString());

            double angleInRadians = Math.PI * dRes / 180.0;
            double cosValue = Math.Cos(angleInRadians);

            int startIndex = inputExpr.Length - dRes.ToString().Length;
            inputExpr.Remove(startIndex, dRes.ToString().Length);
            if (dRes > 0 && inputExpr.Length - 1 > 0)
            {
                inputExpr.Remove(inputExpr.Length - 1, 1);
            }
            if (cosValue > 0)
            {
                inputExpr.Append("+");
            }


            args.ModifiedString = inputExpr.ToString();
            args.CurrentOperand = cosValue;
            onShiftPressed(args);
        }

        public void ClearOccured(StringBuilder inputExpr)
        {
            ClearPressArgs args = new ClearPressArgs(); 
            inputExpr.Clear();
            args.ClearString = inputExpr.ToString();
            onClearPressed(args);

        }

        public void EOccured(StringBuilder inputExpr)
        {
            EPressArgs args = new EPressArgs();
            CurrentOperand cd = new CurrentOperand();
            double dRes = cd.Final_Res_Current(inputExpr.ToString());

            int startIndex = inputExpr.Length - dRes.ToString().Length;
            inputExpr.Remove(startIndex, dRes.ToString().Length);



            args.str = dRes.ToString("e");
            //Console.WriteLine(args.str);
            onEPressed(args);

        }






        //meathod responsible for raising event
        public void onequalPressed(EqualPressArgs e)
        {
            EventHandler<EqualPressArgs> handler = equalPress;
            if (handler != null)
            {
                handler(this, e);
            }

        }

        //responsible for raising the event
        public void onThresholdReached(ThreasholdReachedArgs e)
        {
            EventHandler<ThreasholdReachedArgs> handler = threasholdReached;
            if (handler != null)
            {
                handler(this, e);
            }
        }


        public void onLogPressed(LogPressArgs e)
        {
            EventHandler<LogPressArgs> handler = logPress;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        public void onCeilPressed(CeilPressArgs e)
        {
            EventHandler<CeilPressArgs> handler = ceilPress;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        public void onFloorPressed(FloorPressArgs e)
        {
            EventHandler<FloorPressArgs> handler = floorPress;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        public void onShiftPressed(ShiftPressArgs e)
        {
            EventHandler<ShiftPressArgs> handler = shiftPress;
            if (handler != null)
            {
                handler(this, e);
            }
        }
        public void onCosPressed(CosPressArgs e)
        {
            EventHandler<CosPressArgs> handler = cosPress;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        public void onMemPressed(MemPressArgs e)
        {
            EventHandler<MemPressArgs> handler = memPress;
            if (handler != null)
            {
                handler(this, e);
            }
        }
        public void onMemAddPressed(MemAddPressArgs e)
        {
            EventHandler<MemAddPressArgs> handler = memAddPress;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        public void onMemMinusPressed(MemMinusPressArgs e)
        {
            EventHandler<MemMinusPressArgs> handler = memMinusPress;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        public void onMemReadPressed(MemReadPressArgs e)
        {
            EventHandler<MemReadPressArgs> handler = memReadPress;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        public void onClearPressed(ClearPressArgs e)
        {
            EventHandler<ClearPressArgs> handler =clearPress;
            if (handler!=null)
            {
                handler(this, e);
            }
        }

        public void onEPressed(EPressArgs e)
        {
            EventHandler<EPressArgs> handler = ePress;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        public void onPowPressed(PowPressArgs e)
        {
            EventHandler<PowPressArgs> handler = powPress;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        public void onRadPressed(RadPressArgs e)
        {
            EventHandler<RadPressArgs> handler = radPress;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        public void onAbsPressed(AbsPressArgs e)
        {
            EventHandler<AbsPressArgs> handler = absPress;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        public void onDegTogPressed(DegTogPressArgs e)
        {
            EventHandler<DegTogPressArgs> handler = degTogPress;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        public void onMemSavePressed(MemSavePressArgs e)
        {
            EventHandler<MemSavePressArgs> handler = memSavePress;
            if (handler != null)
            {
                handler(this, e);
            }
        }


        public event EventHandler<ThreasholdReachedArgs> threasholdReached;
        //public event EventHandler<TerminateExprArgs> terminateExpr;
        public event EventHandler<ShiftPressArgs> shiftPress;
        public event EventHandler<ClearPressArgs> clearPress;
        public event EventHandler<MemPressArgs> memPress;
        public event EventHandler<MemReadPressArgs> memReadPress;
        public event EventHandler<MemAddPressArgs> memAddPress;
        public event EventHandler<EqualPressArgs> equalPress;
        public event EventHandler<LogPressArgs> logPress;
        public event EventHandler<EPressArgs> ePress;
        public event EventHandler<CosPressArgs> cosPress;
        public event EventHandler<TanPressArgs> tanPress;
        public event EventHandler<CeilPressArgs> ceilPress;
        public event EventHandler<FloorPressArgs> floorPress;
        public event EventHandler<PowPressArgs> powPress;
        public event EventHandler<GradPressArgs> gradPress;
        public event EventHandler<RadPressArgs> radPress;
        public event EventHandler<AbsPressArgs> absPress;
        public event EventHandler<DegTogPressArgs>degTogPress;
        public event EventHandler<MemMinusPressArgs> memMinusPress;
        public event EventHandler<MemSavePressArgs> memSavePress;




    }

    public class MemSavePressArgs : EventArgs
    {

        public string str { get; set; }
    }

    public class DegTogPressArgs : EventArgs
    {

        public string str { get; set; }
    }

        public class PowPressArgs : EventArgs
    {

        public string str { get; set; }
    }

    public class AbsPressArgs : EventArgs
    {

        public string str { get; set; }
    }
    public class DegPressArgs : EventArgs
    {

        public string str { get; set; }
    }

    public class TanPressArgs : EventArgs
    {

        public string str { get; set; }
    }
    public class RadPressArgs : EventArgs
    {
        public ConsoleKeyInfo ShiftPress { get; set; }
        public double CurrentOperand;
        public string ModifiedString { get; set; }
    }
    public class GradPressArgs : EventArgs
    {

        public string str { get; set; }
    }

    public class FloorPressArgs : EventArgs
    {

        public string str { get; set; }
    }

    public class CeilPressArgs : EventArgs
    {

        public string str { get; set; }
    }

    public class FactPressArgs : EventArgs
    {

        public string str { get; set; }
    }

    public class ModPressArgs : EventArgs
    {

        public string str { get; set; }
    }

    public class FEPressArgs : EventArgs
    {

        public string str { get; set; }
    }
    public class EPressArgs : EventArgs { 
    
        public string str { get; set; }
    }
    public class LogPressArgs: EventArgs
    {
        public string xyz { get; set; }
    }
    public class EqualPressArgs : EventArgs
    {
        public string xyz { get; set; }
        public CalculatorArithematicView obj { get; set; }
    }

    public class MemAddPressArgs : EventArgs
    {
        public string xyz { get; set; }

    }

    public class MemMinusPressArgs : EventArgs
    {
        public string xyz { get; set; }

    }

    public class MemReadPressArgs : EventArgs
    {
        public string xyz { get; set; }

    }

    public class MemPressArgs : EventArgs
    {
        public int xyz { get; set; }

    }
    public class TerminateExprArgs : EventArgs
    {
        public DateTime TimeInfo { get; set; }
        public ConsoleKeyInfo ConsoleKeyInform { get; set; }

    }

    public class ShiftPressArgs : EventArgs
    {
        public ConsoleKeyInfo ShiftPress { get; set; }
        public double CurrentOperand;
        public string ModifiedString { get; set; }
    }

    public class CosPressArgs : EventArgs
    {
        public ConsoleKeyInfo ShiftPress { get; set; }
        public double CurrentOperand;
        public string ModifiedString { get; set; }
    }

    public class ThreasholdReachedArgs : EventArgs
    {
        public int threashold { get; set; }
        public DateTime TimeDone { get; set; }
    }

    public class ClearPressArgs : EventArgs
    {
        public string ClearString { get; set; } 
    }
}
