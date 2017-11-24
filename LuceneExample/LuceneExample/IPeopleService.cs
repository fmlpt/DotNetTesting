using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuceneExample
{
    public interface IPeopleService
    {
        IEnumerable<Person> GetAll();
    }
}
