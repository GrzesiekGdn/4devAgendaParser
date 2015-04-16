namespace _4devAgendaParser.Parsers
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Text.RegularExpressions;

    using _4devAgendaParser.Model;

    public class TermTimeParser
    {
        private readonly Regex regex =
            new Regex(
                "<span class=\"start-time\">(?<StartTime>\\d\\d\\:\\d\\d)</span><span class=\"divide-time\"> - "
                + "</span><span class=\"end-time\">(?<EndTime>\\d\\d\\:\\d\\d)</span>",
                RegexOptions.Compiled | RegexOptions.Multiline);

        public IEnumerable<TermTime> Parse(string page)
        {
            var matches = this.regex.Matches(page);

            return
                (matches.Cast<Match>()
                    .Select(
                        match =>
                        new TermTime
                            {
                                StartTime =
                                    DateTime.ParseExact(
                                        match.Groups["StartTime"].Value,
                                        "HH:mm",
                                        DateTimeFormatInfo.InvariantInfo),
                                EndTime =
                                    DateTime.ParseExact(
                                        match.Groups["EndTime"].Value,
                                        "HH:mm",
                                        DateTimeFormatInfo.InvariantInfo)
                            })).ToList();
        }
    }
}