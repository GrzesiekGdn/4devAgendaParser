namespace _4devAgendaParser
{
    using System.Collections.Generic;
    using System.Linq;

    using _4devAgendaParser.Model;

    public class TrackAssembler
    {
        public IEnumerable<TermPoint> Assembly(
            IEnumerable<ParsedTermPoint> parsedTermPoints,
            IEnumerable<TermTime> termTimes,
            IEnumerable<Track> tracks)
        {
            var termTimesArray = termTimes.ToArray();

            var groupped =
                parsedTermPoints.GroupBy(p => p.TrackId)
                    .Select(
                        g =>
                        new
                            {
                                Track = tracks.First(t => t.TrackId == g.Key),
                                Items = g.Select((t, i) => new { TermIndex = i, t.Title, t.Speaker })
                            })
                    .ToList();

            var result =
                groupped.SelectMany(
                    g =>
                    g.Items.Select(
                        i =>
                        new TermPoint
                            {
                                Track = g.Track,
                                TermTime =
                                    i.TermIndex < termTimesArray.Count() ? termTimesArray[i.TermIndex] : null,
                                Title = i.Title,
                                Speaker = i.Speaker
                            }));

            return result;
        }
    }
}