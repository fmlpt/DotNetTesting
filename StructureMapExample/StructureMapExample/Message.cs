using System;

namespace StructureMapExample
{
    public interface IMessage
    {
        string Hello(string person);
    }

    public class Message : IMessage
    {
        public string Hello(string person)
        {
            return string.Format("HELLO {0}!!!", person);
        }
    }
}