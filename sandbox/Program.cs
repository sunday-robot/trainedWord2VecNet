using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace sandbox
{
    class Program
    {
        static void Main(string[] args)
        {
            var s = " こんにちは   + 世界 ";
            var f = s.Split(new char[]{ ' '}, StringSplitOptions.RemoveEmptyEntries);
            Console.WriteLine(f);


            // シリアライズ、デシリアライズは以下のページを参考にした。
            // https://docs.microsoft.com/ja-jp/dotnet/api/system.runtime.serialization.formatters.binary.binaryformatter?view=netframework-4.8
            {
                var fs = new FileStream("DataFile.dat", FileMode.Create);
                var bf = new BinaryFormatter();
                bf.Serialize(fs, f);
                fs.Close();
            }
            {
                var fs = new FileStream("DataFile.dat", FileMode.Open);
                var bf = new BinaryFormatter();
                var f2 = (string[])bf.Deserialize(fs);
                fs.Close();

                Console.WriteLine(f);
            }

        }

    }
}
