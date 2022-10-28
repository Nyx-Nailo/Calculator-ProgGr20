using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator
{
    internal class Math<T>
    {
        // Mathematical operations
        public T Addition(T x, T y)
        {
            dynamic x1 = x;
            dynamic y1 = y;
            return x1 + y1;
        }
        public T Subtraction(T x, T y)
        {
            dynamic x1 = x;
            dynamic y1 = y;
            return x1 - y1;
        }
        public T Multiplication(T x, T y)
        {
            dynamic x1 = x;
            dynamic y1 = y;
            return x1 * y1;
        }
        public T Division(T x, T y)
        {
            dynamic x1 = x;
            dynamic y1 = y;
            return x1 / y1;
        }
    }
}
