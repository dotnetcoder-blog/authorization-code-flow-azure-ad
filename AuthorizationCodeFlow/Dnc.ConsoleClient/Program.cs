using Microsoft.Extensions.Configuration;
using System.Diagnostics;
using System.Net;
using System.Net.Http.Headers;
using System.Text.Json;

namespace Dnc.ConsoleClient
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            Config.Configuration = configuration;

            Console.WriteLine("Press Enter to begin");
            Console.ReadLine();


            
            var code = await GetAuthorizationCodeAsync();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("Press Enter to display the Authorization Code");
            Console.ReadLine();
            Console.WriteLine($"************Authorization Code***************");
            Console.WriteLine($"code: {code}");
            Console.ResetColor();


            var tokens = await ExchangeAutCodeForAccessToken(code);
            var accessToken = tokens["access_token"];
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Press Enter to display the Access Token");
            Console.ReadLine();
            Console.WriteLine($"************Access Token***************");
            Console.WriteLine($"{accessToken}");
            Console.ResetColor();

            var refreshToken = tokens["refresh_token"];
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("Press Enter to display the refresh Token");
            Console.ReadLine();
            Console.WriteLine($"************Refresh Token***************");
            Console.WriteLine($"{refreshToken}");
            Console.ResetColor();



            var employees = await GetWeatherForecast(accessToken);
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("Press Enter to call the secure web api");
            Console.ReadLine();
            Console.WriteLine($"************Response from the API***************");
            Console.WriteLine($"{employees}");

        }

        static async Task<string> GetAuthorizationCodeAsync()
        {
            var tenantId = Config.Configuration["AzureAd:TenantId"];
            var clientId = Config.Configuration["AzureAd:clientId"];
            var redirectUri = Config.Configuration["AzureAd:RedirectUri"];
            var instance = Config.Configuration["AzureAd:Instance"];
            var responseType = "code";
            var scopes = "openid";

            var codeVerifier = "MyRandomCodeVerifier"; // Just for test
            var hash = System.Security.Cryptography.SHA256.HashData(System.Text.Encoding.UTF8.GetBytes(codeVerifier));
            var codeChallenge = Convert.ToBase64String(hash)
                .TrimEnd('=')
                .Replace('+', '*')
                .Replace('/', '_');

            var codeEndpoint = $"{instance}/{tenantId}/oauth2/v2.0/authorize";
            var authorizationUrl = $"{codeEndpoint}?response_type={responseType}&client_id={clientId}&redirect_uri={redirectUri}&scope={scopes}&code_challenge={codeChallenge}&code_challenge_method=S256";

            Process.Start(new ProcessStartInfo
            {
                FileName = authorizationUrl,
                UseShellExecute = true  
            });


            using var listener = new HttpListener();
            listener.Prefixes.Add(redirectUri);
            try
            {
                listener.Start();
                var context = await listener.GetContextAsync();
                var authorizationCode = context.Request.QueryString["code"];
                listener.Stop();
                return authorizationCode;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }

        }
        static async Task<Dictionary<string, string>> ExchangeAutCodeForAccessToken(string authorizationCode)
        {
            var instance = Config.Configuration["AzureAd:Instance"];
            var tenantId = Config.Configuration["AzureAd:tenantId"];
            var applicationId = Config.Configuration["AzureAd:ApplicationId"];
            var clientId = Config.Configuration["AzureAd:clientId"];
            var clientSecret = Config.Configuration["AzureAd:ClientSecret"];
            var redirectUri = Config.Configuration["AzureAd:RedirectUri"];
            var scopes = Config.Configuration["AzureAd:Scopes"];

            //var scope = $"{applicationId}/{scopes}";
            var scope = $"{applicationId}/{scopes} offline_access";

            var grantType = "authorization_code";
            var codeVerifier = "MyRandomCodeVerifier";  // Just for test

            var tokenEndpoint = $"{instance}/{tenantId}/oauth2/v2.0/token";

            var parameters = new Dictionary<string, string>
            {
                { "grant_type", grantType },
                { "client_id", clientId },
                { "client_secret", clientSecret},
                { "code", authorizationCode },
                { "redirect_uri", redirectUri },
                { "scope", scope },
                { "code_verifier", codeVerifier }
            };

            
            var encodedContent = new FormUrlEncodedContent(parameters);

            using var client = new HttpClient();
            var response = await client.PostAsync(tokenEndpoint, encodedContent);
            var test = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
                throw new HttpRequestException($"Request failed with status code {response.StatusCode}");

            using var doc = JsonDocument.Parse(await response.Content.ReadAsStringAsync());

            return new Dictionary<string, string>
            {
                { 
                    "access_token", doc.RootElement.TryGetProperty("access_token", out var access_token) ? 
                    access_token.GetString() : throw new InvalidOperationException("access token not found.") 
                },
                { 
                    "refresh_token", doc.RootElement.TryGetProperty("refresh_token", out var refresh_token) ? 
                    refresh_token.GetString() : throw new InvalidOperationException("refresh token not found.") 
                }
            };

        }
        static async Task<string> GetWeatherForecast(string accessToken)
        {
            using var httpClient = new HttpClient { BaseAddress = new Uri("https://localhost:7236/") };
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            return await httpClient.GetStringAsync("WeatherForecast");
        }
    }

    public static class Config
    {
        public static IConfiguration Configuration { get; set; }
    }
}
