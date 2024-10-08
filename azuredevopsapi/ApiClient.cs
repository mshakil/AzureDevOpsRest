using azuredevopsapi.Authentication;
using RestSharp;
using RestSharp.Authenticators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace azuredevopsapi
{
    public class ApiClient : IApiClient, IDisposable
    {
        string organizationName { get; set; }
        string projectName { get; set; }
        string workItemType { get; set; }
        private string BASE_URL { get; set; }

        private string user_name {get; set;}

        private string user_pat { get; set; }

        readonly RestClient restClient;
        
        public ApiClient(string baseUrl, string username, string userPAT, string organizationName, string projectGuid) {
            this.BASE_URL = baseUrl;
            this.user_name = username;
            this.user_pat = userPAT;
            this.organizationName = organizationName;
            this.projectName = projectGuid;

            var options = new RestClientOptions(BASE_URL)
            {
                Authenticator = new HttpBasicAuthenticator(user_name, user_pat)
            };
            restClient = new RestClient(options);
        }
        public async Task<RestResponse> CreateUser<T>(T payload) where T : class
        {
            var request = new RestRequest(Endpoints.CREATE_USER, Method.Post);
            request.AddBody(payload);

            return await restClient.ExecuteAsync<T>(request);
        }

        public async Task<RestResponse> DeleteUser<T>(string id)
        {
            var request = new RestRequest(Endpoints.DELETE_USER, Method.Delete);
            request.AddUrlSegment(id, id);

            return await restClient.ExecuteAsync(request);
        }

        public void Dispose()
        {
            restClient?.Dispose();
            GC.SuppressFinalize(this);
        }

        public async Task<RestResponse> GetListOfUsers<T>(int pageNumber)
        {
            var request = new RestRequest(Endpoints.GET_LIST_OF_USERS, Method.Get);
            request.AddQueryParameter("page", pageNumber);

            return await restClient.ExecuteAsync(request);
        }

        public async Task<RestResponse> GetUser<T>(string id)
        {
            var request = new RestRequest(Endpoints.GET_SINGLE_USER, Method.Get);
            request.AddUrlSegment(id, id);

            return await restClient.ExecuteAsync(request);
        }

        public async Task<RestResponse> UpdateUser<T>(T payload, string id) where T : class
        {
            var request = new RestRequest(Endpoints.UPDATE_USER, Method.Post);
            request.AddUrlSegment(id, id);
            request.AddBody(payload);

            return await restClient.ExecuteAsync<T>(request);
        }

        public async Task<RestResponse> CreateWorkItem<T>(string payload, string workItemType) {

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
