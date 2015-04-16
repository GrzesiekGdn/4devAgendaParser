namespace _4devAgendaParser.Parsers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;

    using _4devAgendaParser.Model;

    public class TrackParser
    {
        private readonly Regex regex = new Regex(
            "<option value=\"track-(?<TrackId>\\d+)\">(?<Caption>.*)</option>",
            RegexOptions.Compiled | RegexOptions.Multiline);

        public IEnumerable<Track> Parse(string page)
        {
            var matches = this.regex.Matches(page);

            return
                (matches.Cast<Match>()
                    .Select(
                        match =>
                        new Track
                            {
                                TrackId = Int32.Parse(match.Groups["TrackId"].Value),
                                Caption = match.Groups["Caption"].Value
                            })).ToList();
        }
    }
}