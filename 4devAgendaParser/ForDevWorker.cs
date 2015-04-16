namespace _4devAgendaParser
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Net;
    using System.Text;
    using System.Threading.Tasks;

    using _4devAgendaParser.Model;
    using _4devAgendaParser.Parsers;

    public class ForDevWorker
    {
        private readonly TrackParser trackParser = new TrackParser();
        private readonly TermTimeParser termTimeParser = new TermTimeParser();
        private readonly TermPointParser termPointParser = new TermPointParser();

        public async Task Load()
        {
            var stopwatch = Stopwatch.StartNew();

            var page = await this.Load("http://4developers.org.pl/pl/agenda/agenda/");

            var tracks = this.trackParser.Parse(page).OrderBy(t => t.TrackId);

            foreach (var track in tracks)
            {
                Console.WriteLine("TrackId: {0}, Caption: {1}", track.TrackId, track.Caption);
            }
            Console.WriteLine("Tracks count: {0}", tracks.Count());

            var termTimes = this.termTimeParser.Parse(page);

            var termPoints = this.termPointParser.Parse(page).ToList();

            var groupped =
                termPoints
                    .GroupBy(p => p.Track.TrackId)
                    .Select(g => new { TrackName = tracks.First(t => t.TrackId == g.Key).Caption, Count = g.Count() })
                    .ToList();

            foreach (var group in groupped)
            {
                Console.WriteLine("Path: {0}, count: {1}", group.TrackName, group.Count);
            }

            Console.WriteLine("\nLoaded {0} items, by {1} miliseconds", termPoints.Count(), stopwatch.ElapsedMilliseconds);
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