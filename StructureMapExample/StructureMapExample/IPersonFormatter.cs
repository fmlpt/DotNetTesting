namespace StructureMapExample
{
    public interface IPersonFormatter
    {
        string Format(Person person);
    }

    public class SimplePersonFormatter : IPersonFormatter
    {
        public string Format(Person person)
        {
            return string.Format("{0}, {1} years old.", person.Name, person.Age);
        }
    }

    public class CapsPersonFormatter : IPersonFormatter
    {
        public string Format(Person person)
        {
            return string.Format("{0}, {1} years old.", person.Name, person.Age).ToUpper();
        }
    }
}