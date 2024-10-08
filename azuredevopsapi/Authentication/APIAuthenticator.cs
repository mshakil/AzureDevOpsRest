using azuredevopsapi.Models.Response;
using RestSharp;
using RestSharp.Authenticators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Security.Authentication.ExtendedProtection;
using System.Text;
using System.Threading.Tasks;

namespace azuredevopsapi.Authentication
{
    public class APIAuthenticator : AuthenticatorBase
    {
        readonly string baseUrl;
        readonly string clientId;
        readonly string clientSecret;

        public APIAuthenticator(string baseUrl) : base("") { 
            this.baseUrl = baseUrl;
        }

        protected override async ValueTask<RestSharp.Parameter> GetAuthenticationParameter(string accessToken)
        {
            var token = string.IsNullOrEmpty(Token) ? await GetToken() : Token;
            return new HeaderParameter(KnownHeaders.Authorization, token);
        }





        private async Task<string> GetToken() {
            
            var options = new RestClientOptions(baseUrl)
            {
                Authenticator = new HttpBasicAuthenticator(clientId, clientSecret)
            };
            var client = new RestClient(options);

            var request = new RestRequest("oauth2/token").
                AddParameter("grant_type", "client_credentials");

            //var response = await client.PostAsync<TokenResponseRest>(request);
            return $"Hello";
        }
    }
}
