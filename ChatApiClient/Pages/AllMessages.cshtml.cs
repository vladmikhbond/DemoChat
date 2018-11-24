using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace ChatApiClient.Pages
{
public class AllMessagesModel : PageModel
{
    private readonly HttpClient _http;

    public AllMessagesModel(HttpClient http)
    {
        _http = http;
    }

    public async Task OnGet()
    {
        HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, "/api/messages");
        request.Headers.Add("Authorization", "Bearer " + TempData["token"]);

        HttpResponseMessage response = await _http.SendAsync(request);
        ViewData["all"] = await response.Content.ReadAsStringAsync();
    }
}
}