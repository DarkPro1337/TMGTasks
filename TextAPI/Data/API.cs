using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace TextAPI.Data
{
    public class API
    {
        /// <summary>
        /// The HTTP client.
        /// </summary>
        public static HttpClient _client;

        // API Headers declaration.
        // Used to connect remote Azure API.
        public API()
        {
            _client = new HttpClient();
            string key = "TMG-Api-Key";
            string value = "0J/RgNC40LLQtdGC0LjQutC4IQ==";
            _client.DefaultRequestHeaders.Add(key, value);
        }

        /// <summary>
        /// Getting text by ID from remote Azure server.
        /// </summary>
        /// <param name="id">ID for a string</param>
        /// <returns>Returns deserialazed string from JSON</returns>
        public async Task<List<Text>> GetTextByIdAsync(List<int> id)
        {
            string path = @"http://tmgwebtest.azurewebsites.net/api/textstrings/" + id;
            List<Text> result = new List<Text>();
            for (int i = 0; i < id.Count; i++)
            {
                path = @"http://tmgwebtest.azurewebsites.net/api/textstrings/" + id[i];
                HttpResponseMessage response = await _client.GetAsync(path);
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
