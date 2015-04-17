namespace _4devAgendaParser.Logic
{
    using System.Net;
    using System.Text;
    using System.Threading.Tasks;

    public class DataLoader
    {
        private readonly string uri;

        public DataLoader(string uri)
        {
            this.uri = uri;
        }

        public async Task<string> Load()
        {
            return await this.Load(this.uri);
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