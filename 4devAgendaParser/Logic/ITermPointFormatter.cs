namespace _4devAgendaParser.Logic
{
    using System.Collections.Generic;

    using _4devAgendaParser.Model;

    public interface ITermPointFormatter
    {
        string Format(IEnumerable<TermPoint> termPoints);
    }
}