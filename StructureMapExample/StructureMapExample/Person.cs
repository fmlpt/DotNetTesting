namespace StructureMapExample
{
    public class Person
    {
        private readonly IPersonFormatter _personFormatter;

        public Person(IPersonFormatter personFormatter)
        {
            _personFormatter = personFormatter;
        }

        public string Name { get; set; }
        public int Age { get; set; }

        public override string ToString()
        {
            return _personFormatter.Format(this);
        }
    }
}