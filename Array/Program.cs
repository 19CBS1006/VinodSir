//TEMPLATE or GENERICS

using System;
namespace ConsoleApp
{
    class TemplateArray
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter the size of the array: ");
            int size = Convert.ToInt32(Console.ReadLine());
            
            int[] intArray = new int[size];
            double[] doubleArray = new double[size];
            char[] charArray = new char[size];
            string[] stringArray = new string[size];

            Console.WriteLine("\nEnter a string: ");
            string str = Console.ReadLine();

            int num;
            bool intResult = int.TryParse(str, out num);
            double num1;
            bool doubleResult = double.TryParse(str, out num1);
            char num2;
            bool charResult = char.TryParse(str, out num2);


            if (intResult)
            {
                Console.WriteLine("\nThe element \'" + str + "\' is an Integer.");
                Console.WriteLine("\nNow further enter elements in integer array upto " + size + " size: ");
                intArray[0] = num;
                Console.WriteLine("1. " + intArray[0]);
                WritingValue(intArray, size, "integer");
                Console.WriteLine("\n\t...................PRINTING...................");    
                Console.WriteLine("\nINTEGER ARRAY: ");
                Printing(intArray, size);
            }

            else if(doubleResult)
            {
                Console.WriteLine("\nThe element \'" + str + "\' is a Double / Float.");
                Console.WriteLine("\nNow further enter elements in double array upto " + size + " size: ");
                doubleArray[0] = num1;
                Console.WriteLine("1. " + doubleArray[0]);
                WritingValue(doubleArray, size, "double");
                Console.WriteLine("\n\t...................PRINTING...................");
                Console.WriteLine("\nDOUBLE ARRAY: ");
                Printing(doubleArray, size);
            }

            else if(charResult)
            {
                Console.WriteLine("\nThe element \'" + str + "\' is a Character.");
                Console.WriteLine("\nNow further enter elements in character array upto " + size + " size: ");
                charArray[0] = num2;
                Console.WriteLine("1. " + charArray[0]);
                WritingValue(charArray, size, "char");
                Console.WriteLine("\n\t...................PRINTING...................");
                Console.WriteLine("CHARACTER ARRAY: ");
                Printing(charArray, size);
            }

            else if (str.GetType() == typeof(string))
            {
                Console.WriteLine("\nThe element \'" + str + "\' is a String.");
                Console.WriteLine("\nNow further enter elements in string array upto " + size + " size: ");
                stringArray[0] = str;
                Console.WriteLine("1. " + stringArray[0]);
                WritingValue(stringArray, size, "string");
                Console.WriteLine("\n\t...................PRINTING...................");
                Console.WriteLine("STRING ARRAY: ");
                Printing(stringArray, size);
            }

        }

        static void Printing<T>(T[] array, int size)
        {
            int i;
            for(i = 0; i < size; i++)
            {
                Console.Write((i+1) + ". " + array[i] + "\n");
            }
            Console.WriteLine();
        }

        static void WritingValue <T> (T[] array, int size, string expectedType)
        {
            int i;
            for(i = 1; i < array.Length; i++)
            {
                Console.Write(i+1 + ". ");
                string userInput = Console.ReadLine();
                if(TypeOfArray(userInput, out T result))
                {
                    array[i] = result;
                }
                else
                {
                    Console.Write("You need to enter a " + expectedType + " value. Enter the value in " + (i+1) + " again: ");
                    i = i - 1;
                }
            }
            
        }

        static bool TypeOfArray <T> (string input, out T result)
        {
            try
            {
                result = (T)Convert.ChangeType(input, typeof(T));
                return true;
            }
            catch
            {
                result =  default(T);
                return false;
            }
        }
    }
}




