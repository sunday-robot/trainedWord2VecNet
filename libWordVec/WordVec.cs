using System.Collections.Generic;

namespace wordVec
{
    public class WordVec
    {
        public string Word { get; private set; }
        public List<float> Vec { get; private set; }

        public WordVec(string word, List<float> vec)
        {
            Word = word;
            Vec = vec;
        }
    }
}
