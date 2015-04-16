namespace _4devAgendaParser
{
    using System;

    internal class Program
    {
        private static void Main()
        {
            var worker = new ForDevWorker();
            worker.Load().Wait();

            Console.WriteLine("Wpisz cokolwiek by zakończyć!");
            Console.Read();
        }
    }
}