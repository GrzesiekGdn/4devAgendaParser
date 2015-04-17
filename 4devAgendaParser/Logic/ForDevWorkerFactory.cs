namespace _4devAgendaParser.Logic
{
    using _4devAgendaParser.Parsers;

    /// <summary>
    /// Pure DI :)
    /// </summary>
    public class ForDevWorkerFactory
    {
        public ForDevWorker Create()
        {
            var loader = new DataLoader("http://4developers.org.pl/pl/agenda/agenda/");

            return new ForDevWorker(
                loader,
                new TrackParser(),
                new TermTimeParser(),
                new TermPointParser(),
                new TrackAssembler(),
                new SimpleTextTermPointFormatter());
        }
    }
}