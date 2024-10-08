using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace azuredevopsapi.Models.Response
{
    public class CreateWorkItemResponse
    {
        public int id { get; set; }
        public int rev { get; set; }
        public string url { get; set; }

        public Dictionary<string, object> fields { get; set; }
    }
}
