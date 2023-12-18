using System;
using System.Diagnostics.Metrics;
using System.Drawing;
using System.Text;
using Arithematic;
using Power;
using CalculatorComponent;
using CalculatorComponent.Views;
using CalculatorComponent.Data;
using System.Runtime.InteropServices;

namespace ExecutionPoint
{

    public enum AllowedKeys
    {
        OemPlus =1, OemPeriod=2, OemMinus=4,Backspace=8,Add=16,Subract=32,Multiply=64,
        Divide=128,
        Decimal=256,
        Delete=412,
        D0,D1,D2,D3,D4,D5,D6,D7,D8,D9,
        S,M,L,K,B,O,Y,E,C,G,Q,A,
        UpArrow,F,U,P,H,Tab
    }

    internal class Program
    {
        static StringBuilder inputExpression = new StringBuilder();
        static StringBuilder prevExpression = new StringBuilder();

        static void c_thresholdReached(object sender, ThreasholdReachedArgs e)
        {
            Console.WriteLine("\nExpression limit exceeded !");
            Console.WriteLine("The threshold of {0} was reached at {1}.", e.threashold, e.TimeDone);
            Environment.Exit(0);
        }


        static void c_EqualOccured(object sender, EqualPressArgs e)
        {
            Console.WriteLine("\nEqual Pressed Result = {0}",e.xyz);
            prevExpression.Clear();
            prevExpression.Append(inputExpression);
            inputExpression.Clear();
            inputExpression.Append(e.xyz);
            Console.Clear();
            //CalculatorArithematicView calculatorArithematicView = CalculatorObjectFactory.calculatorArithematicViewObject();
           
            //CalculatorArithematicView._num  -=1;
            if (CalculatorArithematicView._num%2!=0)
            {
                e.obj.CreateArithematicView($"Deg");
            }
            else
            {
                e.obj.CreateArithematicView($"Rad");
            }
            Console.WriteLine($"\t{prevExpression}");
            Console.Write(inputExpression);

        }
        static void c_DegTogOccured(object sender, DegTogPressArgs e)
        {
       
            Console.Clear();

            //#change -no need to create new obj
            CalculatorArithematicView calculatorArithematicView = CalculatorObjectFactory.calculatorArithematicViewObject();
            calculatorArithematicView.CreateArithematicView($"{e.str}");
            Console.WriteLine(inputExpression);


        }

        static void c_RadOccured(object sender, RadPressArgs e)
        {
     
            inputExpression.Clear();
            inputExpression.Append(e.ModifiedString);
            inputExpression.Append(e.CurrentOperand);

        }

        //Delete Key
        static void c_ClearOccured(object sender,ClearPressArgs e)
        {
            //Console.Clear();
            inputExpression.Clear();
            inputExpression.Append(e.ClearString.ToString());

        }
        static void c_LogOccured(object sender, LogPressArgs e)
        {
            //Console.Clear();
            inputExpression.Clear();
            inputExpression.Append(e.xyz);
        }

        static void c_AbsOccured(object sender, AbsPressArgs e)
        {
            //Console.Clear();
            inputExpression.Clear();
            inputExpression.Append(e.str);
        }

        static void c_CeilOccured(object sender, CeilPressArgs e)
        {
            //Console.Clear();
            inputExpression.Clear();
            inputExpression.Append(e.str);
        }
        static void c_FloorOccured(object sender, FloorPressArgs e)
        {
            //Console.Clear();
            inputExpression.Clear();
            inputExpression.Append(e.str);
        }
        static void c_PowOccured(object sender, PowPressArgs e)
        {
            //Console.Clear();
            //inputExpression.Append(e.str);
            inputExpression.Clear();
            inputExpression.Append(e.str);
        }
        static void c_EOccured(object sender, EPressArgs e)
        {
            //Console.Clear();
            inputExpression.Append(e.str);
        }
        static void c_MemViewOccured(object sender, MemPressArgs e)
        {
            //Console.WriteLine("Memory View");

        }
        static void c_MemAddOccured(object sender, MemAddPressArgs e)
        {
            //Console.WriteLine("Memory View");

        }
        static void c_MemMinusOccured(object sender, MemAddPressArgs e)
        {
            //Console.WriteLine("Memory View");

        }
        static void c_MemReadOccured(object sender, MemReadPressArgs e)
        {
            inputExpression.Clear();
            inputExpression.Append(e.xyz);
            //Console.WriteLine("Memory View");

        }
        static void c_ShiftPressed(object sender,ShiftPressArgs e)
        {
 

            inputExpression.Clear();
            inputExpression.Append(e.ModifiedString);
            //Console.WriteLine(inputExpression);
            //inputExpression.Append("+");
            inputExpression.Append(e.CurrentOperand);
            //Environment.Exit(0);
        }

        static void c_CosPressed(object sender, CosPressArgs e)
        {


            inputExpression.Clear();
            inputExpression.Append(e.ModifiedString);
            //Console.WriteLine(inputExpression);
            //inputExpression.Append("+");
            inputExpression.Append(e.CurrentOperand);
            //Environment.Exit(0);
        }
        static void Main(string[] args)
        {

           
            Input c1 = new Input(67);
            c1.threasholdReached += c_thresholdReached;
            //c1.terminateExpr += c_EqualOccured;
            c1.shiftPress += c_ShiftPressed;
            c1.clearPress += c_ClearOccured;
            c1.memPress += c_MemViewOccured;
            c1.memReadPress += c_MemReadOccured;
            c1.memAddPress += c_MemAddOccured;
            c1.equalPress += c_EqualOccured;
            c1.ePress += c_EOccured;
            c1.cosPress += c_CosPressed;
            c1.powPress += c_PowOccured;
            c1.radPress += c_RadOccured;
            c1.ceilPress += c_CeilOccured;
            c1.floorPress += c_FloorOccured;
            c1.absPress += c_AbsOccured;
            c1.degTogPress += c_DegTogOccured;
           
         
            Console.WriteLine(CalculatorCommonOutput.GetInstructions());


            bool blnFlag=false;
            while (!blnFlag)
            {
                ConsoleKey Ck = Console.ReadKey(true).Key;

             

                switch (Ck)
                {
                    case ConsoleKey.A:
                        CalculatorArithematicView calculatorArithematicView = CalculatorObjectFactory.calculatorArithematicViewObject();
                        calculatorArithematicView.CreateArithematicView("Deg");
                        GetRes(c1,calculatorArithematicView);
                        break;
                    case ConsoleKey.P:
                        CalculatorScientificView calculatorScientificView = CalculatorObjectFactory.calculatorScientificViewObject();
                        calculatorScientificView.CreateScientificView();
                        GetRes(c1,calculatorScientificView);
                        break;
                    case ConsoleKey.Escape:
                        blnFlag = true;
                        break;
                    default:
                        Console.WriteLine("Incorrect key !");
                        break;
                }
            }
        }

    
        static void GetRes(Input c1,Object view)
        {
            MemoryOperations mO=new MemoryOperations();
            HelpView help = CalculatorObjectFactory.helpViewObject();
            //mO.MPlus(2);

            ConsoleKeyInfo charKey;
         
            while (true)
            {
                charKey = Console.ReadKey(intercept: true);

                if (Enum.TryParse<AllowedKeys>(charKey.Key.ToString(), out AllowedKeys result))
                {
                    if (charKey.Key != ConsoleKey.OemPlus)
                    {

                        if (charKey.Key == ConsoleKey.Backspace && inputExpression.Length > 0)
                        {

                            inputExpression.Remove(inputExpression.Length - 1, 1);
                            Console.Write("\b \b");

                        }
                        else
                        {
                            inputExpression.Append(charKey.KeyChar);
                            Console.Write(charKey.KeyChar);

                            //for sin
                            if (charKey.Key == ConsoleKey.S && charKey.Modifiers == ConsoleModifiers.Shift)
                            {

                                if (inputExpression.Length>=1)
                                {
                                    inputExpression.Remove(inputExpression.Length - 1, 1);
                                    Console.Write("\b \b");
                                }
                                if (CalculatorArithematicView._num%2==0)
                                {
                                    c1.RadPressed(inputExpression);
                                }
                                else
                                {
                                    c1.ShiftPressed(charKey, inputExpression);
                                }
                               

                               
                            }

                            if (charKey.Key == ConsoleKey.C && charKey.Modifiers == ConsoleModifiers.Shift)
                            {

                                if (inputExpression.Length >= 1)
                                {
                                    inputExpression.Remove(inputExpression.Length - 1, 1);
                                    Console.Write("\b \b");
                                }

                                c1.CosPressed(charKey, inputExpression);
                            }

                            if (charKey.Key == ConsoleKey.Delete)
                            {

                                if (inputExpression.Length >= 1)
                                {
                                    inputExpression.Remove(inputExpression.Length - 1, 1);
                                    Console.Write("\b \b");
                                }

                                c1.ClearOccured(inputExpression);
                                c1.FetchRes(inputExpression, (CalculatorArithematicView)view);
                            }

                            if (charKey.Key == ConsoleKey.L && charKey.Modifiers == ConsoleModifiers.Control)
                            {
                                if (inputExpression.Length >= 1)
                                {
                                    inputExpression.Remove(inputExpression.Length - 1, 1);
                                    Console.Write("\b \b");
                                }

                                c1.MemViewPressed(mO.cstack);
                            }

                            if (charKey.Key == ConsoleKey.K && charKey.Modifiers == ConsoleModifiers.Control)
                            {
                                //Console.WriteLine("presses");
                                if (inputExpression.Length >= 1)
                                {
                                    inputExpression.Remove(inputExpression.Length - 1, 1);
                                    Console.Write("\b \b");
                                }

                                c1.MemReadPressed(inputExpression,mO.cstack);
                            }


                            if (charKey.Key == ConsoleKey.U && charKey.Modifiers == ConsoleModifiers.Control)
                            {
                                //Console.WriteLine("presses");
                                if (inputExpression.Length >= 1)
                                {
                                    inputExpression.Remove(inputExpression.Length - 1, 1);
                                    Console.Write("\b \b");
                                }

                                c1.MemMinusPressed(inputExpression, mO);
                            }

                            if (charKey.Key == ConsoleKey.P && charKey.Modifiers == ConsoleModifiers.Control)
                            {
                                //Console.WriteLine("presses");
                                if (inputExpression.Length >= 1)
                                {
                                    inputExpression.Remove(inputExpression.Length - 1, 1);
                                    Console.Write("\b \b");
                                }

                                c1.MemSavePressed(inputExpression, mO);
                            }

                            if (charKey.Key == ConsoleKey.U && charKey.Modifiers == ConsoleModifiers.Control)
                            {
                                //Console.WriteLine("presses");
                                if (inputExpression.Length >= 1)
                                {
                                    inputExpression.Remove(inputExpression.Length - 1, 1);
                                    Console.Write("\b \b");
                                }

                                c1.MemMinusPressed(inputExpression, mO);
                            }


                            if (charKey.Key == ConsoleKey.B && charKey.Modifiers == ConsoleModifiers.Control)
                            {
                                //Console.WriteLine("presses");
                                if (inputExpression.Length >= 1)
                                {
                                    inputExpression.Remove(inputExpression.Length - 1, 1);
                                    Console.Write("\b \b");
                                }

                                c1.MemAddPressed(mO,inputExpression);
                            }

                            if (charKey.Key == ConsoleKey.Y && charKey.Modifiers == ConsoleModifiers.Shift)
                            {
                                //Console.WriteLine("presses");
                                if (inputExpression.Length >= 1)
                                {
                                    inputExpression.Remove(inputExpression.Length - 1, 1);
                                    Console.Write("\b \b");
                                }

                                c1.LogPressed(inputExpression);
                            }

                            if (charKey.Key == ConsoleKey.E && charKey.Modifiers == ConsoleModifiers.Shift)
                            {
                                //Console.WriteLine("presses");
                                if (inputExpression.Length >= 1)
                                {
                                    inputExpression.Remove(inputExpression.Length - 1, 1);
                                    Console.Write("\b \b");
                                }

                                c1.EOccured(inputExpression);
                            }

                            if (charKey.Key == ConsoleKey.G && charKey.Modifiers == ConsoleModifiers.Shift)
                            {
                                //Console.WriteLine("presses");
                                if (inputExpression.Length >= 1)
                                {
                                    inputExpression.Remove(inputExpression.Length - 1, 1);
                                    Console.Write("\b \b");
                                }

                                c1.PowPressed(inputExpression);
                            }

                            //if (charKey.Key == ConsoleKey.Q && charKey.Modifiers == ConsoleModifiers.Shift)
                            //{
                            //    //Console.WriteLine("presses");
                            //    if (inputExpression.Length >= 1)
                            //    {
                            //        inputExpression.Remove(inputExpression.Length - 1, 1);
                            //        Console.Write("\b \b");
                            //    }

                            //    c1.RadPressed(inputExpression);
                            //}

                            if (charKey.Key == ConsoleKey.F && charKey.Modifiers == ConsoleModifiers.Shift)
                            {
                                //Console.WriteLine("presses");
                                if (inputExpression.Length >= 1)
                                {
                                    inputExpression.Remove(inputExpression.Length - 1, 1);
                                    Console.Write("\b \b");
                                }

                                c1.FloorPressed(inputExpression);
                            }
                            if (charKey.Key == ConsoleKey.Q && charKey.Modifiers == ConsoleModifiers.Control)
                            {
                                //Console.WriteLine("presses");
                                if (inputExpression.Length >= 1)
                                {
                                    inputExpression.Remove(inputExpression.Length - 1, 1);
                                    Console.Write("\b \b");
                                }

                                c1.CeilPressed(inputExpression);
                            }

                            if (charKey.Key == ConsoleKey.A && charKey.Modifiers == ConsoleModifiers.Shift)
                            {
                                //Console.WriteLine("presses");
                                if (inputExpression.Length >= 1)
                                {
                                    inputExpression.Remove(inputExpression.Length - 1, 1);
                                    Console.Write("\b \b");
                                }

                                c1.AbsPressed(inputExpression);
                            }

                            if (charKey.Key == ConsoleKey.UpArrow )
                            {
                                //Console.WriteLine("presses");
                                if (inputExpression.Length >= 1)
                                {
                                    inputExpression.Remove(inputExpression.Length - 1, 1);
                                    Console.Write("\b \b");
                                }

                                c1.DegTogPressed(inputExpression); 
                            }

                            if (charKey.Key == ConsoleKey.Tab)
                            {
                                if (inputExpression.Length >= 1)
                                {
                                    inputExpression.Remove(inputExpression.Length - 1, 1);
                                    Console.Write("\b \b");
                                }
                                //Console.WriteLine("presses");
                                CalculatorArithematicView.blnHint = false;
                                c1.FetchRes(inputExpression, (CalculatorArithematicView)view);
                            }

                            if (charKey.Key == ConsoleKey.H)
                            {
                                if (inputExpression.Length >= 1)
                                {
                                    inputExpression.Remove(inputExpression.Length - 1, 1);
                                    Console.Write("\b \b");
                                }
                                help.CreateHelpView();
                                
                            }
                            //to be done using events--------------
                        }

                        c1.Add(1);
                    }

                    else if (charKey.Key == ConsoleKey.OemPlus)
                    {
                        Console.WriteLine("\n"+inputExpression);
                        c1.FetchRes(inputExpression,(CalculatorArithematicView)view);

                    }
                }

            }

        }
    }
}