using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace azuredevopsapi.ApiUtilities
{
    internal interface IApiClient
    {
        Task<RestResponse> CreateWorkItem<T>(string payload, string workItemType);

        Task<RestResponse> GetWorkItem<T>(int workItem);

    }
}
