namespace _4devAgendaParser
{
    using System;

    using _4devAgendaParser.Logic;

    internal class Program
    {
        private static void Main()
        {
            var factory = new ForDevWorkerFactory();
            var worker = factory.Create();

            worker.Load();

            Console.WriteLine("Wpisz cokolwiek by zakończyć!");
            Console.Read();
        }
    }
}