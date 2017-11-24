using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lucene.Net.Analysis;
using Lucene.Net.Analysis.Standard;
using Lucene.Net.Documents;
using Lucene.Net.Index;
using Lucene.Net.QueryParsers;
using Lucene.Net.Search;
using Lucene.Net.Store;
using Directory = Lucene.Net.Store.Directory;
using Version = Lucene.Net.Util.Version;

namespace LuceneExample
{
    public class LuceneSearchConfig
    {
        private Directory _directory;
        private Analyzer _analyzer;
        private IndexWriter _writer;

        public void InitializeSearch()
        {
            var directoryPath = Environment.CurrentDirectory.Replace(@"\bin\Debug", string.Empty) + @"\App_Data\LuceneIndexes";

            ClearIndexFiles(directoryPath);

            var directoryInfo = new DirectoryInfo(directoryPath);
            _directory = FSDirectory.Open(directoryInfo);
            //_analyzer = new StandardAnalyzer(Version.LUCENE_29);
            //_analyzer = new CustomAnalyzer();
            _analyzer = new NGramAnalyzer();
            _writer = new IndexWriter(_directory, _analyzer, IndexWriter.MaxFieldLength.UNLIMITED); 
        }

        private void ClearIndexFiles(string directoryPath)
        {
            var di = new DirectoryInfo(directoryPath);

            foreach (var file in di.GetFiles())
            {
                file.Delete();
            }
        }

        public void IndexData()
        {
            IPeopleService peopleService = new PeopleService();
            var people = peopleService.GetAll();
            

            foreach (var person in people)
            {
                var doc = new Document();
                doc.Add(new Field("FirstName", person.FirstName, Field.Store.YES, Field.Index.ANALYZED));
                doc.Add(new Field("LastName", person.LastName, Field.Store.YES, Field.Index.ANALYZED));
                doc.Add(new Field("EmailAddress", person.EmailAddress, Field.Store.YES, Field.Index.NOT_ANALYZED));
                doc.Add(new Field("BirthDate", person.BirthDate.ToString("dd-MM-yyyy"), Field.Store.YES, Field.Index.NOT_ANALYZED));
                _writer.AddDocument(doc);
            }

            _writer.Optimize();
            _writer.Commit();
            _writer.Dispose();
        }

        public void FinalizeSearch()
        {
            _directory.Dispose();
        }

        public IEnumerable<Person> Search(string query)
        {
            var searchResults = new List<Person>();
            var searcher = new IndexSearcher(_directory, true);
            var parser = new QueryParser(Version.LUCENE_29, "FirstName", _analyzer);
            var searchQuery = parser.Parse(query);
            var hits = searcher.Search(searchQuery, 10);
            
            foreach(var scoreDoc in hits.ScoreDocs)
            {
                var doc = searcher.Doc(scoreDoc.doc);

                var person = new Person
                {
                    FirstName = doc.Get("FirstName"),
                    LastName = doc.Get("LastName"),
                    EmailAddress = doc.Get("EmailAddress"),
                    BirthDate = DateTime.ParseExact(doc.Get("BirthDate"), "dd-MM-yyyy", CultureInfo.InvariantCulture)
                };

                searchResults.Add(person);
            }

            return searchResults.AsEnumerable();
        }
    }
}
