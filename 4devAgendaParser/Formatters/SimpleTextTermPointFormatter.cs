namespace _4devAgendaParser.Formatters
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using _4devAgendaParser.Model;

    public class SimpleTextTermPointFormatter : ITermPointFormatter
    {
        public string Format(IList<TermPoint> termPoints, IList<TermTime> termTimes, IList<Track> tracks)
        {
            var stringBuilder = new StringBuilder();

            var groupped = termPoints.GroupBy(p => p.Track.Caption);

            foreach (var group in groupped)
            {
                stringBuilder.AppendFormat("Path: {0}, count: {1}\n", group.Key, group.Count());

                foreach (var termPoint in group)
                {
                    stringBuilder.AppendFormat(
                        "\tTime: {0:HH:mm} - {1:HH:mm}, path: {2}, title: {3}\n",
                        termPoint.TermTime.StartTime,
                        termPoint.TermTime.EndTime,
                        termPoint.Track.Caption,
                        termPoint.Title);
                }
            }

            return stringBuilder.ToString();
        }
    }
}