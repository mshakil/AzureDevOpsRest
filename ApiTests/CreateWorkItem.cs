using azuredevopsapi;
using azuredevopsapi.Models.Request;
using azuredevopsapi.Models.Response;
using azuredevopsapi.Utility;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow;

namespace ApiTests
{
    
    public class CreateWorkItem
    {
        private CreateWorkItemRequest createWorkItemRequest;
        
        private RestResponse restResponse;
        private HttpStatusCode statusCode;

        [Test]
        public async Task CreateNewWorkItem() {

            // Create the WorkItemFields array
            WorkItemFields[] fields = new WorkItemFields[]
            {
            new WorkItemFields
            {
                op = "add",
                path = "/fields/System.Title",
                from = null,
                value = "workItemType-" + DateTime.Now.ToString("yyyyMMddHHmmss") + "-Auto"
            },
            new WorkItemFields
            {
                op = "add",
                path = "/fields/System.Description",
                from = null,
                value = "Sample Description"
            },
            new WorkItemFields
            {
                op = "add",
                path = "/fields/System.Tags",
                from = null,
                value = "FOR KT SESSION"
            }
            };

            createWorkItemRequest = new CreateWorkItemRequest
            {
                wiFields = fields
            };


            string jsonPayload = JsonConvert.SerializeObject(fields, Formatting.Indented);
            var api = new ApiClient();
            restResponse = await api.CreateWorkItem<CreateWorkItemResponse>(jsonPayload);

            statusCode = restResponse.StatusCode;
            var code = (int)statusCode;
            Assert.AreEqual(200, code);

            var content = HandleContent.GetContent<CreateWorkItemResponse>(restResponse);
            //Assert.AreEqual("New Work Item", content.fields.SystemTitle);
            
        }
    }
}
