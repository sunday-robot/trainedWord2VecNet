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
            //EvaluateSample(args[0]);
            //SerializeSample(args[0]);
            DeserializaSample();
        }

        static void SerializeSample(string tsvFilePath)
        {
            var binFilePath = "ja.bin";
            var bf = new BinaryFormatter();

            var wv = WordVecLoader.Load(tsvFilePath);
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
            //wv.Print();
            var s = " こんにちは   + 世界 ";
            var result = wv.Evaluate(s);
            result.Print();
        }

    }
}
