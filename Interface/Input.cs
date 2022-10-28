using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interface
{
    internal class Input<T>
    {
        public Input()
        {
        }
        // Tar input och converterar till data typ T
        public T ConsoleInputT()
        {
            String? input = Console.ReadLine();
            T ret = (T)Convert.ChangeType(input, typeof(T));
            return ret;
        }
        // Tar in input och converterar till char
        public char ConsoleInputChar()
        {
            String? input;
            char c;
            input = Console.ReadLine();
            char.TryParse(input, out c);
            return c;
        }
    }
}

