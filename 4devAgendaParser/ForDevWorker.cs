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

            foreach (var termTime in termTimes)
            {
                Console.WriteLine("Start: {0:HH:mm}, {1:HH:mm}", termTime.StartTime, termTime.EndTime);
            }

            var termPoints = this.termPointParser.Parse(page).Where(p => p.Track.TrackId == 5).ToList();

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