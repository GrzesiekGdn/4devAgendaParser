namespace _4devAgendaParser
{
    using System;
    using System.Diagnostics;
    using System.Linq;
    using System.Net;
    using System.Text;
    using System.Threading.Tasks;

    using _4devAgendaParser.Parsers;

    public class ForDevWorker
    {
        private readonly TermPointParser termPointParser = new TermPointParser();

        public async Task Load()
        {
            var stopwatch = Stopwatch.StartNew();

            var page = await this.Load("http://4developers.org.pl/pl/agenda/agenda/");
            var termPoints = this.termPointParser.Parse(page).ToList();

            foreach (var termPoint in termPoints)
            {
                Console.WriteLine("Title: {0}", termPoint.Title);
            }

            var count = termPoints.Count();
            Console.WriteLine("\nLoaded {0} items, by {1} miliseconds", count, stopwatch.ElapsedMilliseconds);
        }

        private async Task<string> Load(string url)
        {
            using (var client = new WebClient())
            {
                var data = await client.DownloadDataTaskAsync(url);
                var result = Encoding.UTF8.GetString(data);
                return result;
            }
        }
    }
}