using azuredevopsapi.ApiUtilities;
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

namespace ApiTests.WorkItemTest
{

    public class CreateWorkItem : Hooks
    {
        private CreateWorkItemRequest createWorkItemRequest;

        private RestResponse restResponse;
        private HttpStatusCode statusCode;



        [Test]
        public async Task CreateNewWorkItem()
        {
            string workItemType = "Epic";
            string workItemName = "workItemType-" + DateTime.Now.ToString("yyyyMMddHHmmss") + "-Auto";
            string workItemDescription = "Sample Description";
            string workItemTags = "Created Via Api";
            // Create the WorkItemFields array
            WorkItemFields[] fields = new WorkItemFields[]
            {
            new WorkItemFields
            {
                op = "add",
                path = "/fields/System.Title",
                from = null,
                value = workItemName
            },
            new WorkItemFields
            {
                op = "add",
                path = "/fields/System.Description",
                from = null,
                value = workItemDescription
            },
            new WorkItemFields
            {
                op = "add",
                path = "/fields/System.Tags",
                from = null,
                value = workItemTags
            }
            };

            createWorkItemRequest = new CreateWorkItemRequest
            {
                wiFields = fields
            };

            

            string jsonPayload = JsonConvert.SerializeObject(fields, Formatting.Indented);
            var api = new ApiClient(BASE_URL, USER_NAME, USER_PAT, ORGANIZATION_NAME, PROJECT_GUID);

            restResponse = await api.CreateWorkItem<CreateWorkItemResponse>(jsonPayload, workItemType);

            statusCode = restResponse.StatusCode;
            var code = (int)statusCode;
            Assert.That(code, Is.EqualTo(200));

            var content = HandleContent.GetContent<CreateWorkItemResponse>(restResponse);
            Assert.That(content.fields["System.Title"], Is.EqualTo(workItemName));
            Assert.That(content.fields["System.Tags"], Is.EqualTo(workItemTags));
            Assert.That(content.fields["System.Description"], Is.EqualTo(workItemDescription));

            Console.WriteLine($"Workitem Id is: {content.id}");
            Console.WriteLine($"Workitem Title is: {content.fields["System.Title"]}");

        }
    }
}
