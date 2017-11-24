using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lucene.Net.Analysis;

namespace LuceneExample
{
    public class NGramAnalyzer : Analyzer
    {
        public override TokenStream TokenStream(string fieldName, TextReader reader)
        {
            return new LowerCaseFilter( // case insensitive
                new ASCIIFoldingFilter( // remove accents
                    new NGramTokenizer(reader, 2)));
        }
    }
}
