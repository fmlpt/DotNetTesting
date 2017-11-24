using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lucene.Net.Analysis;

namespace LuceneExample
{
    class CustomAnalyzer : Analyzer
    {
        private static readonly string[] StopWords = new string[]
                                                         {
                                                             "0", "1", "2", "3", "4", "5", "6", "7", "8",
                                                             "9", "000", "$", "£",
                                                             "about", "after", "all", "also", "an", "and",
                                                             "another", "any", "are", "as", "at", "be",
                                                             "because", "been", "before", "being", "between",
                                                             "both", "but", "by", "came", "can", "come",
                                                             "could", "did", "do", "does", "each", "else",
                                                             "for", "from", "get", "got", "has", "had",
                                                             "he", "have", "her", "here", "him", "himself",
                                                             "his", "how", "if", "in", "into", "is", "it",
                                                             "its", "just", "like", "make", "many", "me",
                                                             "might", "more", "most", "much", "must", "my",
                                                             "never", "now", "of", "on", "only", "or",
                                                               "other", "our", "out", "over", "re", "said",
                                                             "same", "see", "should", "since", "so", "some",
                                                             "still", "such", "take", "than", "that", "the",
                                                             "their", "them", "then", "there", "these",
                                                             "they", "this", "those", "through", "to", "too",
                                                             "under", "up", "use", "very", "want", "was",
                                                             "way", "we", "well", "were", "what", "when",
                                                             "where", "which", "while", "who", "will",
                                                             "with", "would", "you", "your",
                                                             "a", "b", "c", "d", "e", "f", "g", "h", "i",
                                                             "j", "k", "l", "m", "n", "o", "p", "q", "r",
                                                             "s", "t", "u", "v", "w", "x", "y", "z"
                                                         };

        public CustomAnalyzer()
            : this(StopWords)
        {
        }

        public CustomAnalyzer(string[] stopWords)
        {
            StopTable = StopFilter.MakeStopSet(stopWords);
        }

        public Hashtable StopTable { get; }

        public override TokenStream TokenStream(string fieldName, TextReader reader)
        {
            return
                new PorterStemFilter(
                    new ASCIIFoldingFilter(new StopFilter(false, new LowerCaseTokenizer(reader),
                        new CharArraySet(StopWords, true))));
        }
    }
}
