using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace trainedWord2VecNet
{
    class Program
    {
        static void Main(string[] args)
        {
            var list = new List<WordVec>();

            using (var sr = new StreamReader(args[0], Encoding.UTF8))
            {
                string word = null;
                List<float> vec = null;
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    var f = line.Split('\t');
                    string elements;
                    if (f.Length != 1)
                    {
                        if (word != null)
                            list.Add(new WordVec(word, vec));
                        word = f[1];
                        vec = new List<float>();
                        elements = f[2].Substring(1);  // 開始行
                    }
                    else if (line.EndsWith("]"))
                        elements = line.Substring(0, line.Length - 1); // 最終行
                    else
                        elements = line; // 途中の行

                    foreach (var e in elements.Split(' '))
                        if (e.Length != 0)
                            vec.Add(float.Parse(e));
                }
                list.Add(new WordVec(word, vec));
            }

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
