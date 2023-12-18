using System;
using System.Text;
using System.IO;
using System.Collections;
//using System.Linq.Expressions;
using System.Net.Http.Headers;
using ScientificHelper;
using System.Diagnostics;
using System.Data;
using System.Reflection.Metadata;
using System.Text.RegularExpressions;
using NCalc;
class Program
{
    static double solveExpression(String expression)
    {
        //[{(2+4)*2+5}+2^5+{(6*6)/6+32}]
        // Stack Approach :
        /*
        Stack<char> operatorStack = new Stack<char>();
        Stack<double> operandStack = new Stack<double>();

        for (int i = 0; i < expression.Length; i++)
        {
            if (expression[i] == '(' || expression[i] == '[' || expression[i] == '{')
            {
                operatorStack.Push(expression[i]);
            }
            else if ((expression[i] >= '0' && expression[i] <= '9') || expression[i] == '.')
            {
                StringBuilder valueBuilder = new StringBuilder();
                while (i < expression.Length && (char.IsDigit(expression[i]) || expression[i] == '.'))
                {
                    valueBuilder.Append(expression[i]);
                    i++;
                }

                if (double.TryParse(valueBuilder.ToString(), out double value))
                {
                    operandStack.Push(value);
                }
                else
                {
                    // Handle parsing error
                    throw new InvalidOperationException("Invalid numeric value");
                }

                i--;
            }
            else if (expression[i] == ')' || expression[i] == ']' || expression[i] == '}')
            {
                if (expression[i] == ')')
                {
                    while (operatorStack.Count > 0 && operatorStack.Peek() != '(')
                    {
                        double b = operandStack.Pop();
                        double a = operandStack.Pop();
                        char op = operatorStack.Pop();
                        operandStack.Push(Arithmetic.SolveTwoVariable(a, b, op));
                    }
                }
                else if (expression[i] == '}')
                {
                    while (operatorStack.Count > 0 && operatorStack.Peek() != '{')
                    {
                        double b = operandStack.Pop();
                        double a = operandStack.Pop();
                        char op = operatorStack.Pop();
                        operandStack.Push(Arithmetic.SolveTwoVariable(a, b, op));
                    }
                }
                else if (expression[i] == ']')
                {
                    while (operatorStack.Count > 0 && operatorStack.Peek() != '[')
                    {
                        double b = operandStack.Pop();
                        double a = operandStack.Pop();
                        char op = operatorStack.Pop();
                        operandStack.Push(Arithmetic.SolveTwoVariable(a, b, op));
                    }
                }
                if (operatorStack.Count > 0)
                {
                    operatorStack.Pop();
                }
            }
            else
            {
                while (operatorStack.Count > 0 && Precedence(expression[i]) <= Precedence(operatorStack.Peek()))
                {
                    double b = operandStack.Pop();
                    double a = operandStack.Pop();
                    char op = operatorStack.Pop();
                    operandStack.Push(Arithmetic.SolveTwoVariable(a, b, op));
                }
                operatorStack.Push(expression[i]);
            }
        }
        while (operatorStack.Count > 0)

        {
            double b = operandStack.Pop();
            double a = operandStack.Pop();
            char op = operatorStack.Pop();
            operandStack.Push(Arithmetic.SolveTwoVariable(a, b, op));
        }
        Console.WriteLine(operandStack.Peek());
        return operandStack.Peek();
        */
        //expression = expression.Replace("^", "**");

        expression = expression.Replace("^", "**");

        try
        {
            Expression e = new Expression(expression);
            object result = e.Evaluate();
            return Convert.ToDouble(result);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error evaluating expression: {ex.Message}");
            // while (true) { }
            return double.NaN; // Handle error as needed
        }
        // Convert the result to double
        //return Convert.ToDouble(result);
    }

    static string BalanceParentheses(string expression)
    {
        int openCount = expression.Count(c => c == '(');
        int closeCount = expression.Count(c => c == ')');

        for (int i = 0; i < openCount - closeCount; i++)
        {
            expression += ")";
        }

        return expression;
    }
    // Using Inheritance
    public class memoryExpressions : ArrayList
    {
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            foreach (Object i in this)
            {
                sb.Append(i + " -> ");
            }
            return sb.ToString();
        }
    }


    public delegate void KeyPressedEventHandler(ConsoleKeyInfo keyInfo);
    public static event KeyPressedEventHandler KeyPressed;

    static StringBuilder expressionHistory = new StringBuilder();
    static StringBuilder expressionSolver = new StringBuilder();
    static ArrayList memoryExpression = new memoryExpressions();
    static StringBuilder History = new StringBuilder();
    static String Current = "Radian";

    static public void Layout()
    {
        Console.WriteLine("Number Keys:");
        Console.WriteLine("  7                                    4                                  1                                    0");
        Console.WriteLine("  8                                    5                                  2                                    .");
        Console.WriteLine("  9                                    6                                  3                                    =");
        Console.WriteLine("  /                                    *                                  -                                    +");


        Console.WriteLine("\nAdditional Keys:                     Additional Shift + Keys:                   Other Special Keys:");
        Console.WriteLine("=================                      =========================                  ====================");
        Console.WriteLine("Ctrl + M: Secant                       Shift + S: 10^x                            Backspace: Remove Last");
        Console.WriteLine("Ctrl + P: Cosecant                     Shift + T: Log10                           Escape: Close");
        Console.WriteLine("Ctrl + Q: Cotangent                    Shift + T: Log10                           S: Abs");
        Console.WriteLine("Ctrl + R: Round Up                     Shift + O: Log2                            T: Exp");
        Console.WriteLine("Ctrl + L: Round Down                   Shift + U: Sin                             O: Modulus (%)");
        Console.WriteLine("Ctrl + G: Random (1-100)               Shift + I: Cos                             U: Factorial");
        Console.WriteLine("Ctrl + S: Rad to Deg                   Shift + J: Tan                             I: (");
        Console.WriteLine("Ctrl + U: Deg to Rad                   Shift + A: Tanh                            J: )");
        Console.WriteLine("Ctrl + I: Use as is (GRAD)             Shift + B: Sinh                            M: 1/x");
        Console.WriteLine("Ctrl + J: Scientific Notation          Shift + C: Cosh                            N: Clear All");
        Console.WriteLine("Ctrl + Y: Negation                     Shift + D: Sech                            Q: x^2");
        Console.WriteLine("Ctrl + Z: F-E                          Shift + E: Cosech                          V: √x");
        Console.WriteLine("Ctrl + A: e                            Shift + F: Coth                            X: x^y");
        Console.WriteLine("Ctrl + B: .                            Shift + H: Toggle                          Up Arror: M+");
        Console.WriteLine("                                                                                  Left Arror: M-");
        Console.WriteLine("                                                                                  Right Arrow: MS");
        Console.WriteLine("                                                                                  Down Arrow: MR");
        Console.WriteLine("                                                                                  G: MC");


        Console.WriteLine();
    }

    static bool isPower = false;
    static void Main()
    {

        Console.Title = " Calculater ";
        //Console.WriteLine("Expression");
        String Expression = "(((2+4)*2+5)+2^5+((6*6)/6+32))";
        // Console.WriteLine(solveExpression(Expression));
        ConsoleKeyInfo cki;
        Console.TreatControlCAsInput = true;
        // Console.Write(" Scientific Calculater ");

        KeyPressed += HandleKeys;

        Console.WriteLine();
        Layout();
        while (true)
        {
            // Start a console read operation. Do not display the input.
            cki = Console.ReadKey(true);
            if (cki.Key == ConsoleKey.Escape) break;
            else if (((cki.Modifiers & ConsoleModifiers.Shift) != 0) || ((cki.Modifiers & ConsoleModifiers.Control) != 0) || (((cki.Modifiers & ConsoleModifiers.Shift) != 0) && ((cki.Modifiers & ConsoleModifiers.Control) != 0))) ;
            // Announce the name of the key that was pressed .
            KeyPressed?.Invoke(cki);
            //Console.WriteLine($"  Key pressed: {cki.Key}\t Modifiers pressed : {cki.Modifiers}.");

        }
    }


    static void HandleKeys(ConsoleKeyInfo keyInfo)
    {

        string specialCharecters = keyInfo.Modifiers.ToString();

        if (specialCharecters == "Control")
        {
            switch (keyInfo.Key)
            {
                case ConsoleKey.M:
                    // sec
                    if (double.TryParse(expressionSolver.ToString(), out double intVaals))
                    {
                        if (Current == "Degree")
                        {
                            double er = intVaals * (Math.PI / 180);
                            expressionSolver = new StringBuilder((1 / Math.Cos(intVaals)).ToString());
                        }
                        else if (Current == "Radian")
                        {
                            //double er = Math.PI * (intVl / 180.0);
                            expressionSolver = new StringBuilder((1 / Math.Cos(intVaals)).ToString());
                        }
                        else if (Current == "Gradient")
                        {
                            double gradientValue = intVaals * (10.0 / 9.0);
                            double radianValue = gradientValue * (Math.PI / 180);

                            // Calculate Sin in gradient
                            // double resultInGradient = Math.Sin(radianValue) * (10.0 / 9.0);
                            expressionSolver = new StringBuilder((1 / Math.Cos(intVaals) * (10.0 / 9.0)).ToString());
                        }

                        //expressionSolver = new StringBuilder((1 / (Math.Cos(intVaals))).ToString());
                        History.Append("Sec(" + intVaals + ")");
                        Console.Clear();
                        Console.WriteLine(expressionHistory);

                        Console.WriteLine(expressionSolver);
                        Console.WriteLine(History + "                                      -> History ");
                        Console.WriteLine(memoryExpression.ToString() + "                             -> Memory ");
                        Layout();
                    }
                    break;
                case ConsoleKey.P:
                    // csc
                    if (double.TryParse(expressionSolver.ToString(), out double intVaalss))
                    {
                        if (Current == "Degree")
                        {
                            double er = intVaalss * (Math.PI / 180);
                            expressionSolver = new StringBuilder((1 / Math.Sin(intVaalss)).ToString());
                        }
                        else if (Current == "Radian")
                        {
                            //double er = Math.PI * (intVl / 180.0);
                            expressionSolver = new StringBuilder((1 / Math.Sin(intVaalss)).ToString());
                        }
                        else if (Current == "Gradient")
                        {
                            double gradientValue = intVaalss * (10.0 / 9.0);
                            double radianValue = gradientValue * (Math.PI / 180);

                            // Calculate Sin in gradient
                            // double resultInGradient = Math.Sin(radianValue) * (10.0 / 9.0);
                            expressionSolver = new StringBuilder((1 / Math.Sin(intVaalss) * (10.0 / 9.0)).ToString());
                        }

                        // expressionSolver = new StringBuilder((1 / (Math.Sin(intVaalss))).ToString());
                        History.Append("Cosec(" + intVaalss + ")");
                        Console.Clear();
                        Console.WriteLine(expressionHistory );

                        Console.WriteLine(expressionSolver );
                        Console.WriteLine(History + "                                      -> History ");
                        Console.WriteLine(memoryExpression.ToString() + "                             -> Memory ");
                        Layout();
                    }
                    break;
                case ConsoleKey.Q:
                    //cot
                    if (double.TryParse(expressionSolver.ToString(), out double intVaalsss))
                    {
                        if (Current == "Degree")
                        {
                            double er = intVaalsss * (Math.PI / 180);
                            expressionSolver = new StringBuilder((1 / Math.Tan(intVaalsss)).ToString());
                        }
                        else if (Current == "Radian")
                        {
                            //double er = Math.PI * (intVl / 180.0);
                            expressionSolver = new StringBuilder((1 / Math.Tan(intVaalsss)).ToString());
                        }
                        else if (Current == "Gradient")
                        {
                            double gradientValue = intVaalsss * (10.0 / 9.0);
                            double radianValue = gradientValue * (Math.PI / 180);

                            // Calculate Sin in gradient
                            // double resultInGradient = Math.Sin(radianValue) * (10.0 / 9.0);
                            expressionSolver = new StringBuilder((1 / Math.Tan(intVaalsss) * (10.0 / 9.0)).ToString());
                        }

                        //expressionSolver = new StringBuilder((1 / (Math.Tan(intVaalsss))).ToString());
                        History.Append("Cot(" + intVaalsss + ")");
                        Console.Clear();
                        Console.WriteLine(expressionHistory );

                        Console.WriteLine(expressionSolver );
                        Console.WriteLine(History + "                                      -> History ");
                        Console.WriteLine(memoryExpression.ToString() + "                             -> Memory ");
                        Layout();
                    }
                    break;
                case ConsoleKey.R:
                    //ceil
                    if (double.TryParse(expressionSolver.ToString(), out double intVaalssss))
                    {
                        expressionSolver = new StringBuilder((Math.Ceiling(intVaalssss)).ToString());
                        History.Append("Ceil(" + intVaalssss + ")");
                        Console.Clear();
                        Console.WriteLine(expressionHistory );

                        Console.WriteLine(expressionSolver );
                        Console.WriteLine(History + "                                      -> History ");
                        Console.WriteLine(memoryExpression.ToString() + "                             -> Memory ");
                        Layout();
                    }
                    break;
                case ConsoleKey.L:
                    //floor
                    if (double.TryParse(expressionSolver.ToString(), out double intVaalsssss))
                    {
                        expressionSolver = new StringBuilder((Math.Floor(intVaalsssss)).ToString());
                        History.Append("Floor(" + intVaalsssss + ")");
                        Console.Clear();
                        Console.WriteLine(expressionHistory );

                        Console.WriteLine(expressionSolver );
                        Console.WriteLine(History + "                                      -> History ");
                        Console.WriteLine(memoryExpression.ToString() + "                             -> Memory ");
                        Layout();
                    }
                    break;
                case ConsoleKey.G:
                    //rand
                    expressionSolver.Append(new Random().Next(1, 100));
                    History.Append("Random()");
                    Console.Clear();
                    Console.WriteLine(expressionHistory);

                    Console.WriteLine(expressionSolver);
                    Console.WriteLine(History + "                                      -> History ");
                    Console.WriteLine(memoryExpression.ToString() + "                             -> Memory ");
                    Layout();
                    break;
                case ConsoleKey.S:
                    //->To DEG
                    if (double.TryParse(expressionSolver.ToString(), out double inttVaalssss))
                    {
                        expressionSolver = new StringBuilder((Math.PI * inttVaalssss / 180.0).ToString());
                        History.Append("DEG(" + inttVaalssss + ")");
                        Console.Clear();
                        Console.WriteLine(expressionHistory);

                        Console.WriteLine(expressionSolver);
                        Console.WriteLine(History + "                                      -> History ");
                        Console.WriteLine(memoryExpression.ToString() + "                             -> Memory ");
                        Layout();
                    }
                    break;

                case ConsoleKey.U:
                    // To RAD
                    if (double.TryParse(expressionSolver.ToString(), out double intttVaalssss))
                    {
                        expressionSolver = new StringBuilder((intttVaalssss * 180.0 / Math.PI).ToString());
                        History.Append("RAD(" + intttVaalssss + ")");
                        Console.Clear();
                        Console.WriteLine(expressionHistory);

                        Console.WriteLine(expressionSolver);
                        Console.WriteLine(History + "                                      -> History ");
                        Console.WriteLine(memoryExpression.ToString() + "                             -> Memory ");
                        Layout();
                    }
                    break;
                case ConsoleKey.I:
                    // To  GRAD
                    if (double.TryParse(expressionSolver.ToString(), out double inttttVaalssss))
                    {
                        expressionSolver = new StringBuilder((inttttVaalssss = inttttVaalssss * 200.0 / Math.PI).ToString());
                        History.Append("GRAD(" + inttttVaalssss + ")");
                        Console.Clear();
                        Console.WriteLine(expressionHistory);

                        Console.WriteLine(expressionSolver);
                        Console.WriteLine(History + "                                      -> History ");
                        Console.WriteLine(memoryExpression.ToString() + "                             -> Memory ");
                        Layout();
                    }
                    break;
                case ConsoleKey.J:
                    // e key
                    if (double.TryParse(expressionSolver.ToString(), out double intttttVaalssss))
                    {

                        string formattedNumber = intttttVaalssss.ToString($"F{2}");

                        // Convert the formatted number to scientific notation
                        string scientificNotation = $"{double.Parse(formattedNumber):0.##e+0}";
                        expressionSolver = new StringBuilder((formattedNumber).ToString());
                        History.Append("e(" + formattedNumber + ")");
                        Console.Clear();
                        Console.WriteLine(expressionHistory);

                        Console.WriteLine(expressionSolver);
                        Console.WriteLine(History + "                                      -> History ");
                        Console.WriteLine(memoryExpression.ToString() + "                             -> Memory ");
                        Layout();
                    }
                    break;



                case ConsoleKey.Y:
                    // Negation
                    if (double.TryParse(expressionSolver.ToString(), out double suma))
                    {
                        expressionSolver = new StringBuilder(("-" + suma).ToString());
                        History.Append("NEG(" + suma + ")");
                        Console.Clear();
                        Console.WriteLine(expressionHistory);

                        Console.WriteLine(expressionSolver );
                        Console.WriteLine(History + "                                      -> History ");
                        Console.WriteLine(memoryExpression.ToString() + "                             -> Memory ");
                        Layout();
                    }
                    break;
                    //case ConsoleKey.N:
                    //    // ... add more Ctrl + Key combinations as needed
                    //    Console.WriteLine($"Ctrl + {keyInfo.Key} pressed");
                    break;
                case ConsoleKey.Z:
                    // F-E
                    if (double.TryParse(expressionSolver.ToString(), out double sumaz))
                    {
                        expressionSolver = new StringBuilder(sumaz.ToString("0.###e+0"));
                        History.Append("F-E(" + sumaz.ToString("0.###e+0") + ")");
                        Console.Clear();
                        Console.WriteLine(expressionHistory);

                        Console.WriteLine(expressionSolver);
                        Console.WriteLine(History + "                                      -> History ");
                        Console.WriteLine(memoryExpression.ToString() + "                             -> Memory ");
                        Layout();
                    }
                    break;
                case ConsoleKey.A:
                    // e
                    expressionSolver = new StringBuilder(((int)Math.E).ToString(""));
                    History.Append("e(" + (Math.E).ToString() + ")");
                    Console.Clear();
                    Console.WriteLine(expressionHistory);
                    Console.WriteLine(expressionSolver );
                    Console.WriteLine(History + "                                      -> History ");
                    Console.WriteLine(memoryExpression.ToString() + "                             -> Memory ");
                    Layout();

                    break;
                case ConsoleKey.B:
                    // .
                    expressionSolver.Append(".");
                    History.Append(".");
                    Console.Clear();
                    Console.WriteLine(expressionHistory);
                    Console.WriteLine(expressionSolver);
                    Console.WriteLine(History + "                                      -> History ");
                    Console.WriteLine(memoryExpression.ToString() + "                             -> Memory ");
                    Layout();

                    break;
            }
        }
        else if (specialCharecters == "Shift")
        {
            switch (keyInfo.Key)
            {
                case ConsoleKey.S:
                    // 10x
                    if (double.TryParse(expressionSolver.ToString(), out double intValue))
                    {
                        expressionSolver = new StringBuilder((Math.Pow(10, intValue)).ToString());
                        History.Append("10^(" + intValue + ")");
                        Console.Clear();
                        Console.WriteLine(expressionHistory);

                        Console.WriteLine(expressionSolver);
                        Console.WriteLine(History + "                                      -> History ");
                        Console.WriteLine(memoryExpression.ToString() + "                             -> Memory ");
                        Layout();
                    }
                    break;
                case ConsoleKey.T:
                    // log
                    if (double.TryParse(expressionSolver.ToString(), out double intVale))
                    {
                        expressionSolver = new StringBuilder((Math.Log10(intVale)).ToString());
                        History.Append("log(" + intVale + ")");
                        Console.Clear();
                        Console.WriteLine(expressionHistory);

                        Console.WriteLine(expressionSolver);
                        Console.WriteLine(History + "                                      -> History ");
                        Console.WriteLine(memoryExpression.ToString() + "                             -> Memory ");
                        Layout();
                    }
                    break;
                case ConsoleKey.O:
                    //ln
                    if (double.TryParse(expressionSolver.ToString(), out double intVal))
                    {
                        expressionSolver = new StringBuilder((Math.ILogB(intVal)).ToString());
                        History.Append("ln(" + intVal + ")");
                        Console.Clear();
                        Console.WriteLine(expressionHistory);

                        Console.WriteLine(expressionSolver );
                        Console.WriteLine(History + "                                      -> History ");
                        Console.WriteLine(memoryExpression.ToString() + "                             -> Memory ");
                        Layout();
                    }
                    break;
                case ConsoleKey.U:
                    // sin
                    if (double.TryParse(expressionSolver.ToString(), out double intVl))
                    {
                        if (Current == "Degree")
                        {
                            double er = intVl * (Math.PI / 180);
                            expressionSolver = new StringBuilder((Math.Sin(er)).ToString());
                        }
                        else if (Current == "Radian")
                        {
                            //double er = Math.PI * (intVl / 180.0);
                            expressionSolver = new StringBuilder((Math.Sin(intVl)).ToString());
                        }
                        else if (Current == "Gradient")
                        {
                            double gradientValue = intVl * (10.0 / 9.0);
                            double radianValue = gradientValue * (Math.PI / 180);

                            // Calculate Sin in gradient
                            // double resultInGradient = Math.Sin(radianValue) * (10.0 / 9.0);
                            expressionSolver = new StringBuilder((Math.Sin(radianValue) * (10.0 / 9.0)).ToString());
                        }

                        //expressionSolver = new StringBuilder((Math.Sin(intVl)).ToString());
                        History.Append("Sin(" + intVl + ")");
                        Console.Clear();
                        Console.WriteLine(expressionHistory);

                        Console.WriteLine(expressionSolver );
                        Console.WriteLine(History + "                                      -> History ");
                        Console.WriteLine(memoryExpression.ToString() + "                             -> Memory ");
                        Layout();
                    }
                    break;
                case ConsoleKey.I:
                    // cos
                    if (double.TryParse(expressionSolver.ToString(), out double intVaal))
                    {
                        if (Current == "Degree")
                        {
                            int er = (int)(intVaal * (Math.PI / 180));
                            expressionSolver = new StringBuilder(((Math.Cos(er))).ToString());
                        }
                        else if (Current == "Radian")
                        {
                            //double er = Math.PI * (intVl / 180.0);
                            expressionSolver = new StringBuilder((Math.Cos(intVaal)).ToString());
                        }
                        else if (Current == "Gradient")
                        {
                            double gradientValue = intVaal * (10.0 / 9.0);
                            double radianValue = gradientValue * (Math.PI / 180);

                            // Calculate Sin in gradient
                            // double resultInGradient = Math.Sin(radianValue) * (10.0 / 9.0);
                            expressionSolver = new StringBuilder((Math.Cos(radianValue) * (10.0 / 9.0)).ToString());
                        }
                        // expressionSolver = new StringBuilder((Math.Cos(intVaal)).ToString());
                        History.Append("Cos(" + intVaal + ")");
                        Console.Clear();
                        Console.WriteLine(expressionHistory);

                        Console.WriteLine(expressionSolver);
                        Console.WriteLine(expressionSolver);
                        Console.WriteLine(History + "                                      -> History ");
                        Console.WriteLine(memoryExpression.ToString() + "                             -> Memory ");
                        Layout();
                    }
                    break;
                case ConsoleKey.J:
                    //tan
                    if (double.TryParse(expressionSolver.ToString(), out double intVaals))
                    {
                        if (Current == "Degree")
                        {
                            double er = intVaals * (Math.PI / 180);
                            expressionSolver = new StringBuilder((Math.Tan(er)).ToString());
                        }
                        else if (Current == "Radian")
                        {
                            //double er = Math.PI * (intVl / 180.0);
                            expressionSolver = new StringBuilder((Math.Tan(intVaals)).ToString());
                        }
                        else if (Current == "Gradient")
                        {
                            double gradientValue = intVaals * (10.0 / 9.0);
                            double radianValue = gradientValue * (Math.PI / 180);

                            // Calculate Sin in gradient
                            // double resultInGradient = Math.Sin(radianValue) * (10.0 / 9.0);
                            expressionSolver = new StringBuilder((Math.Tan(radianValue) * (10.0 / 9.0)).ToString());
                        }
                        //expressionSolver = new StringBuilder((Math.Tan(intVaals)).ToString());
                        History.Append("Tan(" + intVaals + ")");
                        Console.Clear();
                        Console.WriteLine(expressionHistory);

                        Console.WriteLine(expressionSolver );
                        Console.WriteLine(History + "                                      -> History ");
                        Console.WriteLine(memoryExpression.ToString() + "                             -> Memory ");
                        Layout();
                    }
                    break;
                case ConsoleKey.A:
                    //tanh
                    if (double.TryParse(expressionSolver.ToString(), out double intVaalsa))
                    {
                        if (Current == "Degree")
                        {
                            double er = intVaalsa * (Math.PI / 180);
                            expressionSolver = new StringBuilder((Math.Tanh(er)).ToString());
                        }
                        else if (Current == "Radian")
                        {
                            //double er = Math.PI * (intVl / 180.0);
                            expressionSolver = new StringBuilder((Math.Tanh(intVaalsa)).ToString());
                        }
                        else if (Current == "Gradient")
                        {
                            double gradientValue = intVaalsa * (10.0 / 9.0);
                            double radianValue = gradientValue * (Math.PI / 180);

                            // Calculate Sin in gradient
                            // double resultInGradient = Math.Sin(radianValue) * (10.0 / 9.0);
                            expressionSolver = new StringBuilder((Math.Tanh(radianValue) * (10.0 / 9.0)).ToString());
                        }
                        //expressionSolver = new StringBuilder((Math.Tanh(intVaalsa)).ToString());
                        History.Append("Tanh(" + intVaalsa + ")");
                        Console.Clear();
                        Console.WriteLine(expressionHistory );

                        Console.WriteLine(expressionSolver);
                        Console.WriteLine(History + "                                      -> History ");
                        Console.WriteLine(memoryExpression.ToString() + "                             -> Memory ");
                        Layout();
                    }
                    break;
                case ConsoleKey.B:
                    //sinh
                    if (double.TryParse(expressionSolver.ToString(), out double intVaalse))
                    {
                        if (Current == "Degree")
                        {
                            double er = intVaalse * (Math.PI / 180);
                            expressionSolver = new StringBuilder((Math.Sinh(er)).ToString());
                        }
                        else if (Current == "Radian")
                        {
                            //double er = Math.PI * (intVl / 180.0);
                            expressionSolver = new StringBuilder((Math.Sinh(intVaalse)).ToString());
                        }
                        else if (Current == "Gradient")
                        {
                            double gradientValue = intVaalse * (10.0 / 9.0);
                            double radianValue = gradientValue * (Math.PI / 180);

                            // Calculate Sin in gradient
                            // double resultInGradient = Math.Sin(radianValue) * (10.0 / 9.0);
                            expressionSolver = new StringBuilder((Math.Sinh(radianValue) * (10.0 / 9.0)).ToString());
                        }
                        //expressionSolver = new StringBuilder((Math.Sinh(intVaalse)).ToString());
                        History.Append("Sinh(" + intVaalse + ")");
                        Console.Clear();
                        Console.WriteLine(expressionHistory );

                        Console.WriteLine(expressionSolver);
                        Console.WriteLine(History + "                                      -> History ");
                        Console.WriteLine(memoryExpression.ToString() + "                             -> Memory ");
                        Layout();
                    }
                    break;
                case ConsoleKey.C:
                    //cosh
                    if (double.TryParse(expressionSolver.ToString(), out double intVaalis))
                    {
                        if (Current == "Degree")
                        {
                            double er = intVaalis * (Math.PI / 180);
                            expressionSolver = new StringBuilder((Math.Cosh(er)).ToString());
                        }
                        else if (Current == "Radian")
                        {
                            //double er = Math.PI * (intVl / 180.0);
                            expressionSolver = new StringBuilder((Math.Cosh(intVaalis)).ToString());
                        }
                        else if (Current == "Gradient")
                        {
                            double gradientValue = intVaalis * (10.0 / 9.0);
                            double radianValue = gradientValue * (Math.PI / 180);

                            // Calculate Sin in gradient
                            // double resultInGradient = Math.Sin(radianValue) * (10.0 / 9.0);
                            expressionSolver = new StringBuilder((Math.Cosh(radianValue) * (10.0 / 9.0)).ToString());
                        }
                        //expressionSolver = new StringBuilder((Math.Cosh(intVaalis)).ToString());
                        History.Append("Cosh(" + intVaalis + ")");
                        Console.Clear();
                        Console.WriteLine(expressionHistory);

                        Console.WriteLine(expressionSolver );
                        Console.WriteLine(History + "                                      -> History ");
                        Console.WriteLine(memoryExpression.ToString() + "                             -> Memory ");
                        Layout();
                    }
                    break;
                case ConsoleKey.D:
                    //sech
                    if (double.TryParse(expressionSolver.ToString(), out double intVaalsq))
                    {
                        if (Current == "Degree")
                        {
                            double er = intVaalsq * (Math.PI / 180);
                            expressionSolver = new StringBuilder((1 / Math.Cosh(intVaalsq)).ToString());
                        }
                        else if (Current == "Radian")
                        {
                            //double er = Math.PI * (intVl / 180.0);
                            expressionSolver = new StringBuilder((1 / Math.Cosh(intVaalsq)).ToString());
                        }
                        else if (Current == "Gradient")
                        {
                            double gradientValue = intVaalsq * (10.0 / 9.0);
                            double radianValue = gradientValue * (Math.PI / 180);
                            // Calculate Sin in gradient
                            // double resultInGradient = Math.Sin(radianValue) * (10.0 / 9.0);
                            expressionSolver = new StringBuilder((1 / Math.Cosh(intVaalsq) * (10.0 / 9.0)).ToString());
                        }
                        //expressionSolver = new StringBuilder((1/Math.Cosh(intVaalsq)).ToString());
                        History.Append("Sech(" + intVaalsq + ")");
                        Console.Clear();
                        Console.WriteLine(expressionHistory );

                        Console.WriteLine(expressionSolver );
                        Console.WriteLine(History + "                                      -> History ");
                        Console.WriteLine(memoryExpression.ToString() + "                             -> Memory ");
                        Layout();
                    }
                    break;
                case ConsoleKey.E:
                    //cosech
                    if (double.TryParse(expressionSolver.ToString(), out double intVaalws))
                    {
                        if (Current == "Degree")
                        {
                            double er = intVaalws * (Math.PI / 180);
                            expressionSolver = new StringBuilder((1 / Math.Sinh(intVaalws)).ToString());
                        }
                        else if (Current == "Radian")
                        {
                            //double er = Math.PI * (intVl / 180.0);
                            expressionSolver = new StringBuilder((1 / Math.Sinh(intVaalws)).ToString());
                        }
                        else if (Current == "Gradient")
                        {
                            double gradientValue = intVaalws * (10.0 / 9.0);
                            double radianValue = gradientValue * (Math.PI / 180);

                            // Calculate Sin in gradient
                            // double resultInGradient = Math.Sin(radianValue) * (10.0 / 9.0);
                            expressionSolver = new StringBuilder((1 / Math.Sinh(intVaalws) * (10.0 / 9.0)).ToString());
                        }

                        //expressionSolver = new StringBuilder((1/Math.Sinh(intVaalws)).ToString());
                        History.Append("Cosech(" + intVaalws + ")");
                        Console.Clear();
                        Console.WriteLine(expressionHistory);

                        Console.WriteLine(expressionSolver);
                        Console.WriteLine(History + "                                      -> History ");
                        Console.WriteLine(memoryExpression.ToString() + "                             -> Memory ");
                        Layout();
                    }
                    break;
                case ConsoleKey.F:
                    //coth
                    if (double.TryParse(expressionSolver.ToString(), out double iintVaals))
                    {
                        if (Current == "Degree")
                        {
                            double er = iintVaals * (Math.PI / 180);
                            expressionSolver = new StringBuilder((1 / Math.Tanh(iintVaals)).ToString());
                        }
                        else if (Current == "Radian")
                        {
                            //double er = Math.PI * (intVl / 180.0);
                            expressionSolver = new StringBuilder((1 / Math.Tanh(iintVaals)).ToString());
                        }
                        else if (Current == "Gradient")
                        {
                            double gradientValue = iintVaals * (10.0 / 9.0);
                            double radianValue = gradientValue * (Math.PI / 180);

                            // Calculate Sin in gradient
                            // double resultInGradient = Math.Sin(radianValue) * (10.0 / 9.0);
                            expressionSolver = new StringBuilder((1 / Math.Tanh(iintVaals) * (10.0 / 9.0)).ToString());
                        }

                        //expressionSolver = new StringBuilder((1/Math.Tanh(iintVaals)).ToString());
                        History.Append("Coth(" + iintVaals + ")");
                        Console.Clear();
                        Console.WriteLine(expressionHistory );

                        Console.WriteLine(expressionSolver );
                        Console.WriteLine(History + "                                      -> History ");
                        Console.WriteLine(memoryExpression.ToString() + "                             -> Memory ");
                        Layout();
                    }
                    break;
                case ConsoleKey.H:
                    if (Current == "Radian")
                    {
                        Current = "Degree";
                    }
                    else if (Current == "Degree")
                    {
                        Current = "Gradient";
                    }
                    else if (Current == "Gradient")
                    {
                        Current = "Radian";
                    }
                    Console.WriteLine(Current + "- Current Sign");

                    // Console.WriteLine($"Shift + {keyInfo.Key} pressed");
                    break;
            }
        }
        else if (specialCharecters == "Shift, Control")
        {
            switch (keyInfo.Key)
            {
                case ConsoleKey.S:


                case ConsoleKey.T:
                case ConsoleKey.O:
                case ConsoleKey.U:
                case ConsoleKey.I:
                case ConsoleKey.J:
                    // ... add more Ctrl + Key combinations as needed
                    Console.WriteLine($"Control + Shift + {keyInfo.Key} pressed");
                    break;
            }
        }
        else
        {
            // Handle other special keys
            switch (keyInfo.Key)
            {
                case ConsoleKey.Add:
                    // Console.WriteLine(expressionSolver.ToString());

                    expressionHistory.Append(expressionSolver.ToString());
                    Console.Clear();
                    while (expressionHistory[expressionHistory.Length - 1] == '/' || expressionHistory[expressionHistory.Length - 1] == '+' || expressionHistory[expressionHistory.Length - 1] == '-' || expressionHistory[expressionHistory.Length - 1] == '*')
                    {
                        expressionHistory.Remove(expressionHistory.Length - 1, 1);
                    }
                    if ((expressionHistory.Length) == 0)
                    {
                        expressionHistory.Append("0+");
                        expressionSolver.Append("0+");
                        History.Append("0+");

                    }
                    else
                    {
                        expressionHistory.Append("+");
                        expressionSolver.Append("+");
                        //History.Append("+");
                    }
                    Console.WriteLine(expressionHistory);
                    expressionSolver = new StringBuilder();
                    Console.WriteLine(expressionSolver );
                    History.Append("+");


                    Console.WriteLine(History + "                                      -> History ");
                    Console.WriteLine(memoryExpression.ToString() + "                             -> Memory ");
                    Layout();
                    break;
                case ConsoleKey.Subtract:
                    expressionHistory.Append(expressionSolver.ToString());
                    Console.Clear();
                    while (expressionHistory[expressionHistory.Length - 1] == '/' || expressionHistory[expressionHistory.Length - 1] == '+' || expressionHistory[expressionHistory.Length - 1] == '-' || expressionHistory[expressionHistory.Length - 1] == '*')
                    {
                        expressionHistory.Remove(expressionHistory.Length - 1, 1);
                    }
                    if ((expressionHistory.Length) == 0)
                    {
                        expressionHistory.Append("0-");
                        expressionSolver.Append("0-");
                        History.Append("0-");

                    }
                    else
                    {
                        expressionHistory.Append("-");
                        expressionSolver.Append("-");
                        History.Append("-");
                    }

                    Console.WriteLine(expressionHistory);
                    expressionSolver = new StringBuilder();
                    Console.WriteLine(expressionSolver );
                    Console.WriteLine(History + "                                      -> History ");
                    Console.WriteLine(memoryExpression.ToString() + "                             -> Memory ");
                    Layout();
                    break;
                case ConsoleKey.Multiply:
                    expressionHistory.Append(expressionSolver.ToString());
                    Console.Clear();
                    while (expressionHistory[expressionHistory.Length - 1] == '*' || expressionHistory[expressionHistory.Length - 1] == '+' || expressionHistory[expressionHistory.Length - 1] == '-' || expressionHistory[expressionHistory.Length - 1] == '/')
                    {
                        expressionHistory.Remove(expressionHistory.Length - 1, 1);
                    }
                    if ((expressionHistory.Length) == 0)
                    {
                        expressionHistory.Append("1*");
                        expressionSolver.Append("1*");
                        History.Append("1*");
                    }
                    else
                    {
                        expressionHistory.Append("*");
                        expressionSolver.Append("*");
                        History.Append("*");
                    }

                    Console.WriteLine(expressionHistory );
                    expressionSolver = new StringBuilder();
                    Console.WriteLine(expressionSolver );
                    Console.WriteLine(History + "                                      -> History ");
                    Console.WriteLine(memoryExpression.ToString() + "                             -> Memory ");
                    Layout();
                    break;
                case ConsoleKey.Divide:
                    expressionHistory.Append(expressionSolver.ToString());
                    Console.Clear();
                    while (expressionHistory[expressionHistory.Length - 1] == '/' || expressionHistory[expressionHistory.Length - 1] == '+' || expressionHistory[expressionHistory.Length - 1] == '-' || expressionHistory[expressionHistory.Length - 1] == '*')
                    {
                        expressionHistory.Remove(expressionHistory.Length - 1, 1);
                    }
                    if ((expressionHistory.Length) == 0)
                    {
                        expressionHistory.Append("*1/");
                        expressionSolver.Append("*1/");
                        History.Append("1/");
                    }
                    else
                    {
                        expressionHistory.Append("/");
                        expressionSolver.Append("/");
                        History.Append("/");
                    }

                    Console.WriteLine(expressionHistory );
                    expressionSolver = new StringBuilder();
                    Console.WriteLine(expressionSolver );

                    Console.WriteLine(History + "                                      -> History ");
                    Console.WriteLine(memoryExpression.ToString() + "                             -> Memory ");
                    Layout(); break;
                case ConsoleKey.D0:
                    // expressionHistory.Append(0);
                    Console.Clear();
                    if (expressionSolver.Length == 1 && expressionSolver[0] == '0')
                    {
                        expressionSolver.Remove(0, 1);
                    }
                    expressionSolver.Append(0);
                    History.Append(0);
                    Console.WriteLine(expressionHistory );
                    Console.WriteLine(expressionSolver );
                    Console.WriteLine(History + "                                      -> History ");
                    Console.WriteLine(memoryExpression.ToString() + "                             -> Memory ");
                    Layout();
                    break;
                case ConsoleKey.D1:
                    Console.Clear();
                    if (expressionSolver.Length == 1 && expressionSolver[0] == '0')
                    {
                        expressionSolver.Remove(0, 1);

                    }
                    // expressionHistory.Append(1);
                    expressionSolver.Append(1);
                    History.Append(1);
                    Console.WriteLine(expressionHistory);
                    Console.WriteLine(expressionSolver );
                    Console.WriteLine(History + "                                      -> History ");
                    Console.WriteLine(memoryExpression.ToString() + "                             -> Memory ");
                    Layout();
                    break;
                case ConsoleKey.D2:
                    // expressionHistory.Append(2);
                    Console.Clear();
                    if (expressionSolver.Length == 1 && expressionSolver[0] == '0')
                    {
                        expressionSolver.Remove(0, 1);

                    }
                    expressionSolver.Append(2);
                    History.Append(2);
                    Console.WriteLine(expressionHistory);
                    Console.WriteLine(expressionSolver );
                    Console.WriteLine(History + "                                      -> History ");
                    Console.WriteLine(memoryExpression.ToString() + "                             -> Memory ");
                    Layout();
                    break;
                case ConsoleKey.D3:
                    // expressionHistory.Append(3);
                    Console.Clear();
                    if (expressionSolver.Length == 1 && expressionSolver[0] == '0')
                    {
                        expressionSolver.Remove(0, 1);

                    }
                    expressionSolver.Append(3);
                    History.Append(3);
                    Console.WriteLine(expressionHistory);
                    Console.WriteLine(expressionSolver );

                    Console.WriteLine(History + "                                      -> History ");
                    Console.WriteLine(memoryExpression.ToString() + "                             -> Memory ");
                    Layout();
                    break;
                case ConsoleKey.D4:
                    // expressionHistory.Append(4);
                    Console.Clear();
                    if (expressionSolver.Length == 1 && expressionSolver[0] == '0')
                    {
                        expressionSolver.Remove(0, 1);

                    }
                    expressionSolver.Append(4);
                    History.Append(4);
                    Console.WriteLine(expressionHistory);
                    Console.WriteLine(expressionSolver);

                    Console.WriteLine(History + "                                      -> History ");
                    Console.WriteLine(memoryExpression.ToString() + "                             -> Memory ");
                    Layout();
                    break;
                case ConsoleKey.D5:
                    // expressionHistory.Append(5);
                    Console.Clear();
                    if (expressionSolver.Length == 1 && expressionSolver[0] == '0')
                    {
                        expressionSolver.Remove(0, 1);

                    }
                    expressionSolver.Append(5);
                    History.Append(5);
                    Console.WriteLine(expressionHistory);
                    Console.WriteLine(expressionSolver );

                    Console.WriteLine(History + "                                      -> History ");
                    Console.WriteLine(memoryExpression.ToString() + "                             -> Memory ");
                    Layout();
                    break;
                case ConsoleKey.D6:
                    // expressionHistory.Append(6);
                    Console.Clear();
                    if (expressionSolver.Length == 1 && expressionSolver[0] == '0')
                    {
                        expressionSolver.Remove(0, 1);

                    }
                    expressionSolver.Append(6);
                    History.Append(6);
                    Console.WriteLine(expressionHistory + "   -> Current Expression");
                    Console.WriteLine(expressionSolver + "    -> Current Variable");

                    Console.WriteLine(History + "                                      -> History ");
                    Console.WriteLine(memoryExpression.ToString() + "                             -> Memory ");
                    Layout();
                    break;
                case ConsoleKey.D7:
                    //  expressionHistory.Append(7);
                    Console.Clear();
                    if (expressionSolver.Length == 1 && expressionSolver[0] == '0')
                    {
                        expressionSolver.Remove(0, 1);

                    }
                    expressionSolver.Append(7);
                    History.Append(7);
                    Console.WriteLine(expressionHistory);
                    Console.WriteLine(expressionSolver );

                    Console.WriteLine(History + "                                      -> History ");
                    Console.WriteLine(memoryExpression.ToString() + "                             -> Memory ");
                    Layout();
                    break;
                case ConsoleKey.D8:
                    //  expressionHistory.Append(8);
                    Console.Clear();
                    if (expressionSolver.Length == 1 && expressionSolver[0] == '0')
                    {
                        expressionSolver.Remove(0, 1);

                    }
                    expressionSolver.Append(8);
                    History.Append(8);
                    Console.WriteLine(expressionHistory);
                    Console.WriteLine(expressionSolver );

                    Console.WriteLine(History + "                                      -> History ");
                    Console.WriteLine(memoryExpression.ToString() + "                             -> Memory ");
                    Layout();
                    break;
                case ConsoleKey.D9:
                    //  expressionHistory.Append(9);
                    Console.Clear();
                    if (expressionSolver.Length == 1 && expressionSolver[0] == '0')
                    {
                        expressionSolver.Remove(0, 1);

                    }

                    expressionSolver.Append(9);

                    History.Append(9);
                    Console.WriteLine(expressionHistory);
                    Console.WriteLine(expressionSolver );

                    Console.WriteLine(History + "                                      -> History ");
                    Console.WriteLine(memoryExpression.ToString() + "                             -> Memory ");
                    Layout();
                    break;
                case ConsoleKey.OemPlus:
                    if (isPower == true)
                    {
                        expressionHistory.Append($"{expressionSolver.ToString()})");
                        isPower = false;
                    }
                    else
                    {
                        expressionHistory.Append(expressionSolver.ToString());
                    }
                    //expressionHistory.Append(expressionSolver.ToString());
                    //Console.WriteLine(expressionHistory.ToString());
                    double ans = solveExpression(expressionHistory.ToString());
                    //Console.WriteLine(ans+"-----------------------ANS");
                    expressionHistory = new StringBuilder(ans.ToString());
                    expressionSolver = new StringBuilder(ans.ToString());
                    expressionHistory = new StringBuilder();
                    Console.Clear();
                    Console.WriteLine(expressionSolver );
                    Console.WriteLine(expressionSolver );
                    Console.WriteLine(History + "                                      -> History ");
                    Console.WriteLine(memoryExpression.ToString() + "                             -> Memory ");
                    Layout();
                    break;
                case ConsoleKey.Backspace:
                    if (expressionSolver.Length > 0)
                    {
                        expressionSolver.Remove(expressionSolver.Length - 1, 1);
                    }
                    if (History.Length > 0)
                    {
                        History.Remove(History.Length - 1, 1);
                    }
                    Console.WriteLine(expressionHistory);
                    Console.WriteLine(expressionSolver );

                    Console.WriteLine(History + "                                      -> History ");
                    Console.WriteLine(memoryExpression.ToString() + "                             -> Memory ");
                    Layout();
                    break;
                //case ConsoleKey.Delete:
                case ConsoleKey.Escape:
                    Environment.Exit(0);
                    break;
                //case ConsoleKey.F9:
                //case ConsoleKey.R:
                case ConsoleKey.DownArrow:
                    // Memory Recall
                    if (memoryExpression.Count > 0)
                    {
                        expressionSolver = new StringBuilder(memoryExpression[(memoryExpression.Count - 1)].ToString());
                    }
                    Console.Clear();
                    Console.WriteLine(expressionHistory );
                    Console.WriteLine(expressionSolver);
                    Console.WriteLine(History + "                                      -> History ");
                    Console.WriteLine(memoryExpression.ToString() + "                             -> Memory ");
                    Layout();
                    break;
                case ConsoleKey.UpArrow:
                    // M+
                    if (double.TryParse(expressionSolver.ToString(), out double intVale))
                    {
                        double k = double.Parse((String)memoryExpression[(memoryExpression.Count - 1)]);
                        memoryExpression.RemoveAt(memoryExpression.Count - 1);
                        double anss = k + intVale;
                        memoryExpression.Add(anss.ToString());
                        //History.Append("log(" + intVale + ")");
                        Console.Clear();
                        Console.WriteLine(expressionHistory + "   -> Current Expression");
                        Console.WriteLine(expressionSolver + "    -> Current Variable");
                        Console.WriteLine(History + "                                      -> History ");
                        Console.WriteLine(memoryExpression.ToString() + "                             -> Memory ");
                        Layout();
                    }
                    break;
                case ConsoleKey.LeftArrow:
                    // M-
                    if (double.TryParse(expressionSolver.ToString(), out double intValee))
                    {
                        double k = double.Parse((String)memoryExpression[(memoryExpression.Count - 1)]);
                        memoryExpression.RemoveAt(memoryExpression.Count - 1);

                        double anss = Math.Abs(k - intValee);
                        memoryExpression.Add(anss.ToString());
                        //History.Append("M-(" + intVale + ")");
                        Console.Clear();
                        Console.WriteLine(expressionHistory);
                        Console.WriteLine(expressionSolver );
                        Console.WriteLine(History + "                                      -> History ");
                        Console.WriteLine(memoryExpression.ToString() + "                             -> Memory ");
                        Layout();
                    }
                    break;
                case ConsoleKey.RightArrow:
                    // Memory Store
                    if (double.TryParse(expressionSolver.ToString(), out double intValeee))
                    {

                        memoryExpression.Add(intValeee.ToString());
                        //History.Append("log(" + intVale + ")");
                        Console.Clear();
                        Console.WriteLine(expressionHistory);
                        Console.WriteLine(expressionSolver );
                        Console.WriteLine(History + "                                      -> History ");
                        Console.WriteLine(memoryExpression.ToString() + "                             -> Memory ");
                        Layout();
                    }
                    break;
                //case ConsoleKey.F3:
                //case ConsoleKey.F4:
                //case ConsoleKey.F5:
                case ConsoleKey.G:
                    //Clear Memory
                    memoryExpression.RemoveRange(0, memoryExpression.Count);
                    memoryExpression.Add(0.ToString());
                    // pi
                    Console.Clear();
                    //expressionSolver.Append(Math.PI);
                    //History.Append(2);
                    Console.WriteLine(expressionHistory);
                    Console.WriteLine(expressionSolver );
                    Console.WriteLine(History + "                                      -> History ");
                    Console.WriteLine(memoryExpression.ToString() + "                             -> Memory ");
                    Layout();
                    break;
                case ConsoleKey.S:
                    // absolute x
                    if (double.TryParse(expressionSolver.ToString(), out double intVl))
                    {
                        expressionSolver = new StringBuilder((Math.Abs(intVl)).ToString());
                        History.Append("Absolute(" + intVl + ")");
                        Console.Clear();
                        Console.WriteLine(expressionHistory);
                        Console.WriteLine(expressionSolver );
                        Console.WriteLine(History + "                                      -> History ");
                        Console.WriteLine(memoryExpression.ToString() + "                             -> Memory ");
                        Layout();
                    }
                    break;
                case ConsoleKey.T:
                    //exp
                    if (double.TryParse(expressionSolver.ToString(), out double intVsl))
                    {
                        expressionSolver = new StringBuilder((Math.Exp(intVsl)).ToString());
                        History.Append("Exp(" + intVsl + ")");
                        Console.Clear();
                        Console.WriteLine(expressionHistory);
                        Console.WriteLine(expressionSolver );
                        Console.WriteLine(History + "                                      -> History ");
                        Console.WriteLine(memoryExpression.ToString() + "                             -> Memory ");
                        Layout();
                    }
                    break;
                case ConsoleKey.O:
                    //modulus
                    expressionHistory.Append(expressionSolver.ToString());
                    Console.Clear();
                    expressionHistory.Append("%");
                    expressionSolver.Append("%");
                    History.Append("%");
                    Console.WriteLine(expressionHistory );
                    expressionSolver = new StringBuilder();
                    Console.WriteLine(expressionSolver );

                    Console.WriteLine(History + "                                      -> History ");
                    Console.WriteLine(memoryExpression.ToString() + "                             -> Memory ");
                    Layout();
                    break;
                case ConsoleKey.U:
                    //n!
                    if (long.TryParse(expressionSolver.ToString(), out long intVssl))
                    {
                        expressionSolver = new StringBuilder((Factorial(intVssl)).ToString());
                        History.Append("Factorial(" + intVssl + ")");
                        Console.Clear();
                        Console.WriteLine(expressionHistory);
                        Console.WriteLine(expressionSolver );
                        Console.WriteLine(History + "                                      -> History ");
                        Console.WriteLine(memoryExpression.ToString() + "                             -> Memory ");
                        Layout();
                    }
                    break;
                case ConsoleKey.I:
                    //(

                    expressionHistory.Append(expressionSolver.ToString());
                    if (expressionHistory[expressionHistory.Length - 1] == '/')
                    {

                    }
                    expressionHistory.Append("1*(");

                    Console.Clear();
                    Console.WriteLine(expressionHistory);
                    expressionSolver = new StringBuilder();
                    Console.WriteLine(expressionSolver );
                    Console.WriteLine(History + "                                      -> History ");
                    Console.WriteLine(memoryExpression.ToString() + "                             -> Memory ");
                    Layout();
                    break;
                case ConsoleKey.J:
                    //)
                    expressionHistory.Append(expressionSolver.ToString());
                    if (expressionHistory[expressionHistory.Length - 1] == '(')
                    {
                        expressionHistory.Append("0)*");
                    }
                    else
                    {
                        expressionHistory.Append(")*");
                    }

                    Console.WriteLine(expressionHistory + "------------");
                    expressionSolver.Append(")*");
                    Console.Clear();
                    Console.WriteLine(expressionHistory);
                    expressionSolver = new StringBuilder();
                    Console.WriteLine(expressionSolver );
                    Console.WriteLine(History + "                                      -> History ");
                    Console.WriteLine(memoryExpression.ToString() + "                             -> Memory ");
                    Layout();

                    break;
                case ConsoleKey.M:
                    //1/x
                    if (double.TryParse(expressionSolver.ToString(), out double intVsssl))
                    {
                        expressionSolver = new StringBuilder((1 / (intVsssl)).ToString());
                        History.Append("1/x -> (" + intVsssl + ")");
                        Console.Clear();
                        Console.WriteLine(expressionHistory );

                        Console.WriteLine(expressionSolver );
                        Console.WriteLine(History + "                                      -> History ");
                        Console.WriteLine(memoryExpression.ToString() + "                             -> Memory ");
                        Layout();
                    }
                    break;
                case ConsoleKey.N:
                    // clear
                    Console.Clear();
                    expressionHistory = new StringBuilder();
                    expressionSolver = new StringBuilder();
                    Console.WriteLine(History + "                                      -> History ");
                    Console.WriteLine(memoryExpression.ToString() + "                             -> Memory ");
                    Layout();
                    break;

                case ConsoleKey.Q:
                    // x2
                    if (double.TryParse(expressionSolver.ToString(), out double intVssssl))
                    {
                        expressionSolver = new StringBuilder(Math.Pow(intVssssl, 2).ToString());
                        History.Append("\n x^2 -> (" + intVssssl + ")");
                        Console.Clear();
                        Console.WriteLine(expressionHistory );

                        Console.WriteLine(expressionSolver );
                        Console.WriteLine(History + "                                      -> History ");
                        Console.WriteLine(memoryExpression.ToString() + "                             -> Memory ");
                        Layout();
                    }
                    break;
                case ConsoleKey.V:
                    // UnderRoot
                    if (double.TryParse(expressionSolver.ToString(), out double intVssssle))
                    {
                        expressionSolver = new StringBuilder(Math.Pow(intVssssle, 0.5).ToString());
                        History.Append("\n UnderRoot -> (" + intVssssle + ")");
                        Console.Clear();
                        Console.WriteLine(expressionHistory);

                        Console.WriteLine(expressionSolver);
                        Console.WriteLine(History + "                                    -> History   ");
                        Console.WriteLine(memoryExpression.ToString() + "                             -> Memory ");

                        Layout();

                    }
                    break;
                case ConsoleKey.X:
                    //x power y
                    expressionHistory.Append($"Pow({expressionSolver.ToString()},");
                    Console.Clear();
                    isPower = true;
                    //expressionHistory.Append("^");
                    expressionSolver.Append("^");
                    History.Append("^");
                    Console.WriteLine(expressionHistory);
                    expressionSolver = new StringBuilder();
                    Console.WriteLine(expressionSolver );
                    Console.WriteLine(History + "                                    -> History");
                    Console.WriteLine(memoryExpression.ToString() + "                             -> Memory ");
                    Layout();
                    break;
                case ConsoleKey.A:
                    //Toggle

                    Console.WriteLine(Current);
                    Console.WriteLine(expressionHistory );
                    expressionSolver = new StringBuilder();
                    Console.WriteLine(expressionSolver );
                    Console.WriteLine(History + "                                    -> History");
                    Console.WriteLine(memoryExpression.ToString() + "                             -> Memory ");
                    Layout();
                    break;
            }
        }
    }















    //---------------------------------------------------------------------------------------------------------------------------
    static long Factorial(long n)
    {
        if (n < 0)
        {
            throw new ArgumentException("Factorial is undefined for negative numbers.");
        }
        else if (n == 0 || n == 1)
        {
            return 1;
        }
        else
        {
            return n * Factorial(n - 1);
        }

    }


    static int Precedence(char Operand)
    {
        if (Operand == '+' || Operand == '-')
        {
            return 1;
        }
        if (Operand == '*' || Operand == '/' || Operand == '^' || Operand == '%') { return 2; }
        return 0;
    }



    /*static int SolveBrackets(String subExpression)
    {
        //3 + 2222 * 336 / 3 + 24 * 2 +/ 2 * 8
        string currentstring = "";
        int ni = 0;
        int nj = 0;
        string firstdigit = "";
        string seconddigit = "";
        while (ni < subExpression.Length)
        {
            firstdigit += subExpression[ni];
            if (subExpression[ni] == '*' || subExpression[ni] == '/')
            {

            }
            if (subExpression[ni] == '+' || subExpression[ni] == '-')
            {

            }
        }
        return 0;
    }*/
}
