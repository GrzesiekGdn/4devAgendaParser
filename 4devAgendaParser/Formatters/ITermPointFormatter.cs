namespace _4devAgendaParser.Formatters
{
    using System.Collections.Generic;

    using _4devAgendaParser.Model;

    public interface ITermPointFormatter
    {
        string Format(IList<TermPoint> termPoints, IList<TermTime> termTimes, IList<Track> tracks);
    }
}