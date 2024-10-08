using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace azuredevopsapi.Models.Request
{
    public class CreateWorkItemRequest
    {
        public WorkItemFields[] wiFields { get; set; }
    }

    public class WorkItemFields
    {
        public string op { get; set; }
        public string path { get; set; }
        public object from { get; set; }
        public string value { get; set; }
    }

}
