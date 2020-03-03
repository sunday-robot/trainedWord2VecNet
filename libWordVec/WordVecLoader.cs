using System.Collections.Generic;
using System.IO;
using System.Text;

namespace wordVec
{
    public static class WordVecLoader
    {
        public static List<WordVec> load(string filePath)
        {
            var list = new List<WordVec>();

            using (var sr = new StreamReader(filePath, Encoding.UTF8))
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

            return list;
        }
    }
}
