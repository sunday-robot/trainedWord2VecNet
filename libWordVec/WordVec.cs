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

        static void GetMinValueIndex(List<double> list, out int minValueIndex, out double minValue)
        {
            var r = 0;
            var v = double.MaxValue;
            for (int i = 1; i < list.Count; i++)
            {
                if (list[i] < v)
                {
                    v = list[i];
                    r = i;
                }
            }
            minValueIndex = r;
            minValue = v;
        }

        /// <summary>
        /// 指定されたベクトル値に近いものを返す。
        /// </summary>
        /// <param name="v"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public List<Tuple<string,  Vec>> GetNeighbors(Vec v, int count)
        {
            var wvList = new List<Tuple<string, Vec>>();
            var d2List = new List<double>();

            var minD2 =  0.0;
            var minD2Index = 0;
            foreach (var e in dictionary)
            {
                var d2 = (v - e.Value).Length2();
                if (wvList.Count < count)
                {
                    wvList.Add(new Tuple<string, Vec>(e.Key, e.Value));
                    d2List.Add(d2);
                    if (wvList.Count == count)
                        GetMinValueIndex(d2List, out minD2Index, out minD2);
                }
                else
                {
                    if (d2 < minD2)
                    {
                        wvList[minD2Index] = new Tuple<string, Vec>(e.Key, e.Value);
                        GetMinValueIndex(d2List, out minD2Index, out minD2);
                    }
                }
            }
            return wvList;
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
