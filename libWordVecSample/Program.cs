using System;
using System.Collections.Generic;
using wordVec;

namespace libWordVecSample
{
    class Program
    {
        static void Main(string[] args)
        {
            var wv = WordVecLoader.Load(args[0]);
            //wv.Print();
            var s = " こんにちは   + 世界 ";
            var result = wv.Evaluate(s);
        }
    }
}
