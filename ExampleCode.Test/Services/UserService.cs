using ExampleCode.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExampleCode.Test.Services
{
    public class UserService
    {

        public string BaseUrl = "https://localhost:7090/api/";
        public HttpClient client;

        public UserService()
        {
            client = new HttpClient();
        }

        public async Task<UserModelTest> GetUsers()
        {
            UserModelTest user = new UserModelTest();
            client.BaseAddress = new Uri(BaseUrl);
            client.DefaultRequestHeaders.Add("Accept", "application/json");
            var endPoint = "GetUser";

            HttpResponseMessage httpResponse = await client.GetAsync(endPoint);
            httpResponse.EnsureSuccessStatusCode();

            user = await httpResponse.Content.ReadAsAsync<UserModelTest>();

            return user;
        }

    }
}
