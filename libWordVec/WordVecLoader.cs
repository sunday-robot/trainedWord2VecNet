using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

namespace wordVec
{
    public static class WordVecLoader
    {
        public static WordVec LoadTsv(string filePath)
        {
            int vectorSize;
            var dictionary = new Dictionary<string, Vec>();

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
                            dictionary[word] = new Vec(vec.ToArray());
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
                vectorSize = vec.Count;
                dictionary[word] = new Vec(vec.ToArray());
            }

            return new WordVec(vectorSize, dictionary);
        }

        public static void Save(WordVec wordVec, string filePath)
        {
            var bf = new BinaryFormatter();
            var fs = new FileStream(filePath, FileMode.Create);
            bf.Serialize(fs, wordVec);
            fs.Close();
        }

        public static WordVec Load(string filePath)
        {
            var bf = new BinaryFormatter();
            var fs = new FileStream(filePath, FileMode.Open);
            var wv = (WordVec)bf.Deserialize(fs);
            fs.Close();
            return wv;
        }
    }
}
