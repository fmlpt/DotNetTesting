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

            //TestGrouping();

            //ParseDate2();

            //TestMoneyParse();

            //SortPeople();

            //TestFindAll();

            //TestNameOf();

            //TestPercentFormat();

            //TestParseDateWithCustomCulture();

            //TestEquals();

            //GenericTest();            

            Console.ReadLine();
        }

        private static void GenericTest()
        {
            var initScript = string.Format(CultureInfo.InvariantCulture,
                    "var containerNames = $('#{0}').val(); $('#{1}').click(function() {{ ToggleContainerControl(containerNames); }});",
                    "idnumber1", "idnumber2");

            var initScript2 =   $"var containerNames = $('#idnumber1').val();" +
                                $"$('#idnumber2').click(function()" + "{ToggleContainerControl(containerNames)});";

            Console.WriteLine(initScript.Equals(initScript2, StringComparison.OrdinalIgnoreCase));
        }

        private static void TestEquals()
        {
            var val = "M";

            var equals = "m".Equals(val, StringComparison.OrdinalIgnoreCase);

            Console.WriteLine(equals);
        }

        private static void TestParseDateWithCustomCulture()
        {
            var date = "2017/05/06";

            var format = new DateTimeFormatInfo
            {
                ShortDatePattern = "yyyy/MM/dd"
            };

            var result = DateTime.Parse(date, format);

            Console.WriteLine(result.ToString("d"));
        }

        private static void TestPercentFormat()
        {
            var num = 0.45M;
            var perc = num.ToString("0.00\\%", CultureInfo.InvariantCulture);
            Console.WriteLine(perc);
        }

        private static void TestNameOf()
        {
            var nameOfVar = 1400M;

            Console.WriteLine(nameof(nameOfVar));

        }

        private static void TestFindAll()
        {
            var people = new List<Person>
            {
                new Person { Name="John", Gender="M", BirthDate = new DateTime(1965, 5, 8), Address= "Lexington Avenue" },
                new Person { Name="Peter", Gender="M", BirthDate = new DateTime(1995, 12, 6), Address="130 Cool Street" },
                new Person { Name="Kate", Gender="F", BirthDate = new DateTime(2000, 4, 3), Address = "26 Cool Street" },
                new Person { Name="Phil", Gender="M", BirthDate = new DateTime(1980, 4, 25), Address = "Abercrombie Street Apts." },
                new Person { Name="Giselle", Gender="F", BirthDate = new DateTime(1981, 5, 5), Address = "43 Cool Street" },
            };

            var result = people.FindAll(p => "Donald".Equals(p.Name));
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
            var saNumberFormat = new NumberFormatInfo
            {
                CurrencyDecimalSeparator = ".",
                CurrencyGroupSeparator = " ",
                CurrencySymbol = "R"
            };

            var value = "R1 237 500.00";
            var value2 = "1237500.00";

            var result = decimal.Parse(value, NumberStyles.AllowCurrencySymbol | NumberStyles.AllowDecimalPoint | NumberStyles.AllowThousands, saNumberFormat);
            Console.WriteLine(result.ToString(CultureInfo.InvariantCulture));
            var result2 = decimal.Parse(value2, NumberStyles.AllowCurrencySymbol | NumberStyles.AllowDecimalPoint | NumberStyles.AllowThousands, saNumberFormat);
            Console.WriteLine(result2.ToString(CultureInfo.InvariantCulture));

        }

        private static void ParseDate2()
        {
            DateTime selectedDate;
            if (DateTime.TryParseExact("551105", "yyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.None, out selectedDate))
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

            res.ForEach(r =>
            {
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
