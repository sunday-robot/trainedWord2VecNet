using System;
using System.Collections.Generic;

namespace wordVec
{
    [Serializable]
    public class WordVec
    {
        int vectorSize;
        Dictionary<string, Vec> dictionary;

        public WordVec(int vectorSize, Dictionary<string, Vec> dictionary)
        {
            this.vectorSize = vectorSize;
            this.dictionary = dictionary;
        }

        public Vec Vec(string word) => dictionary[word];

        public Vec Evaluate(string expression)
        {
            var result = new Vec(vectorSize);
            var operationIsPlus = true;
            var tokens = expression.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (var token in tokens)
            {
                if (token == "+")
                    operationIsPlus = true;
                else if (token == "-")
                    operationIsPlus = false;
                else
                {
                    if (!dictionary.ContainsKey(token))
                        throw new ArgumentException($"unknown word {token}");
                    var v = dictionary[token];
                    if (operationIsPlus)
                        result += v;
                    else
                        result -= v;
                }
            }
            return result;
        }

        public List<Tuple<string,  Vec>> GetNeighbors(Vec v, int count)
        {
            double maxLength2 = 0;
            foreach (var e in dictionary.Keys)
            {

            }
        }

        public void Print()
        {
            foreach (var e in dictionary)
            {
                Console.Write($"<{e.Key}>, ");
                e.Value.Print();
            }
        }

        static void Print(string word, float[] vec)
        {
            Console.Write($"<{word}>, [{vec.Length}]");
            foreach (var e in vec)
                Console.Write($", {e}");
            Console.WriteLine();
        }
    }
}
