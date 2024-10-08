using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace azuredevopsapi.ApiUtilities
{
    public class Endpoints
    {

        public static readonly string CREATE_NEW_WI_ENDPOINT = "{organization}/{projectGuid}/_apis/wit/workitems/${workItemType}";
        public static readonly string GET_WORKITEM_ENDPOINT = "{organization}/{projectGuid}/_apis/wit/workitems/{WorkItemId}";
    }
}
