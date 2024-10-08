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
        private string Organization_Name { get; set; }
        private string Project_Name { get; set; }

        string Api_Version { get; set; }
        private string Base_Url { get; set; }

        private string User_Name { get; set; }

        private string User_Pat { get; set; }

        readonly RestClient restClient;

        public ApiClient(string baseUrl, string username, string userPAT, string organizationName, string projectGuid, string apiVersion)
        {
            this.Base_Url = baseUrl;
            this.User_Name = username;
            this.User_Pat = userPAT;
            this.Organization_Name = organizationName;
            this.Project_Name = projectGuid;
            this.Api_Version = apiVersion;

            var options = new RestClientOptions(this.Base_Url)
            {
                Authenticator = new HttpBasicAuthenticator(this.User_Name, this.User_Pat)
            };
            restClient = new RestClient(options);
        }




        public void Dispose()
        {
            restClient?.Dispose();
            GC.SuppressFinalize(this);
        }

        private RestRequest AddDefaultUrlSegments(RestRequest request) {
            request.AddUrlSegment("organization", this.Organization_Name);
            request.AddUrlSegment("projectGuid", this.Project_Name);
            request.AddQueryParameter("api-version", this.Api_Version);

            return request;
        }

        public async Task<RestResponse> CreateWorkItem<T>(string payload, string workItemType)
        {

            restClient.AddDefaultHeader("Content-Type", "application/json-patch+json");

            var request = new RestRequest(Endpoints.CREATE_NEW_WI_ENDPOINT, Method.Post);

            AddDefaultUrlSegments(request).AddUrlSegment("workItemType", workItemType);

            request.AddBody(payload);

            return await restClient.ExecuteAsync<T>(request);
        }

        public async Task<RestResponse> GetWorkItem<T>(int workItemId)
        {
            restClient.AddDefaultHeader("Accept", "*/*");

            var request = new RestRequest(Endpoints.GET_WORKITEM_ENDPOINT, Method.Get);
            AddDefaultUrlSegments(request).AddUrlSegment("WorkItemId", workItemId);

            return await restClient.ExecuteAsync<T>(request);
        }


    }
}
