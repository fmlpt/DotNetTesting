using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestingConsoleApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            TestEnumeration();
        }

        private static void TestEnumeration()
        {
            var enumerable = Enumerable.Range(1, 3);

            foreach (var v in enumerable)
            {
                Console.WriteLine(v);
            }
        }

        private static void ParseDate2()
        {
            DateTime selectedDate;
            if(DateTime.TryParseExact("551105", "yyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.None, out selectedDate))
            {
                Console.WriteLine($"Yay! Date: {selectedDate}");
                return;
            }

            Console.WriteLine("Booo!");
        }

        private static void TestGrouping()
        {
            var people = new List<Person>
            {
                new Person { Name="John", Gender="M", BirthDate = new DateTime(1965, 5, 8) },
                new Person { Name="Peter", Gender="M", BirthDate = new DateTime(1995, 12, 6) },
                new Person { Name="Kate", Gender="F", BirthDate = new DateTime(2000, 4, 3) }
            };


            var res = people.GroupBy(p => p.Gender).Select(g => new Tuple<string, List<Person>>(g.Key, g.ToList())).ToList();

            res.ForEach(r => {
                Console.WriteLine();
                Console.WriteLine("Gender: " + r.Item1);

                r.Item2.ForEach(rg =>
                {
                    Console.WriteLine($"Name: {rg.Name} Birth date: {rg.BirthDate:dd/MM/yyyy}");
                });
            });
        }

        private static void ParseDate()
        {
            var res = DateTime.ParseExact("20170519 100322", "yyyyMMdd hhmmss", CultureInfo.InvariantCulture);
        }
    }
}
