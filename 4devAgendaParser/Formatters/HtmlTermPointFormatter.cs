namespace _4devAgendaParser.Formatters
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using _4devAgendaParser.Model;

    public class HtmlTermPointFormatter : ITermPointFormatter
    {
        private readonly string baseAddress;

        public HtmlTermPointFormatter(string baseAddress)
        {
            this.baseAddress = baseAddress;
        }

        public string Format(IList<TermPoint> termPoints, IList<TermTime> termTimes, IList<Track> tracks)
        {
            var builder = new StringBuilder();

            this.AppendDocumentHeader(builder);
            this.AppendTable(builder, termPoints, termTimes, tracks);
            this.AppendDocumentFooter(builder);

            return builder.ToString();
        }

        private void AppendDocumentHeader(StringBuilder builder)
        {
            builder.AppendLine("<!DOCTYPE html>");
            builder.AppendLine("<html>");
            builder.AppendLine("<head>");
            builder.AppendLine("<meta charset=\"UTF-8\">");
            builder.AppendLine("<title>Agenda 4developers 2015</title>");

            builder.AppendLine("<style>");
            builder.AppendLine("table {border-collapse: collapse;}");
            builder.AppendLine("table, th, td {border: 1px solid lightgray;padding: 5px;}");
            builder.AppendLine("tbody tr th {font-size:75%}");
            builder.AppendLine("</style>");

            builder.AppendLine("</head>");
            builder.AppendLine("<body>");

            builder.AppendLine("<h2>Agenda 4developers 2015</h2>");
        }

        private void AppendDocumentFooter(StringBuilder builder)
        {
            builder.AppendLine("</body>");
            builder.AppendLine("</html>");
        }

        private void AppendTable(
            StringBuilder builder,
            IList<TermPoint> termPoints,
            IEnumerable<TermTime> termTimes,
            IList<Track> tracks)
        {
            builder.AppendLine("<table>");
            this.AppendTableHeaders(builder, tracks);
            builder.AppendLine("<tbody>");

            foreach (var termTime in termTimes)
            {
                var termPointsInTime = termPoints.Where(p => p.TermTime == termTime).ToList();
                this.AppendOneRow(builder, termTime, tracks, termPointsInTime);
            }

            builder.AppendLine("</tbody>");
            builder.AppendLine("</table>");
        }

        private void AppendTableHeaders(StringBuilder builder, IEnumerable<Track> tracks)
        {
            builder.AppendLine("<thead>");
            builder.AppendLine("<tr>");

            builder.AppendLine("<th>Termin</th>");

            foreach (var track in tracks)
            {
                builder.AppendFormat("<th>{0}</th>\n", track.Caption);
            }

            builder.AppendLine("</tr>");
            builder.AppendLine("</thead>");
        }

        private void AppendOneRow(
            StringBuilder builder,
            TermTime termTime,
            IEnumerable<Track> tracks,
            IList<TermPoint> points)
        {
            builder.AppendLine("<tr>");
            builder.AppendFormat("<th>{0:HH:mm} - {1:HH:mm}</th>\n", termTime.StartTime, termTime.EndTime);

            foreach (var track in tracks)
            {
                var currentPoint = points.FirstOrDefault(p => p.Track == track);
                if (currentPoint != null)
                {
                    var currentTitle = currentPoint.Title;

                    var currentLink = currentPoint.TitleLink.StartsWith("http")
                                          ? currentPoint.TitleLink
                                          : this.baseAddress + currentPoint.TitleLink;

                    builder.AppendFormat("<th><a href=\"{0}\">{1}</a></th>\n", currentLink, currentTitle);
                }
                else
                {
                    builder.AppendLine("<th></th>");
                }
            }

            builder.AppendLine("</tr>");
        }
    }
}