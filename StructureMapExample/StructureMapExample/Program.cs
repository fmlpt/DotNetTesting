using System;
using StructureMap;

namespace StructureMapExample
{
    class Program
    {
        static void Main(string[] args)
        {
            var container = Container.For<ConsoleRegistry>();

            var app = container.GetInstance<Application>();
            app.Run();
            
            var myPerson = container.GetInstance<Person>(); // getting a Person will get all dependencies specified in its constructor
            myPerson.Name = "Steve";
            myPerson.Age = 38;
            Console.WriteLine("Person:");
            Console.WriteLine(myPerson);
            
            Console.ReadLine();
        }
    }

    public class ConsoleRegistry : Registry
    {
        public ConsoleRegistry()
        {
            Scan(scan =>
            {
                scan.TheCallingAssembly();
                scan.WithDefaultConventions();
            });
            // requires explicit registration; doesn't follow convention
            For<ILog>().Use<ConsoleLogger>();
            For<IPersonFormatter>().Use<SimplePersonFormatter>();
            For<IPersonFormatter>().Use<CapsPersonFormatter>();
        }
    }

    public interface IWriter
    {
        void WriteLine(string output);
    }

    // will be automatically wired up by default convention
    public class Writer : IWriter
    {
        public void WriteLine(string output)
        {
            Console.WriteLine(output);
        }
    }

    public interface ILog
    {
        void Info(string message);
    }

    public class ConsoleLogger : ILog
    {
        public void Info(string message)
        {
            var color = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(message);
            Console.ForegroundColor = color;
        }
    }

    public class Application
    {
        private readonly IWriter _writer;
        private readonly ILog _logger;

        public Application(IWriter writer, ILog logger)
        {
            _writer = writer;
            _logger = logger;
        }

        public void Run()
        {
            _logger.Info(nameof(Application) + " started.");

            _writer.WriteLine("Hello World!");

            _logger.Info(nameof(Application) + " finished.");
        }
    }
}