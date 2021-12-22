using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace TextStrings.Models
{
    public class API
    {
        public static HttpClient _client;

        public API()
        {
            _client = new HttpClient();
            string key = "TMG-Api-Key";
            string value = "0J/RgNC40LLQtdGC0LjQutC4IQ==";
            _client.DefaultRequestHeaders.Add(key, value);
        }

        public async Task<List<Text>> GetTextByIdAsync(List<int> id)
        {
            string path = @"http://tmgwebtest.azurewebsites.net/api/textstrings/" + id;
            List<Text> result = new List<Text>();
            for (int i = 0; i < id.Count; i++)
            {
                path = @"http://tmgwebtest.azurewebsites.net/api/textstrings/" + id[i];
                HttpResponseMessage response = await _client.GetAsync(path);
                // response.Content.Headers.Fir
                if (response.IsSuccessStatusCode)
                {
                    var requestResult = _client.GetStreamAsync(path);
                    result.Add(JsonSerializer.DeserializeAsync<Text>(await requestResult).Result);
                }
            }
            return result;
        }
    }
}
