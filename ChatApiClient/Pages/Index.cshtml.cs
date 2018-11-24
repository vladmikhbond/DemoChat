using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace ChatApiClient.Pages
{
    public class IndexModel : PageModel
    {        
        private readonly HttpClient _http;

        public IndexModel(HttpClient http)
        {
            _http = http;
        }

        public async Task OnGet()
        {
            KeyValuePair<string, string>[] formData = {
                new KeyValuePair<string, string>("username", "aaa"),
                new KeyValuePair<string, string>("password", "bbb") };

            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, "/token");
            request.Content = new FormUrlEncodedContent(formData);

            HttpResponseMessage response = await _http.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                string str = await response.Content.ReadAsStringAsync();
                var obj = (Newtonsoft.Json.Linq.JObject)JsonConvert.DeserializeObject(str);
                string token = obj["access_token"].ToString();
                TempData["token"] = token;
                ViewData["token"] = token;
            }
            else
            {
                ViewData["token"] = $" response status code: {response.StatusCode}";
            }         
        }
    }
}
