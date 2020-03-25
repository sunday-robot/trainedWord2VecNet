using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using wordVec;

namespace libWordVecSample
{
    class Program
    {
        static void Main(string[] args)
        {
            var binFilePath = "../../../ja.bin";
            ListWords(binFilePath);
            //EvaluateSample(binFilePath);
            //SerializeSample(args[0]);
            //DeserializaSample();
        }

        static void ListWords(string filePath)
        {
            var wv = WordVecLoader.Load(filePath);
            var e = wv.GetWords();
            for (var i = 0; i < e.Length; i++)
            {
                Console.WriteLine($"{i}\t[{e[i]}]");
            }
        }

        static void SerializeSample(string tsvFilePath)
        {
            var binFilePath = "../../../ja.bin";
            var bf = new BinaryFormatter();

            var wv = WordVecLoader.LoadTsv(tsvFilePath);
            WordVec wv2;

            {
                var fs = new FileStream(binFilePath, FileMode.Create);
                bf.Serialize(fs, wv);
                fs.Close();
            }
            {
                var fs = new FileStream(binFilePath, FileMode.Open);
                wv2 = (WordVec)bf.Deserialize(fs);
                fs.Close();
            }
            var result = wv2.Evaluate("こんにちは + 世界");
            result.Print();
        }

        static void DeserializaSample()
        {
            var binFilePath = "../../../ja.bin";
            var bf = new BinaryFormatter();

            var fs = new FileStream(binFilePath, FileMode.Open);
            var wv = (WordVec)bf.Deserialize(fs);
            fs.Close();

            var result = wv.Evaluate("こんにちは + 世界");
            result.Print();
        }

        static void EvaluateSample(string filePath)
        {
            var wv = WordVecLoader.Load(filePath);
            string s;
            s = " こんにちは   + 世界 ";
            s = "日本";
            s = "おはよう - 朝 - 夜";
            s = "男 - 女";
            var result = wv.Evaluate(s);
//            result.Print();
            var neighbors = wv.GetNeighbors(result, 10);
            foreach (var e in neighbors)
            {
                var word = e.Item1;
                var distance = e.Item3;
                Console.WriteLine($"{word} : {distance}");
            }
        }
    }
}
