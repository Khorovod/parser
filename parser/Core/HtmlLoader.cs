using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Threading;

namespace parser.Core
{
    class HtmlLoader
    {
        readonly HttpClient _client;
        string _url;
        public HtmlLoader(IParserSettings settings)
        {
            _client = new HttpClient();
            _url = settings.BaseUrl + settings.PostFix;
        }

        public async Task<string> GetPageSourseByPageId(int id, CancellationToken token)
        {
            string res = null;
            var pg = await _client.GetAsync(_url + id.ToString(), token);
            if(pg != null && pg.StatusCode == HttpStatusCode.OK)
            {
                res = await pg.Content.ReadAsStringAsync();
            }
            return res;
        }
    }
}
