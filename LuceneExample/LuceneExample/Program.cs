using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuceneExample
{
    class Program
    {
        static void Main(string[] args)
        {
            var config = new LuceneSearchConfig();

            config.InitializeSearch();

            config.IndexData();

            const string query = "an";

            var results = config.Search(query);

            config.FinalizeSearch();

            if (results.Any())
            {
                Console.WriteLine($"{results.Count()} results found for '{query}':");

                foreach (var result in results)
                {
                    Console.WriteLine(
                        $"Name: {result.FirstName} {result.LastName} - Born on {result.BirthDate.ToString("dd-MM-yyyy")}");
                }
            }
            else
            {
                Console.WriteLine($"No results found for '{query}'");
            }

            Console.WriteLine("Press any key to continue...");
            Console.ReadLine();
        }

        
    }
}
