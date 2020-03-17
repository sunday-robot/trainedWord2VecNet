using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wordVec
{
    public class Vec
    {
        float[] values;

        public Vec(float[] values)
        {
            this.values = (float[])values.Clone();
        }

        public Vec(int vectorSize)
        {
            this.values = new float[vectorSize];
        }

        Vec Add(Vec a)
        {
            for (int i = 0; i < values.Length; i++)
                values[i] += a.values[i];
            return this;
        }

        Vec Sub(Vec a)
        {
            for (int i = 0; i < values.Length; i++)
                values[i] -= a.values[i];
            return this;
        }

        public void Print()
        {
            Console.Write("[{values.Length}]");
            foreach (var e in values)
                Console.Write($", {e}");
            Console.WriteLine();
        }

        public static Vec operator +(Vec a, Vec b)
        {
            var r = new Vec(a.values.Length);
            for (int i = 0; i < a.values.Length; i++)
                r.values[i] = a.values[i] + b.values[i];
            return r;
        }

        public static Vec operator -(Vec a, Vec b)
        {
            var r = new Vec(a.values.Length);
            for (int i = 0; i < a.values.Length; i++)
                r.values[i] = a.values[i] - b.values[i];
            return r;
        }
    }
}
