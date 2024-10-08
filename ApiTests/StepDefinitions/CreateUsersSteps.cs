using azuredevopsapi;
using azuredevopsapi.Models.Request;
using azuredevopsapi.Models.Response;
using azuredevopsapi.Utility;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow;

namespace ApiTests.StepDefinitions
{
    [Binding]
    public class CreateUsersSteps
    {
        private CreateUserRequestRest createUserRequest;
        private RestResponse restResponse;
        private ScenarioContext scenarioContext; //provide methods related to steps, scenario,
        private HttpStatusCode statusCode;

        public CreateUsersSteps(CreateUserRequestRest createUserReq,  ScenarioContext scenarioContext) {
            this.createUserRequest = createUserReq;
            this.scenarioContext = scenarioContext;
        }

        [Given(@"User with name ""(.*)""")]
        public void GivenUserNameWithName(string userName) {
            
            createUserRequest.name = userName;

        }

        [Given(@"User with job ""(.*)""")]
        public void GivenUserWithJob(string job)
        {
            createUserRequest.job = job;
        }

        [When(@"Send request to create user")]
        public async Task WhenSendRequestToCreateUser()
        {
            var api = new ApiClient();
            restResponse = await api.CreateUser<CreateUserRequestRest>(createUserRequest);
        }

        [Then(@"Validate usr is created")]
        public void ThenValidateUsrIsCreated()
        {
            statusCode = restResponse.StatusCode;
            var code = (int)statusCode;
            Assert.AreEqual(201, code);

            var content = HandleContent.GetContent<CreateUserRest>(restResponse);
            Assert.AreEqual(createUserRequest.name, content.name);
            Assert.AreEqual(createUserRequest.job, content.job);


        }
    }
}
