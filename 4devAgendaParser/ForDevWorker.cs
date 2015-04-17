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
        private readonly TrackParser trackParser = new TrackParser();

        private readonly TermTimeParser termTimeParser = new TermTimeParser();

        private readonly TermPointParser termPointParser = new TermPointParser();

        private readonly TrackAssembler trackAssembler = new TrackAssembler();

        private readonly TermPointFormatter termPointFormatter = new TermPointFormatter();

        public async Task Load()
        {
            var stopwatch = Stopwatch.StartNew();

            var page = await this.Load("http://4developers.org.pl/pl/agenda/agenda/");

            var tracks = this.trackParser.Parse(page).OrderBy(t => t.TrackId);
            var termTimes = this.termTimeParser.Parse(page);
            var termPoints = this.termPointParser.Parse(page).ToList(); 
            
            Console.WriteLine(
                "\nLoaded {0} items, by {1} miliseconds",
                termPoints.Count(),
                stopwatch.ElapsedMilliseconds);

            var assembled = this.trackAssembler.Assembly(termPoints, termTimes, tracks);
            var formattedData = this.termPointFormatter.Format(assembled);

            Console.WriteLine(
                "\nAssembled and formatted by {0} miliseconds",
                stopwatch.ElapsedMilliseconds);
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