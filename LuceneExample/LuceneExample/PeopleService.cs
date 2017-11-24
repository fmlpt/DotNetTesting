using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuceneExample
{
    public class PeopleService :IPeopleService
    {
        public IEnumerable<Person> GetAll()
        {
            return new[]
            {
                new Person{FirstName = "Rafael", LastName = "Nadal", EmailAddress = "rafa@tennis.com", BirthDate = new DateTime(1986, 6, 3)},
                new Person{FirstName = "Pete", LastName = "Sampras", EmailAddress = "pete@tennis.com", BirthDate = new DateTime(1971, 8, 12)},
                new Person{FirstName = "Andre", LastName = "Agassi", EmailAddress = "andre@tennis.com", BirthDate = new DateTime(1970, 4, 29)},
            };
        }
    }
}
