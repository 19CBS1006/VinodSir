//The 'Intern' & 'IsIntern', is a process of storing only one copy of each distinct string value in memory, even if there are multiple instances of that string in the program.
//This helps in saving the memory by reducing the no. of duplicate string instances.
//String literals are interned by default, but the string created from StringBuilder is not interned unless you explicitly call String.Intern on it.

using System;
using System.Net.NetworkInformation;
using System.Text;
namespace Assignment6_String_Intern_Example
{
    internal class String_Intern_Example
    {
        static void Main()
        {
            string s1 = "One";
            string s2 = new StringBuilder().Append("On").Append("e").ToString();
            string s3 = new StringBuilder().Append("On").Append("e").ToString();

            Console.WriteLine($"String s1= {s1}");
            Console.WriteLine($"String s2= {s2} created using String Builder");
            Console.WriteLine($"String s3= {s3} created using String Builder");
            Console.WriteLine();
            //We are using IsInterned to check if each string is interned.The method returns either the interned reference or null.
            Console.WriteLine("Is s1 Interned or not: " + String.IsInterned(s1));
            Console.WriteLine("Is s2 Interned or not: " + String.IsInterned(s2));
            Console.WriteLine("Is s3 Interned or not: " + String.IsInterned(s3));
            Console.WriteLine();
            //Checkin if they are sharing the same refernce in memory or not.
            Console.WriteLine($"Is s1 the same reference as s2: {(Object)s1 == (Object)s2}");
            Console.WriteLine($"Is s1 the same reference as s3: {(Object)s1 == (Object)s3}");
            Console.WriteLine($"Is s2 the same reference as s3: {(Object)s2 == (Object)s3}");
            Console.WriteLine();
            //Using Intern to intern a string & checking it further whether it is interned or not.
            string s4 = String.Intern(s2);
            Console.WriteLine($"String s4 (interned from s2)= {s4}");
            Console.WriteLine();
            Console.WriteLine("Is s4 Interned or not: " + String.IsInterned(s4));
            Console.WriteLine();
            Console.WriteLine($"Is s1 the same reference as s4: {(Object)s1 == (Object)s4}");
            Console.WriteLine($"Is s3 the same reference as s4: {(Object)s3 == (Object)s4}");
            Console.WriteLine();
            string s5 = String.Intern(s3);
            Console.WriteLine($"String s5 (interned from s3) = {s5}");
            Console.WriteLine();
            Console.WriteLine("Is s5 Interned or not: " + String.IsInterned(s5));
            Console.WriteLine();
            Console.WriteLine($"Is s5 the same reference as s4: {(Object)s5 == (Object)s4}");
            Console.WriteLine();
            string s6 = "Two";
            string s7 = "TWO";
            Console.WriteLine($"s6= {s6}");
            Console.WriteLine($"s7= {s7}");
            Console.WriteLine();
            Console.WriteLine("Is s6 Interned or not: " + String.IsInterned(s6));
            Console.WriteLine("Is s7 Interned or not: " + String.IsInterned(s7));
            Console.WriteLine();
            Console.WriteLine($"Is s6 the same reference as s7: {(Object)s6 == (Object)s7}");
            Console.WriteLine();
            string s8 = String.Intern(s7);
            Console.WriteLine($"s8= {s8}");
            Console.WriteLine($"Is s6 the same reference as s8: {(Object)s6 == (Object)s8}");
            Console.WriteLine();
            string s9 = "Two";
            Console.WriteLine($"s9= {s9}");
            Console.WriteLine($"Is s6 the same reference as s9: {(Object)s6 == (Object)s9}");
            Console.WriteLine();
            string s10 = new StringBuilder().Append("One").ToString();
            Console.WriteLine($"s10= {s10}");
            Console.WriteLine($"Is s1 the same reference as s10: {(Object)s1 == (Object)s10}");
            Console.WriteLine();
            string s11 = new StringBuilder().Append("Fo").Append("ur").ToString();
            Console.WriteLine($"s11= {s11}");
            Console.WriteLine("Is s11 Interned or not: " + String.IsInterned(s11));
            Console.WriteLine();
            string s12 = new StringBuilder().Append("Four").ToString();
            Console.WriteLine($"s12= {s12}");
            Console.WriteLine("Is s12 Interned or not: " + String.IsInterned(s12));
            Console.WriteLine();
            string s13 = new StringBuilder().Append("Five").ToString();
            Console.WriteLine($"s13= {s13}");
            Console.WriteLine("Is s13 Interned or not: " + String.IsInterned(s13));
            Console.WriteLine();
            string s14 = new StringBuilder().Append("Si").Append("x").ToString();
            Console.WriteLine($"s14= {s14}");
            Console.WriteLine("Is s14 Interned or not: " + String.IsInterned(s14));
        }
    }
}


