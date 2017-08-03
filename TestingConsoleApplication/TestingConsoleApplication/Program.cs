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
            //ParseDate();

            // TestGrouping();

            //ParseDate2();

            //TestMoneyParse();

            SortPeople();

            Console.ReadLine();
        }

        private static void SortPeople()
        {
            var people = new List<Person>
            {
                new Person { Name="John", Gender="M", BirthDate = new DateTime(1965, 5, 8), Address= "Lexington Avenue" },
                new Person { Name="Peter", Gender="M", BirthDate = new DateTime(1995, 12, 6), Address="130 Cool Street" },
                new Person { Name="Kate", Gender="F", BirthDate = new DateTime(2000, 4, 3), Address = "26 Cool Street" },
                new Person { Name="Phil", Gender="M", BirthDate = new DateTime(1980, 4, 25), Address = "Abercrombie Street Apts." },
                new Person { Name="Giselle", Gender="F", BirthDate = new DateTime(1981, 5, 5), Address = "43 Cool Street" },
            };

            people.Sort(new PersonComparerByAddress());

            people.ForEach(Console.WriteLine);
        }

        private static void TestMoneyParse()
        {
            var saNumberFormat = new  NumberFormatInfo
            {
                CurrencyDecimalSeparator = ".",
                CurrencyGroupSeparator = " ",
                CurrencySymbol = "R"
            };

            var value = "R1 237 500.00";

            var result = decimal.Parse(value, NumberStyles.AllowCurrencySymbol | NumberStyles.AllowDecimalPoint | NumberStyles.AllowThousands, saNumberFormat);

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
