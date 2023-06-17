namespace MarketCore.Utills
{
    public static class HttpUtils
    {

        /// <summary>
        /// Receive a request from the server
        /// </summary>
        public static async Task<string> GetResponseAsync(string url)
        {
            string responseBody = string.Empty;
            try
            {
                using var client = new HttpClient();

                HttpResponseMessage response = await client.GetAsync(url);
                response.EnsureSuccessStatusCode();

                using HttpContent content = response.Content;
                responseBody = await response.Content.ReadAsStringAsync();
            }
            catch
            {

            }
            return responseBody;
        }
    }
}