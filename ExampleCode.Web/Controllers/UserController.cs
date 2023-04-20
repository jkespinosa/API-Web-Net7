using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using ExampleCode.Models;
using System.IO;

namespace ExampleCode.Web.Controllers
{
    public class UserController : Controller
    {
        private readonly IHttpClientFactory _httpClient;
        JsonSerializerOptions options = new JsonSerializerOptions() { PropertyNameCaseInsensitive = true };
        HttpClient client = new HttpClient();

        public UserController(IHttpClientFactory httpClient)
        {
            _httpClient = httpClient;
            client = _httpClient.CreateClient("ExampleCode");
        }

        public async Task<IActionResult> Index()
        {
            List<UserModel> userList = new List<UserModel>();


            var handler = new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = (request, cert, chain, errors) =>
                {
                    Console.WriteLine("SSL error skipped");
                    return true;
                }
            };

       

            try
            {

                HttpClient client = new HttpClient(handler);
                HttpResponseMessage response = await client.GetAsync("https://localhost:7090/api/GetUser");
                //var response = await client.GetAsync("GetUser");
                
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var result = JsonSerializer.Deserialize<APIResponse>(content,options);

                    return Ok(result);
                }


            }
            catch (Exception ex) { }
            return View();
        }
    }
}
