using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Linq.Dynamic;
using System.Text;
using System.Threading.Tasks;
using static System.FormattableString;

namespace TestingConsoleApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            //UseInvariant();

            //TestDynamicQueryable();

            //TestKeyValuePairNull();
        }

        private static void TestKeyValuePairNull()
        {
            var x = default(KeyValuePair<string, string>);
        }

        private static void TestDynamicQueryable()
        {
            var people = new List<Person>
            {
                new Person {Name = "John", Gender = "M", BirthDate = new DateTime(1965, 5, 8)},
                new Person {Name = "Peter", Gender = "M", BirthDate = new DateTime(1995, 12, 6)},
                new Person {Name = "Kate", Gender = "F", BirthDate = new DateTime(2000, 4, 3)}
            }.AsQueryable();


            var orderedPeople = people.OrderBy("BirthDate asc").ToList();


            orderedPeople.ForEach(person => Console.WriteLine($"{person.Name} - {person.BirthDate}"));
        }


        private static void UseInvariant()
        {
            var test = 123;

            Console.WriteLine(Invariant($"abc {test}"));
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
