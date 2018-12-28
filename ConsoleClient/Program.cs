using System;
using System.Net.Http;
using System.Threading.Tasks;
using IdentityModel.Client;
using Newtonsoft.Json.Linq;

namespace ConsoleClient
{
    public class Program
    {
        public static void Main(string[] args) => MainAsync().GetAwaiter().GetResult();

        private static async Task MainAsync()
        {
            var discoveryPointMetadata = await new HttpClient().GetDiscoveryDocumentAsync("http://localhost:5000/");

            if (discoveryPointMetadata.IsError)
            {
                Console.WriteLine(discoveryPointMetadata.Error);
                return;
            }

            var tokenRequest = new ClientCredentialsTokenRequest
            {
                ClientId = "clientId",
                Address = discoveryPointMetadata.TokenEndpoint,
                ClientSecret = "secret",
                Scope = "api1"
            };

            var tokenResponse = await new HttpClient().RequestClientCredentialsTokenAsync(tokenRequest);

            if (tokenResponse.IsError)
            {
                Console.WriteLine(tokenResponse.ErrorDescription);
                return;
            }

            Console.WriteLine(tokenResponse.Json);

            var client = new HttpClient();
            client.SetBearerToken(tokenResponse.AccessToken);

            var response = await client.GetAsync("http://localhost:5001/api/identity");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                Console.WriteLine(JArray.Parse(content));
            }
            else
            {
                Console.WriteLine(response.StatusCode);
            }

            Console.ReadLine();
        }
    }
}
