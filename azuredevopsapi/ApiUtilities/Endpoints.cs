using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace azuredevopsapi.ApiUtilities
{
    public class Endpoints
    {

        public static readonly string CREATE_NEW_WI_ENDPOINT = "{organization}/{projectGuid}/_apis/wit/workitems/${workItemType}?api-version=5.1";
    }
}
