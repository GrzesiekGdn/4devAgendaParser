namespace _4devAgendaParser
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using _4devAgendaParser.Model;

    public class TermPointFormatter
    {
        public string Format(IEnumerable<TermPoint> termPoints)
        {
            var groupped =
                termPoints.GroupBy(p => p.Track.Caption)
                    .Select(g => new { TrackName = g.Key, Count = g.Count() })
                    .ToList();

            foreach (var group in groupped)
            {
                Console.WriteLine("Path: {0}, count: {1}", group.TrackName, group.Count);
            }

            foreach (var termPoint in termPoints)
            {
                Console.WriteLine(
                    "Time: {0:HH:mm} - {1:HH:mm}, path: {2}, title: {3}",
                    termPoint.TermTime.StartTime,
                    termPoint.TermTime.EndTime,
                    termPoint.Track.Caption,
                    termPoint.Title);
            }

            return null;
        }
    }
}