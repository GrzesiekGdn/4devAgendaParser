namespace _4devAgendaParser
{
    using System;
    using System.Diagnostics;
    using System.Net;
    using System.Text;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;
    using System.Web;

    public class ForDevWorker
    {
        private readonly Regex regex =
            new Regex(
                "<div class=\"agenda-term-point\\s+track-(?<TrackId>\\d+)"
                + "[.\\s\\w\"=\\-><]*?<span class=\"point-title\"><a href=\"(?<TitleLink>.*)\">"
                + "(?<Title>.*)</a></span>(<br/>)*(\\s*<span class=\"point-speaker\">\\s*"
                + "(?<Speaker>.*)[\\s()\\w]*</span><br/>)*\\s*",
                RegexOptions.Compiled | RegexOptions.Multiline);

        public async Task Load()
        {
            var stopwatch = Stopwatch.StartNew();

            var page = await this.Load("http://4developers.org.pl/pl/agenda/agenda/");
            var matches = this.regex.Matches(page);

            int count = 0;

            foreach (Match match in matches)
            {
                Console.WriteLine(
                    "TrackId: {0}\n\tTitle: {1}\n\tSpeaker: {2}\n",
                    match.Groups["TrackId"].Value,
                    HttpUtility.HtmlDecode(match.Groups["Title"].Value),
                    match.Groups["Speaker"].Value);

                count++;
            }

            Console.WriteLine("\nLoaded {0} items, by {1} miliseconds", count, stopwatch.ElapsedMilliseconds);
        }

        private async Task<string> Load(string url)
        {
            using (var client = new WebClient())
            {
                var data = await client.DownloadDataTaskAsync(url);
                var result = Encoding.UTF8.GetString(data);
                return result;
            }
        }
    }
}