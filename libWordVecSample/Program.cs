using System;
using System.Collections.Generic;
using wordVec;

namespace libWordVecSample
{
    class Program
    {
        static void Main(string[] args)
        {
            var list = WordVecLoader.load(args[0]);
            Print(list);
        }

        static void Print(List<WordVec> list)
        {
            foreach (var e in list)
                Print(e.Word, e.Vec);
        }

        static void Print(string word, List<float> vec)
        {
            Console.Write($"<{word}>, [{vec.Count}]");
            foreach (var e in vec)
                Console.Write($", {e}");
            Console.WriteLine();
        }
    }
}
