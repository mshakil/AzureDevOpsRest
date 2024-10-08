using azuredevopsapi.Authentication;
using RestSharp;
using RestSharp.Authenticators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace azuredevopsapi.ApiUtilities
{
    public class ApiClient : IApiClient, IDisposable
    {
        string organizationName { get; set; }
        string projectName { get; set; }
        string workItemType { get; set; }
        private string BASE_URL { get; set; }

        private string user_name { get; set; }

        private string user_pat { get; set; }

        readonly RestClient restClient;

        public ApiClient(string baseUrl, string username, string userPAT, string organizationName, string projectGuid)
        {
            BASE_URL = baseUrl;
            user_name = username;
            user_pat = userPAT;
            this.organizationName = organizationName;
            projectName = projectGuid;

            var options = new RestClientOptions(BASE_URL)
            {
                Authenticator = new HttpBasicAuthenticator(user_name, user_pat)
            };
            restClient = new RestClient(options);
        }




        public void Dispose()
        {
            restClient?.Dispose();
            GC.SuppressFinalize(this);
        }

        public async Task<RestResponse> CreateWorkItem<T>(string payload, string workItemType)
        {

            restClient.AddDefaultHeader("Content-Type", "application/json-patch+json");

            var request = new RestRequest(Endpoints.CREATE_NEW_WI_ENDPOINT, Method.Post);
            request.AddUrlSegment("organization", organizationName);
            request.AddUrlSegment("projectGuid", projectName);
            request.AddUrlSegment("workItemType", workItemType);

            request.AddBody(payload);

            return await restClient.ExecuteAsync<T>(request);
        }
    }
}
