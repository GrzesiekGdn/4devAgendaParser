namespace _4devAgendaParser.Logic
{
    using System;
    using System.Diagnostics;
    using System.Linq;
    using System.Threading.Tasks;

    using _4devAgendaParser.Parsers;

    public class ForDevWorker
    {
        private readonly DataLoader dataLoader;

        private readonly TrackParser trackParser;

        private readonly TermTimeParser termTimeParser;

        private readonly TermPointParser termPointParser;

        private readonly TrackAssembler trackAssembler;

        private readonly ITermPointFormatter termPointFormatter;

        public ForDevWorker(
            DataLoader dataLoader,
            TrackParser trackParser,
            TermTimeParser termTimeParser,
            TermPointParser termPointParser,
            TrackAssembler trackAssembler,
            ITermPointFormatter termPointFormatter)
        {
            this.trackParser = trackParser;
            this.termTimeParser = termTimeParser;
            this.termPointParser = termPointParser;
            this.trackAssembler = trackAssembler;
            this.termPointFormatter = termPointFormatter;
            this.dataLoader = dataLoader;
        }

        public async Task Load()
        {
            var stopwatch = Stopwatch.StartNew();

            var page = await this.dataLoader.Load();

            var tracks = this.trackParser.Parse(page).OrderBy(t => t.TrackId);
            var termTimes = this.termTimeParser.Parse(page);
            var termPoints = this.termPointParser.Parse(page).ToList();

            Console.WriteLine(
                "\nLoaded {0} items, by {1} miliseconds",
                termPoints.Count(),
                stopwatch.ElapsedMilliseconds);

            var assembled = this.trackAssembler.Assembly(termPoints, termTimes, tracks);
            var formattedData = this.termPointFormatter.Format(assembled);

            Console.WriteLine(formattedData);

            Console.WriteLine("\nAssembled and formatted by {0} miliseconds", stopwatch.ElapsedMilliseconds);
        }
    }
}