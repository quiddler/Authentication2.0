using System;
using System.Net.Http;
using System.Threading.Tasks;
using IdentityModel.Client;
using Newtonsoft.Json.Linq;

namespace ConsoleClient
{
    public class Program
    {
        private const string Sts = "http://localhost:5000/";
        private const string Api1IdentityEndpoint = "http://localhost:5001/api/identity";
        private const string Api1ClientId = "ConsoleClient";
        private const string Api1ClientSecret = "secret";
        private const string Api1RequestedScopes = "api1";

        public static void Main(string[] args) => MainAsync().GetAwaiter().GetResult();

        private static async Task MainAsync()
        {
            var discoveryPointMetadata = await GetDiscoveryPointMetadata();

            var tokenResponse = await GetToken(discoveryPointMetadata?.TokenEndpoint);

            Console.WriteLine(tokenResponse?.Json);

            var identity = await GetApi1Identity(tokenResponse?.AccessToken);

            Console.WriteLine(JArray.Parse(identity ?? "[]"));

            Console.ReadLine();
        }

       public static async Task<DiscoveryResponse> GetDiscoveryPointMetadata()
        {
            var discoveryPointMetadata = await new HttpClient().GetDiscoveryDocumentAsync(Sts);

            if (discoveryPointMetadata.IsError)
            {
                Console.WriteLine(discoveryPointMetadata.Error);
                return null;
            }

            return discoveryPointMetadata;
        }

        public static async Task<TokenResponse> GetToken(string address)
        {
            var tokenRequest = new ClientCredentialsTokenRequest
            {
                ClientId = Api1ClientId,
                Address = address,
                ClientSecret = Api1ClientSecret,
                Scope = Api1RequestedScopes
            };

            var tokenResponse = await new HttpClient().RequestClientCredentialsTokenAsync(tokenRequest);

            if (tokenResponse.IsError)
            {
                Console.WriteLine(tokenResponse.ErrorDescription);
                return null;
            }

            return tokenResponse;
        }

        private static async Task<string> GetApi1Identity(string accessToken)
        {
            var client = new HttpClient();
            client.SetBearerToken(accessToken);

            var response = await client.GetAsync(Api1IdentityEndpoint);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return content;
            }

            return null;
        }
    }
}
