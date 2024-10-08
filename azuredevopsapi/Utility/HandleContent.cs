using Newtonsoft.Json;
using RestSharp;


namespace azuredevopsapi.Utility
{
    public class HandleContent
    {
        public static T GetContent<T>(RestResponse restResponse) { 
            var content = restResponse.Content;
            return JsonConvert.DeserializeObject<T>(content);
        }

        public static T ParseJson<T>(string fileName) { 
            return JsonConvert.DeserializeObject<T>(File.ReadAllText(fileName));
        }
    }
}
