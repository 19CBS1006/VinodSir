using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculatorComponent.Data
{

    public class CustomStack<T>
    {

        public int top = -1;
        T[] stack = new T[10];

        public T this[int index]
        {
            get
            {
                return stack[index];
            }
            set { }
        }
        //public void Plus(double data)
        //{
        //    stack[top] = 
        //}

        public T peek()
        {
            if (top == -1)
            {
                return default;
            }
            return stack[top];
        }
        public void push(T val)
        {
            if (top == stack.Length - 1)
            {
                T[] newArray = new T[top * 2];
                int i = 0;
                foreach (T x in stack)
                {
                    newArray[i] = x;
                    i++;
                }

            }

            stack[++top] = val;

        }
        public void Clear()
        {
            for (int i = 0; i <= top; i++)
            {
                stack[i] = default;
            }
            top = -1;
        }
        public T pop()
        {
            if (top == -1)
            {
                return default;
            }
            return stack[top--];
        }

        public void Display()
        {
            foreach (T x in stack)
            {
                Console.WriteLine(x);
            }
        }
    }


    public class MemoryOperations
    {



        public CustomStack<double> cstack = null;

        //public CustomStack<double> this[]{


        public MemoryOperations()
        {
            cstack = new CustomStack<double>();
     
        }

      

        public void MSave(double data)
        {
            cstack.push(data);
            Console.WriteLine(cstack.peek());
        }

        public void MPlus(double data)
        {
            double n_data = cstack.peek();
            cstack.pop();

            cstack.push(data + n_data);

        }

        public void MMinus(double data)
        {

            double n_data = cstack.peek();
            cstack.pop();

            cstack.push(n_data - data);
            

        }

        public double MRead()
        {

            return cstack.peek();

        }

        public void MClear()
        {
            cstack.Clear();
        }



    }


}
