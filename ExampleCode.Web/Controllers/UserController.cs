using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using ExampleCode.Models;
using System.IO;
using System.Text;
using static System.Net.Mime.MediaTypeNames;

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
                    var result = JsonSerializer.Deserialize<APIResponse>(content, options);

                    if (result.isExitoso == true)
                        userList = JsonSerializer.Deserialize<List<UserModel>>(result.result.ToString(), options);

                    return View(userList);

                }
            }

            catch (Exception ex) { }
            return View();
        }


        public IActionResult Create()
        {
            return View();
        }

        public async Task<IActionResult> Save(UserModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var handler = new HttpClientHandler
                    {
                        ServerCertificateCustomValidationCallback = (request, cert, chain, errors) =>
                        {
                            Console.WriteLine("SSL error skipped");
                            return true;
                        }
                    };

                    HttpClient client = new HttpClient(handler);


                    StringContent userJson = Serializar(model);

                    var response = await client.PostAsync("https://localhost:7090/api/AddUser", userJson);

                }
                catch { View("Error"); }

            }
            return RedirectToAction("Index");

        }

        [HttpGet]

        public async Task<IActionResult> Edit(int id)
        {
            UserModel userM = new();

            try {
                var handler = new HttpClientHandler
                {
                    ServerCertificateCustomValidationCallback = (request, cert, chain, errors) =>
                    {
                        Console.WriteLine("SSL error skipped");
                        return true;
                    }
                };

                HttpClient client = new HttpClient(handler);


                var response = await client.GetAsync(string.Format("https://localhost:7090/api/GetUsertById/{0}", id));
                if (response.IsSuccessStatusCode)
                {

                    var content = await response.Content.ReadAsStringAsync();

                    var result = JsonSerializer.Deserialize<APIResponse>(content, options);

                    if (result.isExitoso == true)
                        userM = JsonSerializer.Deserialize<UserModel>(result.result.ToString(), options);

                }
            }
            catch(Exception ex) { return View("Error"); }

            return View(userM);

        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, UserModel model)
        {
            UserModel userM = new();         

            try
            {

                if (ModelState.IsValid)
                {
                    var handler = new HttpClientHandler
                    {
                        ServerCertificateCustomValidationCallback = (request, cert, chain, errors) =>
                        {
                            Console.WriteLine("SSL error skipped");
                            return true;
                        }
                    };

                    HttpClient client = new HttpClient(handler);

                    StringContent userJson = Serializar(model);
                    var response = await client.PutAsync(string.Format("https://localhost:7090/api/PutUser/{0}", id), userJson);



                }
            }
            catch (Exception ex) { return View("Error"); }

            return RedirectToAction("Index");
        }

        //public async Task<IActionResult> Delete(int id)
        //{
        //}

        public static StringContent Serializar(UserModel userM)
        {
            return new StringContent(
                JsonSerializer.Serialize(userM),
                Encoding.UTF8,
                Application.Json);
        }
    }
}
