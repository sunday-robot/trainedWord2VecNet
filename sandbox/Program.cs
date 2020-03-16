using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sandbox
{
    class Program
    {
        static void Main(string[] args)
        {
            var s = " こんにちは   + 世界 ";
            var f = s.Split(new char[]{ ' '}, StringSplitOptions.RemoveEmptyEntries);
            Console.WriteLine(f);
        }

    }
}
