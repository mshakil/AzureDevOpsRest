using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace azuredevopsapi
{
    internal interface IApiClient
    {
        Task<RestResponse>CreateUser<T>(T payload) where T : class;
        Task<RestResponse>UpdateUser<T>(T payload, string id) where T : class;

        Task<RestResponse> DeleteUser<T>(string id);
        Task<RestResponse> GetUser<T>(string userId);
        Task<RestResponse> GetListOfUsers<T>(int pageNumber);

        Task<RestResponse> CreateWorkItem<T>(string payload, string workItemType);


    }
}
