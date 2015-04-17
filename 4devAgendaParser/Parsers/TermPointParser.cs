namespace _4devAgendaParser.Parsers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;
    using System.Web;

    using _4devAgendaParser.Model;

    public class TermPointParser
    {
        private readonly Regex regex =
            new Regex(
                "<div class=\"agenda-term-point\\s+track-(?<TrackId>\\d+)"
                + "[.\\s\\w\"=\\-><:;]*?<span class=\"point-title\"><a href=\"(?<TitleLink>.*)\">"
                + "(?<Title>.*)</a></span>(<br/>)*(\\s*<span class=\"point-speaker\">\\s*"
                + "(?<Speaker>.*)[\\s()\\w]*</span><br/>)*\\s*",
                RegexOptions.Compiled | RegexOptions.Multiline);

        public IEnumerable<ParsedTermPoint> Parse(string page)
        {
            var matches = this.regex.Matches(page);

            return
                (matches.Cast<Match>()
                    .Select(
                        match =>
                        new ParsedTermPoint
                            {
                                TrackId = Int32.Parse(match.Groups["TrackId"].Value),
                                Title = HttpUtility.HtmlDecode(match.Groups["Title"].Value),
                                Speaker = match.Groups["Speaker"].Value,
                                TitleLink = match.Groups["TitleLink"].Value
                            })).ToList();
        }
    }
}